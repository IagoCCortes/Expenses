using System;

namespace Expenses.Domain.Common
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CollectionNameAttribute : Attribute
    {
        public string CollectionName { get; }

        public CollectionNameAttribute(string name)
        {
            CollectionName = name;
        }
    }
}