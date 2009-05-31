using System.Linq;
using NUnit.Framework;

namespace Omniscient.Foundation.Security
{
    [TestFixture]
    public class SecurePrincipalTest
    {
        [Test]
        public void TestCreate()
        {
            SecurePrincipal u = new SecurePrincipal(new SecureIdentity("test", null), new string[]{});
        }

        [Test]
        public void TestIsInRole()
        {
            SecurePrincipal u = new SecurePrincipal(new SecureIdentity("test", null), new[] { "role1", "role2" });
            Assert.IsTrue(u.IsInRole("role1"));
            Assert.IsTrue(u.IsInRole("role2"));
            Assert.IsFalse(u.IsInRole("role3"));
        }

        [Test]
        public void TestAnonymous()
        {
            SecurePrincipal u = new SecurePrincipal();
            Assert.IsNotNull(u.Identity);
            Assert.IsNotNull(u.AllRoles);
            Assert.AreEqual(0, u.AllRoles.Count());
            Assert.IsFalse(u.IsInRole("test"));
            Assert.IsFalse(u.Identity.IsAuthenticated);
            Assert.AreEqual(string.Empty, u.Identity.AuthenticationType);
            Assert.AreEqual(string.Empty, u.Identity.Name);
        }

        [Test]
        public void TestPromoteIdentity1()
        {
            SecurePrincipal u = new SecurePrincipal();
            u.Identity.Promote("dave", null);
            Assert.AreEqual("dave", u.Identity.Name);
            Assert.AreEqual(string.Empty, u.Identity.AuthenticationType);
            Assert.IsTrue(u.Identity.IsAuthenticated);
        }

        [Test]
        public void TestPromoteIdentity2()
        {
            SecurePrincipal u = new SecurePrincipal();
            u.Identity.Promote("dave", null, "test");
            Assert.AreEqual("dave", u.Identity.Name);
            Assert.AreEqual("test", u.Identity.AuthenticationType);
            Assert.IsTrue(u.Identity.IsAuthenticated);
        }

        [Test]
        public void TestPromotePrincipal()
        {
            SecurePrincipal u = new SecurePrincipal();
            u.Promote(new string[] {"r1", "r2"});
            Assert.AreEqual(2, u.AllRoles.Count());
            Assert.IsTrue(u.IsInRole("r1"));
            Assert.IsTrue(u.IsInRole("r2"));
        }
    }
}
