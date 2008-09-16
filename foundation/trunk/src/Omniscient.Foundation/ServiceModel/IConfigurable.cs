using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// Defines a configurable service.  Implement this interface when you need your service to receive configuration.
    /// </summary>
    /// <remarks>
    /// See the configuration schema for more details.
    /// </remarks>
    public interface IConfigurable
    {
        /// <summary>
        /// Called at configuration time.  If the service is defined in the configuration file, that method is automatically called.
        /// </summary>
        /// <param name="config">The deserialized configuration.</param>
        /// <remarks>
        /// See the configuration schema for more details.
        /// </remarks>
        void Configure(object config);
    }
}
