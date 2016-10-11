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
	[ComponentCategory(CategoryTypes.CATID_Decoder)]
	[System.Runtime.InteropServices.Guid("67C8CFB9-D89A-4415-A112-76187FC294D1")]
	public class ZipDecodeComponent :
		IBaseComponent,
		Microsoft.BizTalk.Component.Interop.IComponent,
		Microsoft.BizTalk.Component.Interop.IPersistPropertyBag,
		IComponentUI
	{

		// Component information
		#region IBaseComponent
		[Browsable(false)] public string Name { get { return "ZIP decoder"; } }
		[Browsable(false)] public string Version { get { return "1.0"; } }
		[Browsable(false)] public string Description { get { return "Zip Decode Pipeline Component"; } }
		[Browsable(false)] public System.IntPtr Icon	{ get { return ((Bitmap)resourceManager.GetObject("IconBitmap")).GetHicon(); } }
		#endregion

		private System.Resources.ResourceManager resourceManager =
			new System.Resources.ResourceManager("Microsoft.Utility.PipelineGnuPG.ZipDecodeComponent", Assembly.GetExecutingAssembly());

		// Property: Password
		private string _password;
		[
		DisplayName("Password"),
		Description("Password used to unzip messages.")
		]
		public string Password
		{
			get {  return _password; }
			set {  _password = value; }
		}

		private Stream Decode (Stream inStream)
		{
			Stream outStream = inStream;
			string inFile = Path.GetTempFileName();
			string outFile = Path.ChangeExtension(inFile, "txt");

			try 
			{
				ZipInputStream zipStream = new ZipInputStream( inStream );

				// get password, if supplied
				if ((_password != null) && (_password != ""))
					zipStream.Password = _password;

				// this algorithm demands that the zip archive contain exactly one file
				ZipEntry entry = zipStream.GetNextEntry();
				if (entry == null)
					throw new ApplicationException( "Input ZIP archive does not contain any files - expecting exactly one file" );
				if (entry.IsDirectory)
					throw new ApplicationException( "Input ZIP contains a directory - expecting exactly one file" );

				// copy the compressed stream into the output stream
				outStream = new MemoryStream();
				byte[] buffer = new byte[4096];
				int count = 0;
				while ((count = zipStream.Read(buffer, 0, buffer.Length)) != 0)
					outStream.Write( buffer, 0, count );

				// make sure that was the one and only file
				entry = zipStream.GetNextEntry();
				if (entry != null)
					throw new ApplicationException( "Input ZIP archive contains multiple files and/or directories - expecting exactly one file" );

				zipStream.Close();

#if DEBUG
				outStream.Seek( 0, SeekOrigin.Begin );
				Microsoft.Samples.PipelineUtilities.FileStreamReadWrite.DumpStreamToFile( outStream, outFile );
#endif

				outStream.Seek( 0, SeekOrigin.Begin );
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
				throw;
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
			classID = new Guid ("19800584-283D-44da-B1EE-0968387DA088");
		}

		public void Load(IPropertyBag propertyBag, int errorLog)
		{
			string text;
			text = (string)PropertyBagReadWrite.ReadPropertyBag( propertyBag, "Password" );
			if (text != null) _password = text;
		}

		public void Save(IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
		{
			object val;
			val = (object)_password;
			PropertyBagReadWrite.WritePropertyBag( propertyBag, "Password", val );
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
					pInMsg.BodyPart.Data = Decode( originalStream );
					pContext.ResourceTracker.AddResource( pInMsg.BodyPart.Data );
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine( "Exception caught in ZipDecodeComponent::Execute: " + ex.Message );
				throw new ApplicationException( "ZipDecodeComponent was unable to decompress input stream. This may occur if there is more than one file in the zip archive. See inner exception for more information.", ex );
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
