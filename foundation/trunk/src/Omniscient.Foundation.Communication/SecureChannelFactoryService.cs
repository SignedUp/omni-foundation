using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Omniscient.Foundation.ServiceModel;
using System.ServiceModel;
using Omniscient.Foundation.Security;
using System.Security.Permissions;
using System.Threading;
using System.Runtime.InteropServices;
using System.Xml;
using System.Configuration;

namespace Omniscient.Foundation.Communication
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

            IntPtr ptr = Marshal.SecureStringToBSTR(principal.Identity.Password);
            string password = Marshal.PtrToStringBSTR(ptr);
            Marshal.ZeroFreeBSTR(ptr);

            factory.Credentials.UserName.UserName = principal.Identity.Name;
            factory.Credentials.UserName.Password = password;
            return factory.CreateChannel();
            
        }

        #region IConfigurable Members

        public void Configure(System.Xml.XmlElement config)
        {
            //todo: add a complex type to contrib's schema file. 
            XmlNode endpointNode;
            endpointNode = config.SelectSingleNode(".//endpointName");
            if (endpointNode == null)
                throw new ConfigurationErrorsException("Can't find element endpointName under configuration of service SecureChannelFactoryService.");
            _endpoint = endpointNode.Value;
        }

        #endregion
    }
}
