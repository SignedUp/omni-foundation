using System;

namespace Omniscient.Foundation.ApplicationModel.Configuration
{
    /// <summary>
    /// Defines exceptions that occur at configuration-time.
    /// </summary>
    public class ConfigurationErrorsException : SystemException
    {
        /// <summary>
        /// Initializes a new instance of the ConfigurationErrorsException class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public ConfigurationErrorsException(string message) 
            : base(message)
        {
        }
    }
}
