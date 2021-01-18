using System;

namespace Domain.Common
{
    public interface IMongoEntityBase
    {
        Guid Id { get; set; }
    }
}