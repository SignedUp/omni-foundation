using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Omniscient.Foundation.Security
{
    public class SecurePrincipal : GenericPrincipal
    {
        public SecurePrincipal(IIdentity identity, string[] roles) : base(identity, roles) { }
    }
}
