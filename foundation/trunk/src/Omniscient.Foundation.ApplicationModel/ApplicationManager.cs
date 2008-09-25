using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.Data;
using Omniscient.Foundation.ServiceModel;
using Omniscient.Foundation.ApplicationModel.Configuration;
using Omniscient.Foundation.ApplicationModel.Presentation;
using Omniscient.Foundation.ApplicationModel.Modularity;

namespace Omniscient.Foundation.ApplicationModel
{
    /// <summary>
    /// Abstracts the concept of an application.  Serves as the bootstrapper of any application
    /// that uses the Foundation.
    /// </summary>
    public class ApplicationManager
    {
        private static ApplicationManager _instance;
        private ApplicationConfiguration _config;
        private bool _started;

        static ApplicationManager()
        {
            _instance = new ApplicationManager();
        }

        private ApplicationManager()
        {
        }

        /// <summary>
        /// Gets the current Application object.  Each application using foundation has a single Current Application.
        /// </summary>
        public static ApplicationManager Current
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
        /// Gets or sets the Presentation Controller to be used.  Generally an instance of <see cref="PresentationController"/>.
        /// </summary>
        public IPresentationController PresentationController
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an <see cref="IObjectContainer"/> instance.  Defaults to <see cref="ObjectContainer"/>.
        /// </summary>
        public IObjectContainer ObjectContainer
        {
            get;
            set;
        }

        public IShell Shell
        {
            get;
            set;
        }

        public IExtensionPortManager ExtensionPortManager
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
            _config = System.Configuration.ConfigurationManager.GetSection("foundation.application") as ApplicationConfiguration;

            if (ServiceContainer == null) ServiceContainer = new ServiceContainer();
            if (PresentationController == null) PresentationController = new PresentationController();
            if (ObjectContainer == null) ObjectContainer = new ObjectContainer();
            if (ExtensionPortManager == null) ExtensionPortManager = new ExtensionPortManager(ObjectContainer);

            if (_config != null)
            {
                ConfigManager.ConfigureServices(ServiceContainer, _config);
                ConfigManager.ConfigureModules(ObjectContainer, _config);
            }

            //start services
            foreach (IService service in ServiceContainer.AllServices)
            {
                IStartable startable;
                startable = service as IStartable;
                if (startable != null) startable.Start();
            }

            //Display the Shell
            if (Shell != null) Shell.Show();

            //Start modules
            foreach (object obj in ObjectContainer.AllObjects)
            {
                IModule module = obj as IModule;
                if (module != null)
                {
                    module.PresentationController = PresentationController;
                    IStartable start;
                    start = module as IStartable;
                    if (start != null) start.Start();
                }
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
            foreach (IService service in ServiceContainer.AllServices.Reverse<IService>())
            {
                IStartable startable;
                startable = service as IStartable;
                if (startable != null) startable.Stop();
            }

            _started = false;
        }
    }
}
