using System;

namespace Omniscient.Foundation.Logging
{
    ///<summary>
    ///</summary>
    public interface ILoggingService
    {
        ///<summary>
        ///</summary>
        ///<param name="name"></param>
        ///<returns></returns>
        ILogger GetLogger(string name);
        ///<summary>
        ///</summary>
        ///<param name="logSource"></param>
        ///<returns></returns>
        ILogger GetLogger(Type logSource);
    }
}
