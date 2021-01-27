using System;

namespace Expenses.Domain.Exceptions
{
    public class UnsupportedCurrencyException : Exception
    {
        public UnsupportedCurrencyException(string currency)
            : base($"Currency \"{currency}\" is unsupported.")
        {
        }
    }
}