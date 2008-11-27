using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Security.Principal;

namespace Omniscient.Foundation.Security
{
    [TestFixture()]
    public class SecurePrincipalTest
    {
        [Test()]
        public void TestCreate()
        {
            SecurePrincipal u = new SecurePrincipal(new GenericIdentity("test"), new string[]{});
        }

        [Test()]
        public void TestIsInRole()
        {
            SecurePrincipal u = new SecurePrincipal(new GenericIdentity("test"), new string[] { "role1", "role2" });
            Assert.IsTrue(u.IsInRole("role1"));
            Assert.IsTrue(u.IsInRole("role2"));
            Assert.IsFalse(u.IsInRole("role3"));
        }
    }
}
