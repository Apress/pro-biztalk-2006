namespace Microsoft.Utility.PipelineGnuPG
{
	using System;
	using System.ComponentModel;
	using System.Collections;
	using System.Diagnostics;
	using System.Drawing;
	using System.IO;
	using System.Reflection;
	using Microsoft.BizTalk.Component.Interop;
	using Microsoft.Samples.PipelineUtilities;
	using Microsoft.Utilities.Cryptography.GnuPG;

	[ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
	[ComponentCategory(CategoryTypes.CATID_Encoder)]
	[System.Runtime.InteropServices.Guid("C1917FE1-841B-4583-A59E-B57F76871899")]
	public class GnuPGEncodeComponent :
		IBaseComponent,
		Microsoft.BizTalk.Component.Interop.IComponent,
		Microsoft.BizTalk.Component.Interop.IPersistPropertyBag,
		IComponentUI
	{

		// Component information
		#region IBaseComponent
		[Browsable(false)] public string Name { get { return "Gnu PGP encoder"; } }
		[Browsable(false)] public string Version { get { return "1.0"; } }
		[Browsable(false)] public string Description { get { return "GnuPG Encode Pipeline Component"; } }
		[Browsable(false)] public System.IntPtr Icon	{ get { return ((Bitmap)resourceManager.GetObject("IconBitmap")).GetHicon(); } }
		#endregion

		private System.Resources.ResourceManager resourceManager =
			new System.Resources.ResourceManager("Microsoft.Utility.PipelineGnuPG.GnuPGEncodeComponent", Assembly.GetExecutingAssembly());

		// Property: Recipient
		private string _recipient;
		[
		DisplayName("Recipient"),
		Description("Recipient identifier used to retreive public key from encryption.")
		]
		public string Recipient
		{
			get {  return _recipient; }
			set {  _recipient = value; }
		}

		// Property: GnuPGBinDir
		private string _gnupgbindir;
		[
		DisplayName("GnuPGBinDir"),
		Description(@"Installation directory of GnuPG, that contains gpg.exe file. Default, if not specified, is ""C:\gnupg"". Do not include trailing slash.")
		]
		public string GnuPGBinDir
		{
			get {  return _gnupgbindir; }
			set {  _gnupgbindir = value; }
		}

		private Stream Encode (Stream inStream)
		{
			Stream outStream = inStream;
			string inFile = Path.GetTempFileName();
			string outFile = Path.ChangeExtension(inFile, "gpg");

			try 
			{
				FileStreamReadWrite.DumpStreamToFile( inStream, inFile );

				GnuPGWrapper GPG = new GnuPGWrapper(_gnupgbindir);
				GnuPGCommand GPGCommand = GPG.Command;
				GPGCommand.Command = Commands.Encrypt;
				GPGCommand.Recipient = _recipient;
				GPGCommand.Armor = true;
				GPGCommand.InputFile = inFile;
				GPGCommand.OutputFile = outFile;
				
				GPG.Execute(null);
					
				outStream = FileStreamReadWrite.ReadFileToMemoryStream( outFile );
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
			}
			finally
			{
				if (File.Exists(inFile))
				{
					File.Delete(inFile);
				}

				if (File.Exists(outFile))
				{
					File.Delete(outFile);
				}
			}

			return outStream;
		}

		#region IPersistPropertyBag Members

		public void InitNew()
		{
		}

		public void GetClassID(out Guid classID)
		{
			classID = new Guid ("A398E8D1-4213-4438-9010-66F366D4BDF4");
		}

		public void Load(IPropertyBag propertyBag, int errorLog)
		{
			string text;
			text = (string)PropertyBagReadWrite.ReadPropertyBag( propertyBag, "Recipient" );
			if (text != null) _recipient = text;
			text = (string)PropertyBagReadWrite.ReadPropertyBag( propertyBag, "GnuPGBinDir" );
			if (text != null) _gnupgbindir = text;
		}

		public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
		{
			object val;
			val = (object)_recipient;
			PropertyBagReadWrite.WritePropertyBag( propertyBag, "Recipient", val );
			val = (object)_gnupgbindir;
			PropertyBagReadWrite.WritePropertyBag( propertyBag, "GnuPGBinDir", val );
		}

		#endregion
	
	
		#region IComponent Members

		public Microsoft.BizTalk.Message.Interop.IBaseMessage Execute(IPipelineContext pContext, Microsoft.BizTalk.Message.Interop.IBaseMessage pInMsg)
		{
			try
			{
				if (pInMsg != null)
				{
					Stream originalStream = pInMsg.BodyPart.GetOriginalDataStream();
					pInMsg.BodyPart.Data = Encode( originalStream );
					pContext.ResourceTracker.AddResource( pInMsg.BodyPart.Data );
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine( "Exception caught in GnuPGDecodeComponent::Execute: " + ex.Message );
			}
			return pInMsg;
		}

		#endregion

		#region IComponentUI Members

		/// <summary>
		/// The Validate method is called by the BizTalk Editor during the build 
		/// of a BizTalk project.
		/// </summary>
		/// <param name="obj">An Object containing the configuration properties.</param>
		/// <returns>The IEnumerator enables the caller to enumerate through a collection of strings containing error messages. These error messages appear as compiler error messages. To report successful property validation, the method should return an empty enumerator.</returns>
		public IEnumerator Validate(object projectSystem)
		{
			// example implementation:
			// ArrayList errorList = new ArrayList();
			// errorList.Add("This is a compiler error");
			// return errorList.GetEnumerator();
			return null;
		}
        

		#endregion
	}
}
