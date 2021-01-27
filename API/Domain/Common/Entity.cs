using System;

namespace Expenses.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public DateTime Created { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? LastModified { get; private set; }
        public string LastModifiedBy { get; private set; }

        public void Create(string createdBy)
        {
            Created = DateTime.Now;
            CreatedBy = createdBy;
        }

        public void Update(string updatedBy)
        {
            LastModified = DateTime.Now;
            LastModifiedBy = updatedBy;
        }
    }
}