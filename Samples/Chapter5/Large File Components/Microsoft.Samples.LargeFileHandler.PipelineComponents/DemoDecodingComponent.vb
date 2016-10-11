Imports System
Imports System.IO
Imports System.Text
Imports System.Drawing
Imports System.Resources
Imports System.Reflection
Imports System.Diagnostics
Imports System.Collections
Imports System.ComponentModel
Imports Microsoft.BizTalk.Message.Interop
Imports Microsoft.BizTalk.Component.Interop
Imports Microsoft.BizTalk.Component
Imports Microsoft.BizTalk.Messaging
Imports Microsoft.BizTalk.Component.Utilities

Namespace Microsoft.Samples.LargeFileHandler.PipelinesComponents
    <ComponentCategory(CategoryTypes.CATID_PipelineComponent), _
         System.Runtime.InteropServices.Guid("89dedce4-0525-472f-899c-64dc66f60727"), _
         ComponentCategory(CategoryTypes.CATID_Decoder)> _
    Public Class DemoDecodingComponent
        Implements IBaseComponent, IPersistPropertyBag, IComponentUI, Global.Microsoft.BizTalk.Component.Interop.IComponent, IProbeMessage

        Private _OutBoundFileDocumentSpecification As SchemaWithNone = New Global.Microsoft.BizTalk.Component.Utilities.SchemaWithNone("")
        Private _InboundFileDocumentSpecification As SchemaWithNone = New Global.Microsoft.BizTalk.Component.Utilities.SchemaWithNone("")
        Private _ThresholdSize As Integer = 4096

        Private resourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Microsoft.Samples.LargeFileHandler.PipelinesComponents.DemoDecodingComponent", [Assembly].GetExecutingAssembly)
        Private Const PROPERTY_SCHEMA_NAMESPACE = "http://Microsoft.Samples.LargeFileHandler.Schemas.LargeFilePropertySchema"
        Private _FileLocation As String

        '<summary>
        'this property will contain a single schema
        '</summary>
        <Description("The inbound request document specification. Only messages of this type will be accepted by the component.")> <DisplayName("Inbound Specification")> _
        Public Property InboundFileDocumentSpecification() As Global.Microsoft.BizTalk.Component.Utilities.SchemaWithNone
            Get

                Return _InboundFileDocumentSpecification
            End Get
            Set(ByVal Value As Global.Microsoft.BizTalk.Component.Utilities.SchemaWithNone)
                _InboundFileDocumentSpecification = Value
            End Set
        End Property

        '<summary>
        'this property will contain a single schema
        '</summary>
        <Description("The Large File Message specification. The component will create messages of this type.")> <DisplayName("Outbound Specification")> _
        Public Property OutBoundFileDocumentSpecification() As Global.Microsoft.BizTalk.Component.Utilities.SchemaWithNone
            Get

                Return _OutBoundFileDocumentSpecification
            End Get
            Set(ByVal Value As Global.Microsoft.BizTalk.Component.Utilities.SchemaWithNone)
                _OutBoundFileDocumentSpecification = Value
            End Set
        End Property

        <Description("Threshold value in bytes for incoming file to determine whether or not to treat the message as large. Default is 4096 bytes")> <DisplayName("Threshold file size")> <DefaultValue(4096)> _
        Public Property ThresholdSize() As Integer
            Get
                Return Me._ThresholdSize
            End Get
            Set(ByVal value As Integer)
                Me._ThresholdSize = value
            End Set
        End Property

        <Description("Directory for storing decoded large messages. Defaults to C:\Temp.")> <DisplayName("Large File Folder Location")> _
