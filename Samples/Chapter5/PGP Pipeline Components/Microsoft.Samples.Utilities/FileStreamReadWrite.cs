namespace Microsoft.Samples.PipelineUtilities
{
	using System;
	using System.IO;

	/// <summary>
	/// Summary description for FileStreamReadWrite.
	/// </summary>
	public abstract class FileStreamReadWrite
	{
		public static void DumpStreamToFile (Stream fromStream, string toFilename)
		{
			FileStream file = null;
			try
			{
				file = new FileStream(toFilename, System.IO.FileMode.Create);
				byte[] tmpBuff = new byte[4096];
				int bytesRead = 0;

				while ((bytesRead = fromStream.Read(tmpBuff, 0, tmpBuff.Length)) != 0)
				{
					file.Write(tmpBuff, 0, bytesRead);
				}

				file.Close();
				file = null;
			}
			finally
			{
				if (file != null) file.Close();
			}
		}

		public static MemoryStream ReadFileToMemoryStream (string fromFilename)
		{
			FileStream file = null;
			try
			{
				file = new FileStream(fromFilename, System.IO.FileMode.Open);
				MemoryStream memStream = new MemoryStream();
				byte[] tmpBuff = new byte[4096];
				int bytesRead = 0;

				while ((bytesRead = file.Read(tmpBuff, 0, tmpBuff.Length)) != 0)
				{
					memStream.Write(tmpBuff, 0, bytesRead);
				}

				file.Close();
				file = null;

				memStream.Position = 0;
				return memStream;
			}
			finally
			{
				if (file != null) file.Close();
			}
		}

	}
}
