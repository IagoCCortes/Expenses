using System.Collections.Generic;
using Expenses.Domain.Common;
using Expenses.Domain.Exceptions;

namespace Expenses.Domain.ValueObjects
{
    public class Money : ValueObject
    {
        private static Dictionary<string, string> Currencies = new Dictionary<string, string>
        {
            { "dollar", "$" },
            { "brazilian real", "R$" },
            { "british pound", "£" },
        };

        private string _currency;

        public string Currency 
        { 
            get => _currency; 
            private set 
            {
                var lowerValue = value.ToLower();
                if (!Currencies.TryGetValue(lowerValue, out _))
                {
                    throw new UnsupportedCurrencyException(value);
                }

                _currency = lowerValue;
            } 
        }

        /** 
         * * should have a private setter, leaving as it is for now to avoid Mongo's
         * * System.FormatException: An error occurred while deserializing the Price property of class Expenses.Domain.Entities.Product: No matching creator found.
        */
        public decimal Amount { get;  set; }

        public Money(string currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public override string ToString() => $"{Currencies[Currency]}{Amount:N}";

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Currency;
            yield return Amount;
        }
    }
}