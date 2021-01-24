using System;

namespace Expenses.Domain.Common
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime Created { get; set; }
        string CreatedBy { get; set; }
        DateTime? LastModified { get; set; }
        string LastModifiedBy { get; set; }
    }
}