using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CurrencyApp.Database;
using CurrencyApp.Model;
using Fody;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Queries.Handlers
{
    [ConfigureAwait(false)]
    internal class GetCurrencyListQueryHandler : IRequestHandler<GetCurrencyListQuery, IEnumerable<Currency>>
    {
        private readonly CurrencyContext _context;

        public GetCurrencyListQueryHandler(CurrencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Currency>> Handle(GetCurrencyListQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Currencies.ToListAsync();

            return data;
        }
    }
}