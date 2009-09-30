using System;
using System.Configuration;
using System.ServiceModel;
using Omniscient.Foundation.ApplicationModel;
using Omniscient.Foundation.ServiceModel;
using Omniscient.Foundation.Security;
using System.Security.Permissions;
using System.Threading;
using System.Runtime.InteropServices;
using System.Xml;

namespace Omniscient.Foundation.Contrib.Communication
{
    public class SecureChannelFactoryService<TContract>: ServiceBase<TContract>, IConfigurable
    {
        private string _endpoint;

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override TContract GetImplementation()
        {
            ChannelFactory<TContract> factory;
            SecurePrincipal principal;

            principal = (SecurePrincipal)Thread.CurrentPrincipal;
            factory = new ChannelFactory<TContract>(_endpoint);

            string password = string.Empty;
            if (principal.Identity.Password != null)
            {
                IntPtr ptr = Marshal.SecureStringToBSTR(principal.Identity.Password);
                password = Marshal.PtrToStringBSTR(ptr);
                Marshal.ZeroFreeBSTR(ptr);
            }

            factory.Credentials.UserName.UserName = principal.Identity.Name;
            factory.Credentials.UserName.Password = password;
            return factory.CreateChannel();
            
        }

        #region IConfigurable Members

        public void Configure(XmlElement config)
        {
            //todo: add a complex type to contrib's schema file. 
            if (config == null) throw new ArgumentNullException("config");

            XmlNode endpointNode = config;
            if (config.Name != "endpointName")
            {
                endpointNode = config.SelectSingleNode(".//endpointName");
                if (endpointNode == null)
                    throw new ConfigurationErrorsException("Can't find element endpointName under configuration of service SecureChannelFactoryService.");
            }
            _endpoint = endpointNode.InnerText;
        }

        #endregion
    }
}
