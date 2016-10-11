using System;

namespace Microsoft.Utilities.Cryptography.GnuPG
{
	/// <summary>
	/// Summary description for GnuPGException.
	/// </summary>
	public class GnuPGException: Exception
	{
		public GnuPGException(string message): base(message)
		{
		}
	}
}
