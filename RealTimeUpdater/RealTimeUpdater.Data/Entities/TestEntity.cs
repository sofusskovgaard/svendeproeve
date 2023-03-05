using App.Data;
using App.Data.Attributes;

namespace RealTimeUpdater.Data.Entities
{
    [CollectionDefinition(nameof(TestEntity))]
    public class TestEntity : BaseEntity
    {
        public string SomePublicProperty { get; set; }

        public string SomeDetailedProperty { get; set; }
    }
}