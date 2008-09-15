﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// Defines a "startable" service.  Those services are informed of the application startup and shutdown.
    /// </summary>
    /// <typeparam name="TContract">The type of contract implemented by this service.</typeparam>
    public interface IStartableService<TContract>: IService<TContract>
    {
        /// <summary>
        /// Called when the application starts.  All services are loaded when this method is called.  Services are started
        /// in the order they are defined in the configuration.
        /// </summary>
        void Start();
        
        /// <summary>
        /// Called when the application shuts down.
        /// </summary>
        void Stop();
    }
}
