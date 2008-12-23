using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Security
{
    public interface IRoleProvider
    {
        string[] GetRolesForUser(string username);
    }
}
