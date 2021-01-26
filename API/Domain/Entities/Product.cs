using System;
using Expenses.Domain.Common;

namespace Expenses.Domain.Entities
{
    public class Product : Entity
    {
        public Product(string name, float price)
        {
            Name = name;
            Price = price;
        }
        public string Name { get; set; }
        public float Price { get; set; }
    }
}