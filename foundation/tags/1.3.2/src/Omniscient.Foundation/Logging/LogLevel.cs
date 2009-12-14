namespace Omniscient.Foundation.Logging
{
    /// <summary>
    /// Log level used to categorize log entries.
    /// </summary>
    public enum LogLevel
    { 
        /// <summary>
        /// This level is commonly used to trace the program for debugging purposes.  Generally not used in production environments.
        /// </summary>
        Debug, 
        
        /// <summary>
        /// This level is often use to log information in the normal course of the program
        /// </summary>
        Info, 
        
        /// <summary>
        /// This level is generally used to log errors from which the program can recover (normally used in catch statements).
        /// </summary>
        Error, 
        
        /// <summary>
        /// This level is used to log errors from which the program cannot recover or that leave inconsistent state.
        /// </summary>
        Fatal

    }
}