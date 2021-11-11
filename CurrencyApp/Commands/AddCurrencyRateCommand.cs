using System;
using MediatR;

namespace CurrencyApp.Commands
{
    internal class AddCurrencyRateCommand : IRequest
    {
        public string CurrencyId { get; set; }
        public decimal Value { get; set; }
        public DateTimeOffset CurrencyDate { get; set; }

        public AddCurrencyRateCommand(string currencyId, decimal value, DateTimeOffset currencyDate)
        {
            CurrencyId = currencyId;
            Value = value;
            CurrencyDate = currencyDate;
        }
    }
}