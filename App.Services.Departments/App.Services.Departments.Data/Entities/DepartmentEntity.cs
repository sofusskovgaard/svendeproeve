using App.Data;
using App.Data.Attributes;

namespace App.Services.Departments.Data.Entities
{
    [CollectionDefinition(nameof(DepartmentEntity))]
    public class DepartmentEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string[] OrganizationIds { get; set; }
    }
}
