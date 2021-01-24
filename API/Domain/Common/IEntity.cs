using System;

namespace Expenses.Domain.Common
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}