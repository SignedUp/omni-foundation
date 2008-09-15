using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Data
{
    public class EntityMock: EntityBase
    {
        public EntityMock() : base(EntityStatus.New) { }
        public EntityMock(Guid id) : base(id) { }

        [EntityProperty(EntityPropertyType.Value)]
        public string Name { get; set; }

        [EntityProperty(EntityPropertyType.Value)]
        public int Age { get; set; }

        public int total { get; set; }
    }
}
