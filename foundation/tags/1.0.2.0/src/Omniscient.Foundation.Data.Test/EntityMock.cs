using System;

namespace Omniscient.Foundation.Data
{
    public class EntityMock: EntityBase
    {
        public EntityMock() { }
        public EntityMock(Guid id, bool isLoaded) : base(id, isLoaded) { }

        [EntityProperty(EntityPropertyType.Value)]
        public string Name { get; set; }

        [EntityProperty(EntityPropertyType.Value)]
        public int Age { get; set; }

        public int total { get; set; }
    }
}