Public Property LargeFileFolder() As String
            Get
                Return Me._FileLocation
            End Get
            Set(ByVal value As String)
                Me._FileLocation = value
            End Set
        End Property
        '<summary>
        'Name of the component
        '</summary>
        <Browsable(False)> _
        Public ReadOnly Property Name() As String Implements Global.Microsoft.BizTalk.Component.Interop.IBaseComponent.Name
            Get
                Return resourceManager.GetString("COMPONENTNAME", System.Globalization.CultureInfo.InvariantCulture)
            End Get
        End Property

        '<summary>
        'Version of the component
        '</summary>
        <Browsable(False)> _
        Public ReadOnly Property Version() As String Implements Global.Microsoft.BizTalk.Component.Interop.IBaseComponent.Version
            Get
                Return resourceManager.GetString("COMPONENTVERSION", System.Globalization.CultureInfo.InvariantCulture)
            End Get
        End Property

        '<summary>
        'Description of the component
        '</summary>
        <Browsable(False)> _
        Public ReadOnly Property Description() As String Implements Global.Microsoft.BizTalk.Component.Interop.IBaseComponent.Description
            Get
                Return resourceManager.GetString("COMPONENTDESCRIPTION", System.Globalization.CultureInfo.InvariantCulture)
            End Get
        End Property

        '<summary>
        'Component icon to use in BizTalk Editor
        '</summary>
        <Browsable(False)> _
        Public ReadOnly Property Icon() As IntPtr Implements Global.Microsoft.BizTalk.Component.Interop.IComponentUI.Icon
            Get
                Return CType(Me.resourceManager.GetObject("COMPONENTICON", System.Globalization.CultureInfo.InvariantCulture), System.Drawing.Bitmap).GetHicon
            End Get
        End Property

        '<summary>
        'Gets class ID of component for usage from unmanaged code.
        '</summary>
        '<param name="classid">
        'Class ID of the component
        '</param>
        Public Sub GetClassID(ByRef classid As System.Guid) Implements Global.Microsoft.BizTalk.Component.Interop.IPersistPropertyBag.GetClassID
            classid = New System.Guid("89dedce4-0525-472f-899c-64dc66f60727")
        End Sub

        '<summary>
        'not implemented
        '</summary>
        Public Sub InitNew() Implements Global.Microsoft.BizTalk.Component.Interop.IPersistPropertyBag.InitNew
        End Sub

        '<summary>
        'Loads configuration properties for the component
        '</summary>
        '<param name="pb">Configuration property bag</param>
        '<param name="errlog">Error status</param>
        Public Overridable Sub Load(ByVal pb As Global.Microsoft.BizTalk.Component.Interop.IPropertyBag, ByVal errlog As Integer) Implements Global.Microsoft.BizTalk.Component.Interop.IPersistPropertyBag.Load
            Try
                Me._ThresholdSize = ReadPropertyBag(pb, "ThresholdSize")
            Catch
                Me._ThresholdSize = 4096
            End Try

            Try
                Me._FileLocation = ReadPropertyBag(pb, "FileLocation")
            Catch
                Me._FileLocation = "C:\Temp"
            End Try
            Try
                Me.InboundFileDocumentSpecification = New SchemaWithNone(ReadPropertyBag(pb, "InboundFileDocumentSpecification"))
            Catch
                Me.InboundFileDocumentSpecification = New SchemaWithNone("")
            End Try
            Try
                Me.OutBoundFileDocumentSpecification = New SchemaWithNone(ReadPropertyBag(pb, "OutboundFileDocumentSpecification"))
            Catch
                Me.OutBoundFileDocumentSpecification = New SchemaWithNone("")
            End Try


        End Sub

        '<summary>
        'Saves the current component configuration into the property bag
        '</summary>
        '<param name="pb">Configuration property bag</param>
        '<param name="fClearDirty">not used</param>
        '<param name="fSaveAllProperties">not used</param>
        Public Overridable Sub Save(ByVal pb As Global.Microsoft.BizTalk.Component.Interop.IPropertyBag, ByVal fClearDirty As Boolean, ByVal fSaveAllProperties As Boolean) Implements Global.Microsoft.BizTalk.Component.Interop.IPersistPropertyBag.Save

            WritePropertyBag(pb, "ThresholdSize", Me._ThresholdSize)
            WritePropertyBag(pb, "FileLocation", Me._FileLocation)
            WritePropertyBag(pb, "InboundFileDocumentSpecification", _InboundFileDocumentSpecification.SchemaName)
            WritePropertyBag(pb, "OutboundFileDocumentSpecification", _OutBoundFileDocumentSpecification.SchemaName)

        End Sub

        '<summary>
        'Reads property value from property bag
        '</summary>
        '<param name="pb">Property bag</param>
        '<param name="propName">Name of property</param>
        '<returns>Value of the property</returns>
        Private Function ReadPropertyBag(ByVal pb As Global.Microsoft.BizTalk.Component.Interop.IPropertyBag, ByVal propName As String) As Object
            Dim val As Object = Nothing
            Try
                pb.Read(propName, val, 0)
            Catch e As System.ArgumentException
                Return val
            Catch e As System.Exception
                Throw New System.ApplicationException(e.Message)
            End Try
            Return val
        End Function

        '<summary>
        'Writes property values into a property bag.
        '</summary>
        '<param name="pb">Property bag.</param>
        '<param name="propName">Name of property.</param>
        '<param name="val">Value of property.</param>
        Private Sub WritePropertyBag(ByVal pb As Global.Microsoft.BizTalk.Component.Interop.IPropertyBag, ByVal propName As String, ByVal val As Object)
            Try
                pb.Write(propName, val)
            Catch e As System.Exception
                Throw New System.ApplicationException(e.Message)
            End Try
        End Sub

        '<summary>
        'The Validate method is called by the BizTalk Editor during the build 
        'of a BizTalk project.
        '</summary>
        '<param name="obj">An Object containing the configuration properties.</param>
        '<returns>The IEnumerator enables the caller to enumerate through a collection of strings containing error messages. These error messages appear as compiler error messages. To report successful property validation, the method should return an empty enumerator.</returns>
        Public Function Validate(ByVal obj As Object) As System.Collections.IEnumerator Implements Global.Microsoft.BizTalk.Component.Interop.IComponentUI.Validate
            'example implementation:
            'ArrayList errorList = new ArrayList();
            'errorList.Add("This is a compiler error");
            'return errorList.GetEnumerator();
            Return Nothing
        End Function
        '<summary>
        'called by the messaging engine when a new message arrives
        'checks if the incoming message is in a recognizable format
        'if the message is in a recognizable format, only this component
        'within this stage will be execute (FirstMatch equals true)
        '</summary>
        '<param name="pc">the pipeline context</param>
        '<param name="inmsg">the actual message</param>
        Public Function Probe(ByVal pc As Global.Microsoft.BizTalk.Component.Interop.IPipelineContext, ByVal inmsg As Global.Microsoft.BizTalk.Message.Interop.IBaseMessage) As Boolean Implements Global.Microsoft.BizTalk.Component.Interop.IProbeMessage.Probe

            Dim streamReader As New StreamReader(inmsg.BodyPart.Data)
            Dim xmlreader As New Xml.XmlTextReader(inmsg.BodyPart.Data)
            xmlreader.MoveToContent()

            If (_InboundFileDocumentSpecification.DocSpecName = xmlreader.NamespaceURI.Replace("http://", "")) Then
                Return True
            Else
                Return False
            End If

        End Function
        '<summary>
        'Implements IComponent.Execute method.
        '</summary>
        '<param name="pc">Pipeline context</param>
        '<param name="inmsg">Input message</param>
        '<returns>Original input message</returns>
        '<remarks>
        'IComponent.Execute method is used to initiate
        'the processing of the message in this pipeline component.
        '</remarks>
        Public Function Execute(ByVal pContext As IPipelineContext, ByVal inmsg As IBaseMessage) As Global.Microsoft.BizTalk.Message.Interop.IBaseMessage Implements Global.Microsoft.BizTalk.Component.Interop.IComponent.Execute
            'Build the message that is to be sent out but only if it is greater than the threshold
            If inmsg.BodyPart.GetOriginalDataStream.Length > Me._ThresholdSize Then
                StoreMessageData(pContext, inmsg)
            End If
            Return inmsg
        End Function

        '<summary>
        'Method used to write the message data to a file and promote the   
        'location to the MessageContext.
        '</summary>
        '<param name="pc">Pipeline context</param>
        '<param name="inmsg">Input message to be assigned</param>
        '<returns>Original input message by reference</returns>
        '<remarks>
        'Receives the input message ByRef then assigns the file stream to the messageBody.Data property
        '</remarks>
        Private Sub StoreMessageData(ByVal pContext As IPipelineContext, ByRef inMsg As IBaseMessage)
            Dim FullFileName As String = _FileLocation + inMsg.MessageID.ToString + ".msg"
            Dim dataFile As New FileStream(FullFileName, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite, 4096)
            Dim myMemoryStream As Stream = inMsg.BodyPart.GetOriginalDataStream

            Dim Buffer(4095) As Byte
            Dim byteCount As Integer

            'Not really needed, just want to initialize the data within the message part to something.
            'Proper way to do this would be to create a separate XML schema for messages which have been encoded using the
            'encoder, create a new empty document which has an element named "FilePath" and set the value of the element
            'to FullFileName. But at least this way we can see the value in the document should we need to write it out
            Dim myStream As New MemoryStream(UTF8Encoding.Default.GetBytes(FullFileName))

            If myMemoryStream.CanSeek Then
                myMemoryStream.Position = 0
            Else
                'Impossible to occur, but added it anyway
                Throw New Exception("The stream is not seekable")
            End If

            byteCount = myMemoryStream.Read(Buffer, 0, 4096)

            While myMemoryStream.Position < myMemoryStream.Length - 1 'pos < inMsg.BodyPart.Data.Length - 1
                dataFile.Write(Buffer, 0, 4096)
                dataFile.Flush()
                byteCount = myMemoryStream.Read(Buffer, 0, 4096)
            End While
            dataFile.Write(Buffer, 0, byteCount)
            dataFile.Flush()
            dataFile.Close()
            inMsg.BodyPart.Data = myStream
            inMsg.Context.Promote("LargeFileLocation", PROPERTY_SCHEMA_NAMESPACE, FullFileName)

            'Useful for CBR operations - i.e. route all messages that are large to a specific send port.
            inMsg.Context.Promote("IsEncoded", PROPERTY_SCHEMA_NAMESPACE, True)

        End Sub

    End Class
End Namespace
