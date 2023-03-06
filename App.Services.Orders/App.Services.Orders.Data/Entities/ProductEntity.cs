using App.Data;
using App.Data.Attributes;

namespace App.Services.Orders.Data.Entities
{
    [SearchIndexDefinition("search")]
    [CollectionDefinition(nameof(ProductEntity))]

    public class ProductEntity : BaseEntity
    {
        [IndexedProperty("search")]
        public string Name { get; set; }

        [IndexedProperty("search")]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public string? ReferenceId { get; set; }

        public string? ReferenceType { get; set; }
    }
}
