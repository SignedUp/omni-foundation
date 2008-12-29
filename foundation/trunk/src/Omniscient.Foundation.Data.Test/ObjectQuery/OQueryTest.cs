using NUnit.Framework;

namespace Omniscient.Foundation.Data.ObjectQuery
{
    [TestFixture]
    public class OQueryTest
    {
        [Test]
        public void TestSimpleQuery()
        {
            OQuery<EntityMock> query = new OQuery<EntityMock>();
        }
    }
}
