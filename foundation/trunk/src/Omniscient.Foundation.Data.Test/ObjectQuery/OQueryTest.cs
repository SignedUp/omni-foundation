using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Omniscient.Foundation.Data.ObjectQuery
{
    [TestFixture()]
    public class OQueryTest
    {
        [Test()]
        public void TestSimpleQuery()
        {
            OQuery<EntityMock> query;
            query = new OQuery<EntityMock>();
        }
    }
}
