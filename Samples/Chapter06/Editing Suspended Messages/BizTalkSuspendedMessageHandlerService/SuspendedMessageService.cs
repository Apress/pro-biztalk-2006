using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Management;
using System.Xml;


namespace BizTalkSuspendedMessageHandlerService
{
	public partial class Service1: ServiceBase
	{
		public Service1()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			// listen for messages
string scope = "root\\MicrosoftBizTalkServer";
string wqlQuery = "Select * from MSBTS_ServiceInstanceSuspendedEvent";
watcher = new ManagementEventWatcher(scope, wqlQuery);
watcher.EventArrived += new EventArrivedEventHandler(MyEventHandler);
watcher.Start();

		}

static public void MyEventHandler(object sender, EventArrivedEventArgs e)
{
try
{
// Read the TempDirectoryName from config file
string TempDirectoryName = ConfigurationSettings.AppSettings["SuspendedMessagesTempFileLocation"];
// Read WaitingDirectoryName
// This folder is the location for the new XML document that this service
// creates based on context and message parts.
string WaitingDirectoryName = ConfigurationSettings.AppSettings["SuspendedMessagesFileLocation"];
// If you want to add processing instruction for InfoPath
// this will get it
string pi = ConfigurationSettings.AppSettings["ProcessingInstruction"];

string waitingMessageFileName;
// xwriter for suspended message
XmlTextWriter xwriter;

//Lookup MSBTS_ServiceInstanceSuspendedEvent in the BTS04 documentation for additional properties
string ErrorID = e.NewEvent["ErrorID"].ToString();
string ErrorCategory = e.NewEvent["ErrorCategory"].ToString();
string ErrorDescription = e.NewEvent["ErrorDescription"].ToString();
string ServiceStatus = e.NewEvent["ServiceStatus"].ToString();
string ServiceInstanceID = e.NewEvent["InstanceID"].ToString();   
EnumerationOptions enumOptions = new EnumerationOptions();

enumOptions.ReturnImmediately = false;

ManagementObjectSearcher MessageInstancesInServiceInstance = new ManagementObjectSearcher("root\\MicrosoftBizTalkServer",
	"Select * from MSBTS_MessageInstance where ServiceInstanceID='"+ ServiceInstanceID + "'",enumOptions);
//Enumerate through the result set
foreach(ManagementObject MessageInstance in MessageInstancesInServiceInstance.Get())
{
	// The only way to get at the message body is to utilise the SaveToFile
	// method on the BTS_MessageInstance WMI Class.
	// This saves all of the message information to files.
	// Each MessagePart making up a Message is saved in seperate files, typically you only
	// get a Body, but we cater for Multi-Part messages to cover all scenarios

	// As well as the MessageParts a context file is created, we need to use this to
	// extract the MessagePartID's and MessagePartName's out so we can then 
// work out the filenames
	// to open!

	// The context filename format is:
	// <MessageInstanceID>_context.xml

	// And then the actual message information filename format is:

	// <MessageInstanceID>_<MessagePartID>[_<MessagePartName>].out
	// MessagePartName is only required if the MessagePart has a name!

	// We need to build this filename up so we can load it up - no hacking here!

	// Save the files
	MessageInstance.InvokeMethod("SaveToFile", new object[] {TempDirectoryName});

	// Get the MessageInstanceID
	string MessageInstanceID = MessageInstance["MessageInstanceID"].ToString();

	// We now need to load the context file up to get the MessagePart information
	string ContextFileName = 
String.Format(@"{0}\{1}_context.xml",TempDirectoryName,MessageInstanceID);

	// Load the context file up
	XmlDocument doc = new XmlDocument();
	doc.Load(ContextFileName);

	// pull out context properties that we are interested in
	string ReceivedFileName = GetContextProperty(doc, "ReceivedFileName");
	string InboundTransportLocation = GetContextProperty(doc, "InboundTransportLocation");
	string InterchangeID = GetContextProperty(doc, "InterchangeID");
	string ReceivePortID = GetContextProperty(doc, "ReceivePortID");
	string ReceivePortName = GetContextProperty(doc, "ReceivePortName");
	
	// Create an XmlWriter to store the data. 
// This will get written to a file when complete.
	waitingMessageFileName = 
String.Format(@"{0}\{1}.xml",WaitingDirectoryName,MessageInstanceID);
	xwriter = new XmlTextWriter(waitingMessageFileName, System.Text.Encoding.UTF8);
	xwriter.Formatting = Formatting.Indented;
	xwriter.WriteStartDocument();
	//Write the ProcessingInstruction node.
	xwriter.WriteProcessingInstruction("mso-infoPathSolution", pi);
	xwriter.WriteProcessingInstruction("mso-application", "progid=\"InfoPath.Document\"");
	xwriter.WriteComment(string.Format("Created on {0}", DateTime.Now.ToString()));			
	//Write the context information
	xwriter.WriteStartElement("ns0", "SuspendedMessage", 
"http://Microsoft.BizTalk.SuspendQueue.SuspendedMessage");
	xwriter.WriteStartElement("Context");
	xwriter.WriteElementString("ReceivedFileName", ReceivedFileName);
	xwriter.WriteElementString("InboundTransportLocation", InboundTransportLocation);
	xwriter.WriteElementString("InterchangeID", InterchangeID);
	xwriter.WriteElementString("ReceivePortID", ReceivePortID);
	xwriter.WriteElementString("ReceivePortName", ReceivePortName);
	xwriter.WriteEndElement(); // Context
	// Start the Message Element
	xwriter.WriteStartElement("Message");

	// Use XPath to return all of the MessagePart(s) referenced in the context
	// We can then load the file up to get the Message information
	XmlNodeList MessageParts = doc.SelectNodes("/MessageInfo/PartInfo/MessagePart");
	foreach (XmlNode MessagePart in MessageParts)
	{
		// Pull the MessagePart info out that we need
		string MessagePartID = MessagePart.Attributes["ID"].Value;
		string MessagePartName = MessagePart.Attributes["Name"].Value;
		string Contents;
		string FileName;
		// If we have a MessagePartName append this to the end of the filename
		// It's optional so if we don't have it we don't worry about it
		if (MessagePartName.Length > 0)
		{
			FileName = String.Format(@"{0}\{1}_{2}_{3}.out",TempDirectoryName,MessageInstanceID,MessagePartID,MessagePartName);
		}
		else
		{
			FileName = String.Format(@"{0}\{1}_{2}.out",TempDirectoryName,MessageInstanceID,MessagePartID);
		}

		// Load the message, place it in canonical schema and then submit it.
		// Create an instance of StreamReader to read from a file.
		// The using statement also closes the StreamReader.
		using (StreamReader sr = new StreamReader(FileName)) 
		{
			// Read to end of file
			Contents = sr.ReadToEnd();
		}

		// Write out Message part data
		xwriter.WriteStartElement("MessagePart");
		xwriter.WriteElementString("MessagePartId", MessagePartID);
		xwriter.WriteElementString("Name", MessagePartName);
		xwriter.WriteStartElement("Contents");
		// Write out contets as CDATA. You may need a different strategy for this.
		xwriter.WriteCData(Contents);
		xwriter.WriteEndElement(); // Contents
		xwriter.WriteEndElement(); // MessagePart
	}
	xwriter.WriteEndElement(); // Message
	xwriter.WriteEndElement(); // SuspendedMessage
	xwriter.Close(); 
}
}
catch(Exception ex)
{
throw ex;
}
}

// helper function to pull out context properties given a property name
static private string GetContextProperty(XmlDocument doc, string propertyName)
{
XmlNode MessageContext = doc.SelectSingleNode("/MessageInfo/ContextInfo/Property[@Name='" + propertyName + "']");
if (MessageContext != null)
{
if (MessageContext.Attributes["Value"] != null)
{
	return MessageContext.Attributes["Value"].Value;
}
else
	return "Value no found";
}
else
return "Property not found";
}
 
		protected override void OnStop()
		{
			// TODO: Add code here to perform any tear-down necessary to stop your service.
		}
	}
}
