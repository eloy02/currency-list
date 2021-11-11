using System;
using MediatR;

namespace CurrencyApp.Commands
{
    internal class AddCurrencyRateCommand : IRequest
    {
        public string CurrencyId { get; set; }
        public decimal Nominal { get; set; }
        public DateTimeOffset CurrencyDate { get; set; }

        public AddCurrencyRateCommand(string currencyId, decimal nominal, DateTimeOffset currencyDate)
        {
            CurrencyId = currencyId;
            Nominal = nominal;
            CurrencyDate = currencyDate;
        }
    }
}