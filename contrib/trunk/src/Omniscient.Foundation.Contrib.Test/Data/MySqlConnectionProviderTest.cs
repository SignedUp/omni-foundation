using System.Data;
using Moq;
using NUnit.Framework;
using Omniscient.Foundation.ApplicationModel;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.Contrib.Data
{
    [TestFixture]
    public class MySqlConnectionProviderTest
    {
        private IConnectionProvider provider;
        private string connectionStringName = "MySqlConnection";

        [SetUp]
        public void SetUp()
        {
            Mock<IDbConnection> connectionMock = new Mock<IDbConnection>();

            Mock<IConnectionProvider> providerMock;
            providerMock = new Mock<IConnectionProvider>();
            providerMock.Expect(x => x.CreateConnection()).Returns(connectionMock.Object);

            provider = providerMock.Object;
        }

        [Test]
        public void TestInstance()
        {
            provider = new MsSqlConnectionProvider(connectionStringName);
        }

        [Test]
        public void TestCreateConnection()
        {
            provider = new MsSqlConnectionProvider(connectionStringName);

            object actual = provider.CreateConnection();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(typeof(IDbConnection), actual);
        }
    }
}
