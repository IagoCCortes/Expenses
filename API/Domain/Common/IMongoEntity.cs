using System;

namespace Expenses.Domain.Common
{
    public interface IMongoEntity
    {
        Guid Id { get; set; }
        string TableName { get; }
    }
}