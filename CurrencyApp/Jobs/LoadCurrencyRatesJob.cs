using System;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyApp.Commands;
using CurrencyApp.Jobs.XML;
using Fody;
using MediatR;
using Quartz;

namespace CurrencyApp.Jobs
{
    /// <summary>
    /// Задание загрузки котировок валют
    /// </summary>
    [ConfigureAwait(false)]
    internal sealed class LoadCurrencyRatesJob : IJob
    {
        private const string Url = "https://www.cbr.ru/scripts/XML_daily.asp";

        private readonly IMediator _mediator;

        public LoadCurrencyRatesJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using HttpClient? httpClient = new();

            var xmlStr = await httpClient.GetStringAsync(Url);

            var valuta = XmlHelper.ParseXml<ValCurs>(xmlStr);

            if (valuta is null)
                return;

            var date = DateTimeOffset.Parse(valuta.Date);

            foreach (var val in valuta.Valute)
            {
                await _mediator.Send(new AddCurrencyRateCommand(val.ID, decimal.Parse(val.Value), date));
            }
        }
    }
}