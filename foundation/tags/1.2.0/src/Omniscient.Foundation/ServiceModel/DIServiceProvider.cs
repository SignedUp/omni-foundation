using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Core;
using Ninject.Core.Binding;
using Ninject.Core.Creation.Providers;

namespace Omniscient.Foundation.ServiceModel
{
    /// <summary>
    /// Service provider that registers services against Ninject's dependency injection container.  Once registered, the service
    /// is available from the Kernel, as well as from the service provider itself.  In all cases, a call to 
    /// <see cref="IService.GetImplementation"/> will be made each time the service is accessed.
    /// </summary>
    /// <example>
    /// Let's have a validation service: IValidator and IValidatorService (derives from IService).  Let's also have a class named Validator that implements
    /// IValidator and IValidatorService, and returns a reference to itself when GetImplementation is invoked.
    /// You register that service with a call to DIServiceProvider.RegisterService&lt;IValidator&gt;(new Validator());
    /// 
    /// Once it's registered, you can access the service like this:
    /// IValidator v = serviceProvider.GetService&lt;IValidator&gt;();
    /// 
    /// or like that:
    /// IValidator v = kernel.Get&lt;IValidator&gt;();
    /// 
    /// In all cases, the framework will call GetImplementation on the service to return an instance of IValidator.
    /// </example>
    public class DIServiceProvider: ServiceProvider
    {
        private IKernel _kernel;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="kernel">a Ninject kernel</param>
        public DIServiceProvider(IKernel kernel)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");
            Kernel = kernel;
        }

        /// <summary>
        /// Gets or sets the Ninject kernel used to register regular services as Ninject's services, so that they're part of
        /// the DI mechanism.
        /// </summary>
        public IKernel Kernel
        {
            get { return _kernel; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _kernel = value;
            }
        }

        /// <summary>
        /// Adds a service to the provider, and adds a binding to the Kernel, allowing the service to be part of the dependency
        /// injection machanism.
        /// </summary>
        /// <param name="contractType">Service's contract type.</param>
        /// <param name="service">Service implementation (must support contract's type)</param>
        public override void RegisterService(Type contractType, IService service)
        {
            base.RegisterService(contractType, service);
            
            //Create a special binding for services
            IBinding binding;
            binding = new StandardBinding(Kernel, contractType);
            binding.Provider = new ServiceProvider(contractType, service);
            Kernel.AddBinding(binding);
        }

       
        private class ServiceProvider : ProviderBase
        {
            private IService _service;

            public ServiceProvider(Type contractType, IService service): base(contractType)
            {
                _service = service;    
            }

            public override object Create(Ninject.Core.Activation.IContext context)
            {
                return _service.GetImplementation();
            }

            protected override Type DoGetImplementationType(Ninject.Core.Activation.IContext context)
            {
                return _service.ImplementationType;
            }
        }

    }

}
