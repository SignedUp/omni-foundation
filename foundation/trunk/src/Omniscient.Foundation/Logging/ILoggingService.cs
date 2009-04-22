using System;

namespace Omniscient.Foundation.Logging
{
    ///<summary>
    ///</summary>
    [Obsolete("Use the Kernel for dependency injection instead of the service.")]
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
