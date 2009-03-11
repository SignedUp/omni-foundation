using System.Data;
using Moq;
using NUnit.Framework;
using Omniscient.Foundation.Data;

namespace Omniscient.Foundation.Contrib.Data
{
    [TestFixture]
    public class MsSqlConnectionProviderTest
    {
        private IConnectionProvider provider;
        private string connectionString = "Data Source=myServerAddress;Initial Catalog=myDataBase;Integrated Security=SSPI;";

        [SetUp]
        public void SetUp()
        {
            Mock<IDbConnection> connectionMock = new Mock<IDbConnection>();
            connectionMock.Expect(x => x.ConnectionString).Returns(connectionString);
            connectionMock.Expect(x => x.Open());
            connectionMock.Expect(x => x.State).Returns(ConnectionState.Closed);

            Mock<IConnectionProvider> providerMock;
            providerMock = new Mock<IConnectionProvider>();
            providerMock.Expect(x => x.CreateConnection()).Returns((IDbConnection) connectionMock);

            provider = (IConnectionProvider) providerMock;
        }

        [Test]
        public void TestInstance()
        {
            provider = new MsSqlConnectionProvider(connectionString);
        }

        [Test]
        public void TestCreateConnection()
        {
            provider = new MsSqlConnectionProvider(connectionString);

            object actual = provider.CreateConnection();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(typeof (IDbConnection), actual);
        }
    }
}