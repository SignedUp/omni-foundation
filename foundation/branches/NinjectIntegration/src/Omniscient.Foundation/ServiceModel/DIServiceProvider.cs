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
    /// Service provider that registers services against Ninject's dependency injection container.
    /// </summary>
    public class DIServiceProvider: ServiceProvider
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="kernel">a Ninject kernel</param>
        public DIServiceProvider(IKernel kernel)
        {
            Kernel = kernel;
        }

        /// <summary>
        /// Gets or sets the Ninject kernel used to register regular services as Ninject's services, so that they're part of
        /// the DI mechanism.
        /// </summary>
        public IKernel Kernel
        {
            get;
            set;
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
