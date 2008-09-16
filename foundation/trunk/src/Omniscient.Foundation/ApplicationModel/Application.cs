﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.ServiceModel;

namespace Omniscient.Foundation.ApplicationModel
{
    /// <summary>
    /// Base class that abstracts the concept of an application.  Serves as the bootstrapper of any application
    /// that uses the Foundation.
    /// </summary>
    public class Application
    {
        private static Application _instance;
        private ApplicationConfiguration _config;
        private bool _started;

        static Application()
        {
            _instance = new Application();
        }

        private Application()
        {
        }

        /// <summary>
        /// Gets the current Application object.  Each application using foundation has a single Current Application.
        /// </summary>
        public static Application Current
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets a value indicating whether <see cref="Current"/> has been started by a call to <see cref="StartApplication()"/>.
        /// </summary>
        public virtual bool IsStarted
        {
            get { return _started; }
        }

        /// <summary>
        /// Gets or sets the service container to be used.  Generally an instance of <see cref="ServiceContainer"/>.
        /// </summary>
        public IServiceContainer ServiceContainer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the deserialized configuration.
        /// </summary>
        public ApplicationConfiguration Config
        {
            get 
            {
                if (!this.IsStarted) throw new InvalidOperationException("Please start current Application first.");
                return _config; 
            }
        }

        /// <summary>
        /// Starts the application.  That method should be one of the first being called when the program starts.
        /// </summary>
        /// <remarks>
        /// That method will look for application configuration.  In the configuration file, the config section must
        /// be called "foundation.application", and must be in the namespace 
        /// "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd".  See the xsd schema file ApplicationConfiguration.xsd
        /// for the complete schema.
        /// </remarks>
        public void StartApplication()
        {
            //Get configuration from default config file.
            ApplicationConfiguration config;
            config = System.Configuration.ConfigurationManager.GetSection("foundation.application") as ApplicationConfiguration;
            StartApplication(config);
        }

        /// <summary>
        /// Starts the application, providing it with a configuration.  
        /// That method should be one of the first being called when the program starts.
        /// </summary>
        /// <remarks>
        /// All services that implement <see cref="IStartable"/> are started here.
        /// </remarks>
        public void StartApplication(ApplicationConfiguration config)
        {
            if (ServiceContainer == null) ServiceContainer = new ServiceContainer();

            _config = config;
            if (_config.ServicesConfiguration != null)
            {
                ServiceContainer.Configure(config);
            }

            foreach (object service in ServiceContainer.AllServices)
            {
                IStartable startable;
                startable = service as IStartable;
                if (startable != null) startable.Start();
            }

            _started = true;
        }

        /// <summary>
        /// Closes the application.  Generally, that should lead to the process being terminated.
        /// </summary>
        /// <remarks>
        /// All services that implement <see cref="IStartable"/> are stopped here, in reverse order that they 
        /// have been started.
        /// </remarks>
        public virtual void CloseApplication()
        {
            foreach (object service in ServiceContainer.AllServices.Reverse<object>())
            {
                IStartable startable;
                startable = service as IStartable;
                if (startable != null) startable.Stop();
            }

            _started = false;
        }
    }
}
