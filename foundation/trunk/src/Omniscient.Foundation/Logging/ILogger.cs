namespace Omniscient.Foundation.Logging
{
    ///<summary>
    ///</summary>
    public interface ILogger
    {
        ///<summary>
        ///</summary>
        ///<param name="message"></param>
        void Debug(object message);
        ///<summary>
        ///</summary>
        ///<param name="message"></param>
        void Info(object message);
        ///<summary>
        ///</summary>
        ///<param name="message"></param>
        void Error(object message);
        ///<summary>
        ///</summary>
        ///<param name="message"></param>
        void Fatal(object message);
    }
}
