using App.Data;
using App.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Orders.Data.Entities
{
    [CollectionDefinition(nameof(ProductEntity))]

    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}
