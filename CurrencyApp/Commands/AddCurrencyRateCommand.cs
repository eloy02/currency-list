using System;
using MediatR;

namespace CurrencyApp.Commands
{
    internal class AddCurrencyRateCommand : IRequest
    {
        public AddCurrencyRateCommand(string currencyId, decimal value, DateTimeOffset currencyDate) =>
            (CurrencyId, Value, CurrencyDate) = (currencyId, value, currencyDate);

        public string CurrencyId { get; set; }
        public decimal Value { get; set; }
        public DateTimeOffset CurrencyDate { get; set; }
    }
}