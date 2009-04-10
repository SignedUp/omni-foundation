using System;
using System.Configuration;
using System.Data;
using Moq;
using NUnit.Framework;
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
            connectionMock.Expect(x => x.ConnectionString).Returns("Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;");
            connectionMock.Expect(x => x.State).Returns(ConnectionState.Closed);

            Mock<IConnectionProvider> providerMock;
            providerMock = new Mock<IConnectionProvider>();
            providerMock.Expect(x => x.CreateConnection()).Returns(connectionMock.Object);

            provider = providerMock.Object;
        }

        [Test]
        public void TestInstanceSuccessWithValidConnectionString()
        {
            provider = new MySqlConnectionProvider(connectionStringName);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInstanceFailureWithEmptyConnectionString()
        {
            provider = new MySqlConnectionProvider(string.Empty);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void TestInstanceFailureWithInvalidConnectionString()
        {
            provider = new MySqlConnectionProvider("TestString");
        }

        [Test]
        public void TestCreateConnection()
        {
            provider = new MySqlConnectionProvider(connectionStringName);

            object actual = provider.CreateConnection();

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(typeof(IDbConnection), actual);

            Assert.IsFalse(string.IsNullOrEmpty(((IDbConnection) actual).ConnectionString));

            Assert.AreEqual(ConnectionState.Closed, ((IDbConnection) actual).State);
        }
    }
}
