using System;
using System.Configuration;
using System.Linq;
using Ninject.Core;
using Omniscient.Foundation.ApplicationModel.Configuration;
using Omniscient.Foundation.ApplicationModel.Modularity;
using Omniscient.Foundation.ApplicationModel.Presentation;
using Omniscient.Foundation.Commanding;
using Omniscient.Foundation.Logging;
using Omniscient.Foundation.ServiceModel;

namespace Omniscient.Foundation.ApplicationModel
{
    /// <summary>
    /// Abstracts the concept of an application.  Serves as the bootstrapper of any application
    /// that uses the Foundation.
    /// </summary>
    public class ApplicationManager
    {
        private ApplicationConfiguration _config;
        private bool _started;
        
        private ServiceModel.IServiceProvider _serviceProvider;
        private IPresentationController _presentationController;
        private IShell _shell;
        private IKernel _kernel;
        private IApplicationModuleManager _applicationModuleManager;
        private ICommandStore _commandStore;
        private ILogger _logger;

        static ApplicationManager()
        {
            ApplicationContext = new StaticApplicationContext();
        }

        protected internal ApplicationManager()
        {
            TextLogWriter writer = new TextLogWriter(new System.IO.MemoryStream());
            writer.Level = LogLevel.Fatal;
            writer.AutoflushLevel = LogLevel.Fatal;
            writer.IsEnabled = false;
            Logger = new StandardLogger(writer);
        }

        /// <summary>
        /// Gets the current Application object.  Each application using foundation has a single Current Application.
        /// </summary>
        public static ApplicationManager Current
        {
            get { return ApplicationContext.Current; }
        }

        public static IApplicationContext ApplicationContext
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether <see cref="Current"/> has been started by a call to <see cref="StartApplication()"/>.
        /// </summary>
        public virtual bool IsStarted
        {
            get { return _started; }
        }

        /// <summary>
        /// Gets or sets the <see cref="ServiceModel.IServiceProvider"/>.  If not set, then it will automatically be created with an instance
        /// of <see cref="ServiceProvider"/>.
        /// Once the application is started with <see cref="StartApplication()"/>, it is not possible to set this to a new value.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when setting to a value after the application is started.</exception>
        public ServiceModel.IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }

