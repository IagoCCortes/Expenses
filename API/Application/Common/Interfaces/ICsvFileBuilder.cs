using System.Collections.Generic;

namespace Expenses.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile<T>(IEnumerable<T> records);
    }
}
