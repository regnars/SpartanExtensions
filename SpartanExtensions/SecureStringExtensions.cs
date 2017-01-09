using System;
using System.Runtime.InteropServices;
using System.Security;
namespace SpartanExtensions
{
    public static class SecureStringExtensions
    {
        /// <summary>
        /// Transforms SecureString into string. Sorry for the naming, but the more intuitive "ToString" couldn't be used as it is already used by the .NET framework.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToPlainString(this SecureString value)
        {
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