            set
            {
                if (IsStarted) throw new InvalidOperationException("Invalid call after application is started.");
                _serviceProvider = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="IPresentationController"/>.  If not set, then it will automatically be created with an 
        /// instance of <see cref="PresentationController"/>.
        /// Once the application is started with <see cref="StartApplication()"/>, it is not possible to set this to a new value.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when setting to a value after the application is started.</exception>
        public IPresentationController PresentationController
        {
            get
            {
                return _presentationController;
            }

            set
            {
                if (IsStarted) throw new InvalidOperationException("Invalid call after application is started.");
                _presentationController = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="IShell"/>, which is the main UI fabric of a desktop application.  If not set, then
        /// the user will have to manually create presenters (<see cref="IPresenter"/>) and view controllers
        /// (<see cref="IViewController"/>), and bind those to the UI.
        /// Once the application is started with <see cref="StartApplication()"/>, it is not possible to set this to a new value.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when setting to a value after the application is started.</exception>
        public IShell Shell
        {
            get
            {
                return _shell;
            }

            set 
            {
                if (IsStarted) throw new InvalidOperationException("Invalid call after application is started.");
                _shell = value;
            }
        }

        /// <summary>
        /// Gets or sets Ninject's Kernel.  If not set, then this application will run without dependency injection.
        /// Once the application is started with <see cref="StartApplication()"/>, it is not possible to set this to a new value.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when setting to a value after the application is started.</exception>
        public IKernel Kernel
        {
            get
            {
                return _kernel;
            }

            set
            {
                if (IsStarted) throw new InvalidOperationException("Invalid call after application is started.");
                _kernel = value;
            }
        }

        /// <summary>
        /// Gets or sets an <see cref="ICommandStore"/>.  This is a shorcut to a stateful service that stores ICommand objects.
        /// </summary>
        [Obsolete("There shouldn't be any IXXXStore, where XXX is a concrete type.  The store pattern should be implemented correctly instead.  This property will be removed in releases following 1.3.0.")]
        public ICommandStore CommandStore
        {
            get
            {
                return _commandStore;
            }

            set
            {
                if (IsStarted) throw new InvalidOperationException("Invalid call after application is started.");
                _commandStore = value;
            }
        }

        /// <summary>
        /// Gets or sets the application logger.  A logger consists of one or more <see cref="ILogWriter"/> objects,
        /// sequentially called during a logging operation.  The default logger comes with one in-memory log writer.
        /// </summary>
        public ILogger Logger
        {
            get
            {
                return _logger;
            }

            set
            {
                if (IsStarted) throw new InvalidOperationException("Invalid call after application is started.");
                _logger = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="IApplicationModuleManager"/>.  If not set, then it will automatically be created with
        /// an instance of <see cref="ApplicationModuleManager"/>.  
        /// Once the application is started with <see cref="StartApplication()"/>, it is not possible to set this to a new value.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when setting to a value after the application is started.</exception>
        public IApplicationModuleManager ApplicationModuleManager
        {
            get
            {
                return _applicationModuleManager;
            }

            set
            {
                if (IsStarted) throw new InvalidOperationException("Invalid call after application is started.");
                _applicationModuleManager = value;
            }
        }

        /// <summary>
        /// Gets the deserialized configuration.  
        /// In the configuration file, the config section must
        /// be called "foundation.application", and must be in the namespace 
        /// "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd".  See the xsd schema file ApplicationConfiguration.xsd
        /// for the complete schema.
        /// </summary>
        public ApplicationConfiguration Config
        {
            get 
            {
                if (!IsStarted) throw new InvalidOperationException("Please start current Application first.");
                return _config; 
            }
        }

        /// <summary>
        /// Starts the application.  That method should be one of the first being called when the program starts.  The method
        /// loads the configuration from the AppDomain configuration file.
        /// </summary>
        /// <remarks>
        /// That method will look for application configuration.  In the configuration file, the config section must
        /// be called "foundation.application", and must be in the namespace 
        /// "http://schemas.omniscient.ca/foundation/applicationConfiguration.xsd".  The section handler is 
        /// <see cref="ApplicationConfigurationSectionHandler"/>.
        /// </remarks>
        public void StartApplication()
        {
            // Get configuration from default config file.
            var config = ConfigurationManager.GetSection("foundation.application") as ApplicationConfiguration;
            StartApplication(config);
        }

        /// <summary>
        /// Starts the application.  That method should be one of the first being called when the program starts. The method
        /// accepts an existing configuration object.  See <see cref="StartApplication()"/> for default configuration.
        /// </summary>
        /// <param name="config">Loaded application object.</param>
        public void StartApplication(ApplicationConfiguration config)
        {
            Logger.Info("Starting application...");
            _config = config;

            InitializeComponents();

            // Load services and modules from the config file.
            Logger.Info("Loading services and modules from config.");
            if (_config != null)
            {
                ConfigManager.LoadAndConfigureServices(ServiceProvider, _config);
                ConfigManager.LoadModules(ApplicationModuleManager, _config);
            }

            // start services
            Logger.Info("Starting services.");
            foreach (IService service in ServiceProvider.AllServices)
            {
                IStartable startable;
                startable = service as IStartable;
                if (startable != null) startable.Start();
            }

            // Display the Shell, if any.
            if (Shell != null)
            {
                Logger.Info("Displaying the Shell.");
                foreach (IViewController ctrl in Shell.CreateViewControllers())
                {
                    PresentationController.RegisterViewController(ctrl);
                }
                foreach (IPresenter presenter in Shell.CreatePresenters())
                {
                    PresentationController.RegisterPresenter(presenter);
                }
                Shell.Show();
            }
            
            // Activate modules
            Logger.Info("Activating modules.");
            ApplicationModuleManager.ActivateAll();

            _started = true;
            Logger.Info("Application started.");
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
            // Deactivate and unload all modules
            ApplicationModuleManager.DeactivateAll();
            ApplicationModuleManager.UnloadAll();

            // Stop all services
            foreach (IService service in ServiceProvider.AllServices.Reverse())
            {
                IStartable startable;
                startable = service as IStartable;
                if (startable != null) startable.Stop();
            }

            // Clean containers
            // todo: something else to clean?
            ServiceProvider.Clear();

            _started = false;
        }

        protected virtual void InitializeComponents()
        {
            if (ServiceProvider == null)
            {
                // if we have a kernel, then let's create a "depency-injection service provider".
                if (Kernel != null) ServiceProvider = new DIServiceProvider(Kernel);

                // otherwise, create a regular, plain service provider
                else ServiceProvider = new ServiceProvider();
            }

            if (PresentationController == null) PresentationController = new PresentationController();
            if (ApplicationModuleManager == null) ApplicationModuleManager = new ApplicationModuleManager();

            ApplicationModuleManager.PresentationController = PresentationController;

            // if the Kernel is present, then link both module managers (from Foundation and from Ninject).
            if (Kernel != null) ApplicationModuleManager.ModuleManager = Kernel.Components.ModuleManager;
        }
    }
}
