using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Security
{
    public interface IUsernamePasswordValidator
    {
        void Validate(string username, string password);
    }
}
