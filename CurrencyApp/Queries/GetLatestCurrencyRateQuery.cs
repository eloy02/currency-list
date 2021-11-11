using MediatR;

namespace CurrencyApp.Queries
{
    internal class GetLatestCurrencyRateQuery : IRequest<decimal?>
    {
        public GetLatestCurrencyRateQuery(string currencyId)
        {
            CurrencyId = currencyId;
        }

        public string CurrencyId { get; set; }
    }
}