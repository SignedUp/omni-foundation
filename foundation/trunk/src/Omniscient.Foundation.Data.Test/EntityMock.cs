using System;

namespace Omniscient.Foundation.Data
{
    public class EntityMock: EntityBase
    {
        public EntityMock() { }
        public EntityMock(Guid id, bool isLoaded) : base(id) { }

        public string Name { get; set; }

        public int Age { get; set; }

        public int total { get; set; }
    }
}
