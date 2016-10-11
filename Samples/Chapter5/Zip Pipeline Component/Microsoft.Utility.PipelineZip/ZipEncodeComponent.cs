namespace Microsoft.Utility.PipelineZip
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
	using ICSharpCode.SharpZipLib.Zip;

	[ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
	[ComponentCategory(CategoryTypes.CATID_Encoder)]
	[System.Runtime.InteropServices.Guid("56C7B68B-F288-4f78-A67F-20043CA4943E")]
	public class ZipEncodeComponent :
		IBaseComponent,
		Microsoft.BizTalk.Component.Interop.IComponent,
		Microsoft.BizTalk.Component.Interop.IPersistPropertyBag,
		IComponentUI
	{

		// Component information
		#region IBaseComponent
		[Browsable(false)] public string Name { get { return "ZIP encoder"; } }
		[Browsable(false)] public string Version { get { return "1.0"; } }
		[Browsable(false)] public string Description { get { return "Zip Encode Pipeline Component"; } }
		[Browsable(false)] public System.IntPtr Icon	{ get { return ((Bitmap)resourceManager.GetObject("IconBitmap")).GetHicon(); } }
		#endregion

		private System.Resources.ResourceManager resourceManager =
			new System.Resources.ResourceManager("Microsoft.Utility.PipelineGnuPG.ZipEncodeComponent", Assembly.GetExecutingAssembly());

		// Property: Password
		private string _password;
		[
		DisplayName("Password"),
		Description("Password used to zip messages.")
		]
		public string Password
		{
			get {  return _password; }
			set {  _password = value; }
		}

		// Property: Filename
		private const string DEFAULT_FILENAME = "file";
		private string _filename;
		[
		DisplayName("Filename"),
		Description(@"The name of the file that contains the output message.  This file will be added to the ZIP compressed output archive.  Default is """ + DEFAULT_FILENAME + @""".")
		]
		public string Filename
		{
			get {  return _filename; }
			set {  _filename = value; }
		}

		// Property: CompressionLevel
		const string DEFAULT_COMPRESSIONLEVEL_TEXT = "5";
		const int DEFAULT_COMPRESSIONLEVEL= 5;
		private string _compressionlevel;
		[
		DisplayName("CompressionLevel"),
		Description(@"Compression level: 0=Store Only, to 9=Best Compression.  Default is '" + DEFAULT_COMPRESSIONLEVEL_TEXT + "'.")
		]
		public string CompressionLevel
		{
			get {  return _compressionlevel; }
			set {  _compressionlevel = value; }
		}

		private Stream Encode (Stream inStream)
		{
			Stream outStream = inStream;
			string inFile = Path.GetTempFileName();
			string outFile = Path.ChangeExtension(inFile, "zip");

			try 
			{
				ZipOutputStream zipStream = new ZipOutputStream( File.Create( outFile ) );

				// get password, if supplied
				if ((_password != null) && (_password != ""))
					zipStream.Password = _password;

				// get compression level, if supplied
				int compressionlevel = DEFAULT_COMPRESSIONLEVEL;
				if ((_compressionlevel != null) && (_compressionlevel != ""))
					compressionlevel = Convert.ToInt32( _compressionlevel );
				if ((compressionlevel < 0) || (compressionlevel > 9))
					compressionlevel = DEFAULT_COMPRESSIONLEVEL;
				zipStream.SetLevel( compressionlevel );

				// get message filename, if supplied
				string filename = (((_filename != null) && (_filename != "")) ? _filename : DEFAULT_FILENAME );
				ZipEntry entry = new ZipEntry( filename );
				zipStream.PutNextEntry( entry );

				// copy the input into the compressed output stream
				byte[] buffer = new byte[4096];
				int count = 0;
				while ((count = inStream.Read(buffer, 0, buffer.Length)) != 0)
					zipStream.Write( buffer, 0, count );
				zipStream.Finish();
				zipStream.Close();

                outStream = Microsoft.Samples.PipelineUtilities.FileStreamReadWrite.ReadFileToMemoryStream(outFile);
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
			classID = new Guid ("0F94CF83-0B04-49a6-B73C-70473E0CF96F");
		}

		public void Load(IPropertyBag propertyBag, int errorLog)
		{
			string text;
			text = (string)PropertyBagReadWrite.ReadPropertyBag( propertyBag, "Password" );
			if (text != null) _password = text;
			text = (string)PropertyBagReadWrite.ReadPropertyBag( propertyBag, "Filename" );
			if (text != null) _filename = text;
			text = (string)PropertyBagReadWrite.ReadPropertyBag( propertyBag, "CompressionLevel" );
			if (text != null) _compressionlevel = text;
		}

		public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
		{
			object val;
			val = (object)_password;
			PropertyBagReadWrite.WritePropertyBag( propertyBag, "Password", val );
			val = (object)_filename;
			PropertyBagReadWrite.WritePropertyBag( propertyBag, "Filename", val );
			val = (object)_compressionlevel;
			PropertyBagReadWrite.WritePropertyBag( propertyBag, "CompressionLevel", val );
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
				System.Diagnostics.Debug.WriteLine( "Exception caught in ZipEncodeComponent::Execute: " + ex.Message );
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
