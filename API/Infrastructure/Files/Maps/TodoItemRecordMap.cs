using CsvHelper.Configuration;
using System.Globalization;

namespace Expenses.Infrastructure.Files.Maps
{
    public class TodoItemRecordMap<T> : ClassMap<T>
    {
        public TodoItemRecordMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            // Map(m => m.Done).ConvertUsing(c => c.Done ? "Yes" : "No");
        }
    }
}
