using System;
using System.Threading;
using System.Threading.Tasks;
using CurrencyApp.Database;
using CurrencyApp.Model;
using Fody;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Commands.Handlers
{
    [ConfigureAwait(false)]
    internal sealed class AddCurrencyRateCommandHandler : AsyncRequestHandler<AddCurrencyRateCommand>
    {
        private readonly CurrencyContext context;

        public AddCurrencyRateCommandHandler(CurrencyContext context)
        {
            this.context = context;
        }

        protected override async Task Handle(AddCurrencyRateCommand request, CancellationToken cancellationToken)
        {
            var rate = await context.CurrencyRates
                .SingleOrDefaultAsync(x => x.CurrencyId == request.CurrencyId && x.LastUpdateDate.Date == request.CurrencyDate.Date);

            if (rate is not null)
                await UpdateRate(rate.Id, request.Value, request.CurrencyDate);
            else
                await AddRate(request.CurrencyId, request.Value, request.CurrencyDate);

            await context.SaveChangesAsync();
        }

        private async Task UpdateRate(Guid rateGuid, decimal value, DateTimeOffset date)
        {
            var rate = await context.CurrencyRates.SingleAsync(x => x.Id == rateGuid);

            rate.Value = value;
            rate.LastUpdateDate = date;

            context.CurrencyRates.Update(rate);
        }

        private Task AddRate(string currencyId, decimal value, DateTimeOffset date)
        {
            var rate = new CurrencyRate()
            {
                Id = Guid.NewGuid(),
                CurrencyId = currencyId,
                Value = value,
                LastUpdateDate = date
            };

            context.CurrencyRates.Add(rate);

            return Task.CompletedTask;
        }
    }
}