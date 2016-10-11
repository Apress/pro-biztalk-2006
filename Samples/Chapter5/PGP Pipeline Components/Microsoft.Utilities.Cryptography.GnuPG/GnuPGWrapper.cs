using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Microsoft.Utilities.Cryptography.GnuPG
{
	public class GnuPGWrapper
	{
		private Process GnuPGProcess = null;
		private GnuPGCommand GPGCommand = null;
		private int processTimeOutMilliseconds = 30000;
		private int exitCode = 0;
		private string outString = "";
		private string errString = "";
		private const string executableName = @"gpg.exe";
		private const string default_binDir = @"C:\gnupg";

		private string binDir = "";

		#region Properties
		
		public int ExitCode
		{
			get{ return(exitCode); }
		}

		public string ErrorMessage
		{
			get{ return(errString); }
		}

		public GnuPGCommand Command
		{
			get { return(this.GPGCommand); }
		}

		public int ProcessTimeOut
		{
			get { return(processTimeOutMilliseconds); }
			set	{ processTimeOutMilliseconds = value; }
		}

		#endregion

		public GnuPGWrapper()
		{
			this.binDir = default_binDir;
			GPGCommand = new GnuPGCommand();
			GPGCommand.HomeDirectory = default_binDir;
		}

		public GnuPGWrapper(string binDir)
		{
			this.binDir = (binDir == null ? default_binDir : binDir);
			GPGCommand = new GnuPGCommand();
			GPGCommand.HomeDirectory = this.binDir;
		}

		public void Initialize(string binDir)
		{
			this.binDir = (binDir == null ? default_binDir : binDir);
			GPGCommand.HomeDirectory = this.binDir;
		}

		public string Execute(string inText)
		{
			string fileName = this.binDir + @"\" + executableName;
			string arguments = GPGCommand.ToString();
			ProcessStartInfo procInfo = new ProcessStartInfo( fileName, arguments );
			procInfo.WorkingDirectory = binDir;
			procInfo.CreateNoWindow = true;
			procInfo.UseShellExecute = false;
			procInfo.RedirectStandardInput = true;
			procInfo.RedirectStandardOutput = true;
			procInfo.RedirectStandardError = true;

			GnuPGProcess = Process.Start(procInfo);

			string passphrase = GPGCommand.Passphrase;
			if ((passphrase != null) && (passphrase != ""))
			{
				if (procInfo.RedirectStandardInput)
				{
					GnuPGProcess.StandardInput.WriteLine( passphrase );
					GnuPGProcess.StandardInput.Flush();
				}
			}

			if (inText != null)
			{
				if (procInfo.RedirectStandardInput)
				{
					GnuPGProcess.StandardInput.WriteLine(inText);
					GnuPGProcess.StandardInput.Flush();
				}
			}

			if (procInfo.RedirectStandardInput)
				GnuPGProcess.StandardInput.Close();
			outString = "";
			errString = "";

			ThreadStart stdOutDelegate = new ThreadStart(StandardOutHandler);
			Thread stdOutThread = new Thread(stdOutDelegate);
			stdOutThread.Start();
			ThreadStart stdErrDelegate = new ThreadStart(StanderErrHandler);
			Thread stdErrThread = new Thread(stdErrDelegate);
			stdErrThread.Start();

			if (GnuPGProcess.WaitForExit(processTimeOutMilliseconds))
			{
				if (!stdOutThread.Join(processTimeOutMilliseconds/2))
				{
					stdOutThread.Abort();
				}
				if (!stdErrThread.Join(processTimeOutMilliseconds/2))
				{
					stdErrThread.Abort();
				}
			}
			else
			{
				outString = "";
				errString = "Timed out after " + processTimeOutMilliseconds.ToString() + " milliseconds";
				GnuPGProcess.Kill();
				GnuPGProcess.WaitForExit();

				if (stdOutThread.IsAlive)
				{
					stdOutThread.Abort();
				}

				if (stdErrThread.IsAlive)
				{
					stdErrThread.Abort();
				}
			}

			this.exitCode = GnuPGProcess.ExitCode;

			// don't allow warnings to throw errors, so if the call to GnuPG writes something to
			// stdout then we return it, and whatever is on stderr, we only throw an error if
			// there is nothing returned on stdout and the exitcode is not 0.
			if ((exitCode != 0) && (outString == ""))
			{
				if (errString == "")
				{
					errString = "GPGNET: [" + GnuPGProcess.ExitCode.ToString() + "]: Unknown error";
				}

				throw new GnuPGException(errString);
			}

			System.Diagnostics.Debug.WriteLine( string.Format(" Exit Code = {0}", exitCode ) );
			System.Diagnostics.Debug.WriteLine( string.Format(" Output = {0}", outString ) );
			System.Diagnostics.Debug.WriteLine( string.Format(" Error = {0}", errString ) );
			return outString;
		}

		#region Thread Handlers

		public void StandardOutHandler()
		{
			string output = "";
			if (this.GnuPGProcess.StartInfo.RedirectStandardOutput)
				output = this.GnuPGProcess.StandardOutput.ReadToEnd();

			lock(this)
			{
				this.outString = output;
			}
		}

		public void StanderErrHandler()
		{
			string error = "";
			if (this.GnuPGProcess.StartInfo.RedirectStandardError)
				this.GnuPGProcess.StandardError.ReadToEnd();

			lock(this)
			{
				this.errString = error;
			}
		}

		#endregion
	}
}
