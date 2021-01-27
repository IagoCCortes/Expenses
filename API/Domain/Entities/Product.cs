using System;
using Expenses.Domain.Common;
using Expenses.Domain.ValueObjects;

namespace Expenses.Domain.Entities
{
    public class Product : Entity
    {
        public Product(string name, string currency, decimal amount)
        {
            Name = name;
            Price = new Money(currency, amount);
        }
        public Product(string name, Money money)
        {
            Name = name;
            Price = money;
        }
        public string Name { get; private set; }
        public Money Price { get; private set; }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdatePrice(string currency, decimal? price)
        {
            if (currency.ToLower() != Price.Currency || price != Price.Amount)
            {
                var newCurrency = currency ?? Price.Currency;
                var newPrice = price ?? Price.Amount;
                Price = new Money(newCurrency, newPrice);
            }
        }
    }
}