using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CurrencyApp.Database;
using Fody;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Queries.Handlers
{
    [ConfigureAwait(false)]
    internal class GetLatestCurrencyRateQueryHandler : IRequestHandler<GetLatestCurrencyRateQuery, decimal?>
    {
        private readonly CurrencyContext _context;

        public GetLatestCurrencyRateQueryHandler(CurrencyContext context)
        {
            _context = context;
        }

        public async Task<decimal?> Handle(GetLatestCurrencyRateQuery request, CancellationToken cancellationToken)
        {
            var currency = await _context.CurrencyRates
                .Where(x => x.CurrencyId == request.CurrencyId)
                .OrderByDescending(x => x.LastUpdateDate)
                .FirstOrDefaultAsync();

            return currency?.Value;
        }
    }
}