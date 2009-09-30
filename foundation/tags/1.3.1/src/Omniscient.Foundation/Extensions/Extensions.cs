using System.Text;
namespace Omniscient.Foundation
{
    /// <summary>
    /// Defines extension methods on the String class.
    /// </summary>
    public static class Extensions
    {
        #region Extensions on type String
        
        public static string FormatEx(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        
        #endregion
    }
}
