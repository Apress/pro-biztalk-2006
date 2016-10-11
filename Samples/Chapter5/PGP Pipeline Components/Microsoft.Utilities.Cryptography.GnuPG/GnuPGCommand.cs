using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Microsoft.Utilities.Cryptography.GnuPG
{
	public enum Commands 
	{ 
		Encrypt, 
		Decrypt 
	};

	public enum VerboseLevel 
	{ 
		NoVerbose, 
		Verbose, 
		VeryVerbose,
		ExtremelyVerbose
	};

	/// <summary>
	/// Summary description for GnuPGCommand.
	/// </summary>
	public class GnuPGCommand
	{
		private Commands command = Commands.Decrypt;
		private bool armor = true;
		private bool yes = true;
		private string recipient = "";
		private string homeDir = "";
		private string passphrase = "";
		private VerboseLevel verbose = VerboseLevel.NoVerbose;
		private bool batch = true;
		private string originator = "";
		private string inputFile = "";
		private string outputFile = "";

		private bool recipientRequired = false;
		private bool passphraseRequired = false;

		#region Properties
		
		public Commands Command
		{
			set
			{
				this.command = value;

				switch (value)
				{
					case Commands.Decrypt:
						this.recipientRequired = false;
						this.passphraseRequired = false;
						this.armor = false;
						break;

					case Commands.Encrypt:
						this.recipientRequired = true;
						break;

					default:
						break;
				}
			}

			get { return(this.command); }
		}

		public bool Armor
		{
			set	{ this.armor = value; }
			get { return(this.armor); }
		}

		public string Recipient
		{
			set	{ this.recipient = value; }
			get { return(this.recipient); }
		}

		public string Originator
		{
			set	{ this.originator = value; }
			get { return(this.originator); }
		}

		public bool Yes
		{
			set	{ this.yes = value;	}
			get { return(this.yes); }
		}

		public bool Batch
		{
			set	{ this.batch = value; }
			get { return(this.batch); }
		}

		public string Passphrase
		{
			set	{ this.passphrase = value; }
			get { return(this.passphrase); }
		}

		public string HomeDirectory
		{
			set	{ this.homeDir = value; }
			get { return(this.homeDir); }
		}

		public VerboseLevel Verbose
		{
			set	{ this.verbose = value;	}
			get	{ return(this.verbose);	}
		}

		public string InputFile
		{
			set	{ this.inputFile = value; }
			get { return(this.inputFile); }
		}

		public string OutputFile
		{
			set	{ this.outputFile = value; }
			get { return(this.outputFile); }
		}

		#endregion

		public GnuPGCommand()
		{
		}

		public override string ToString()
		{
			return ConstructCommandLine();
		}

		#region Helpers

		private string ConstructCommandLine()
		{
			StringBuilder commandLine = new StringBuilder("", 255);

			if ((this.homeDir != null) && (this.homeDir != ""))
			{
				commandLine.Append("--homedir \"");
				commandLine.Append(this.homeDir);
				commandLine.Append("\" ");
			}

			if (this.yes)
			{
				commandLine.Append("--yes ");
			}

			if (this.batch)
			{
				commandLine.Append("--batch ");
				commandLine.Append("--passphrase-fd 0 "); // get passphrase from stdin (not console device, which is default)
			}

			switch (this.command)
			{
				case Commands.Encrypt:
					commandLine.Append("-e ");
					break;

				case Commands.Decrypt:
					commandLine.Append("-d ");
					break;
			}

			if (this.armor)
			{
				commandLine.Append("-a ");
			}

			if (this.recipient != "")
			{
				string[] recipients = recipient.Split(new Char[] {';'});

				foreach (string s in recipients) 
				{
					System.Diagnostics.Debug.WriteLine(s);
					if (s.Trim() != "")
					{
						commandLine.Append("-r \"");
						commandLine.Append(s);
						commandLine.Append("\" ");
					}
				}
			}
			else
			{
				// If you encrypt, you NEED a recipient!
				if (this.recipientRequired)
				{
					throw new GnuPGException("GPGNET: Missing 'recipient' parameter: cannot encrypt without a recipient");
				}
			}

			if (this.originator != "")
			{
				commandLine.Append("--default-key \"");
				commandLine.Append(this.originator);
				commandLine.Append("\" ");
			}

			if (this.passphrase == "")
			{
				if (this.passphraseRequired)
				{
					throw new GnuPGException("GPGNET: Missing 'passphrase' parameter: cannot sign without a passphrase");
				}
			}

			switch (this.verbose)
			{
				case VerboseLevel.NoVerbose:
					commandLine.Append("--no-verbose ");
					break;

				case VerboseLevel.Verbose:
					commandLine.Append("-v ");
					break;

				case VerboseLevel.VeryVerbose:
					commandLine.Append("-v -v ");
					break;

				case VerboseLevel.ExtremelyVerbose:
					commandLine.Append("-v -v -v ");
					break;
			}

			if (this.outputFile != "")
			{
				commandLine.Append("-o \"");
				commandLine.Append(this.outputFile);
				commandLine.Append("\" ");
			}

			// must always be the last argument
			if (this.inputFile != "")
			{
				commandLine.Append("\"");
				commandLine.Append(this.inputFile);
				commandLine.Append("\"");
			}

			return(commandLine.ToString());
		}

		#endregion
	}
}
