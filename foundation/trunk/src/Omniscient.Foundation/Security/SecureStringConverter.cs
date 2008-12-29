using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Omniscient.Foundation.Security
{
    ///<summary>
    ///</summary>
    public static class SecureStringConverter
    {
        ///<summary>
        ///</summary>
        ///<param name="secureString"></param>
        ///<returns></returns>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static string FromSecureString(SecureString secureString)
        {
            string result;

            IntPtr pointer = Marshal.SecureStringToBSTR(secureString);
            result = Marshal.PtrToStringBSTR(pointer);
            Marshal.ZeroFreeBSTR(pointer);

            return result;
        }

        ///<summary>
        ///</summary>
        ///<param name="regularString"></param>
        ///<returns></returns>
        public static SecureString ToSecureString(string regularString)
        {
            SecureString result = new SecureString();

            foreach (char c in regularString)
            {
                result.AppendChar(c);
            }

            return result;
        }
    }
}
