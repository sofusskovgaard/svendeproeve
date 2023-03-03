using App.Data;
using App.Data.Attributes;

namespace App.Services.Orders.Data.Entities
{
    [CollectionDefinition(nameof(ProductEntity))]

    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
