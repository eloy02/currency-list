using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CurrencyApp.Database;
using CurrencyApp.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Queries.Handlers
{
    internal sealed class GetCurrenciesRatesQueryHandler : IRequestHandler<GetCurrenciesRatesQuery, IEnumerable<CurrencyRate>>
    {
        private readonly CurrencyContext _context;

        public GetCurrenciesRatesQueryHandler(CurrencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CurrencyRate>> Handle(GetCurrenciesRatesQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.CurrencyRates
                .Include(x => x.Currency)
                .AsAsyncEnumerable()
                .GroupBy(x => x.LastUpdateDate)
                .SelectAwait(g => g.FirstAsync())
                .ToListAsync();

            return data;
        }
    }
}