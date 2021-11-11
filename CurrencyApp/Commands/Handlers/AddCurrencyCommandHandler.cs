using System.Threading;
using System.Threading.Tasks;
using CurrencyApp.Database;
using CurrencyApp.Model;
using Fody;
using MediatR;

namespace CurrencyApp.Commands.Handlers
{
    [ConfigureAwait(false)]
    internal sealed class AddCurrencyCommandHandler : AsyncRequestHandler<AddCurrencyCommand>
    {
        private readonly CurrencyContext context;

        public AddCurrencyCommandHandler(CurrencyContext context)
        {
            this.context = context;
        }

        protected override async Task Handle(AddCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = new Currency()
            {
                Id = request.Id,
                Name = request.Name,
                EngName = request.EngName,
                Nominal = request.Nominal,
                ParentCode = request.ParentCode,
            };

            context.Currencies.Add(currency);

            await context.SaveChangesAsync();
        }
    }
}