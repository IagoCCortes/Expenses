using System;
using Expenses.Domain.Common;

namespace Expenses.Domain.Entities
{
    public class Product : IAuditableEntity, IMongoEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public string TableName => "Products";

        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    }
}