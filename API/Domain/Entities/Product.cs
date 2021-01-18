using System;
using Domain.Common;
using Expenses.Domain.Common;

namespace Domain.Entities
{
    public class Product : AuditableEntity, IMongoEntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
    }
}