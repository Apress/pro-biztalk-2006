using System;

namespace Microsoft.Samples.PipelineUtilities
{
	/// <summary>
	/// Summary description for PropertyBagReadWrite.
	/// </summary>
	public  abstract class PropertyBagReadWrite
	{
		/// <summary>
		/// Reads property value from property bag.
		/// </summary>
		/// <param name="pb">Property bag.</param>
		/// <param name="propName">Name of property.</param>
		/// <returns>Value of the property.</returns>
		public static object ReadPropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName)
		{
			object val = null;

			try
			{
				pb.Read(propName, out val, 0);
			}
			catch(ArgumentException)
			{
				return val;
			}
			catch(Exception ex)
			{
				throw new ApplicationException(ex.Message);
			}

			return val;
		}

		/// <summary>
		/// Writes property values into a property bag.
		/// </summary>
		/// <param name="pb">Property bag.</param>
		/// <param name="propName">Name of property.</param>
		/// <param name="val">Value of property.</param>
		public static void WritePropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName, object val)
		{
			try
			{
				pb.Write(propName, ref val);
			}
			catch(Exception ex)
			{
				throw new ApplicationException(ex.Message);
			}
		}

	}
}
