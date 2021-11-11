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
    /// Задание загрузки кодов валют
    /// </summary>
    [ConfigureAwait(false)]
    internal class LoadCurrencyJob : IJob
    {
        private const string CurrencyUrl = "https://www.cbr.ru/scripts/XML_val.asp?d=0";

        private readonly IMediator _mediator;

        public LoadCurrencyJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using HttpClient? httpClient = new();

            var xmlStr = await httpClient.GetStringAsync(CurrencyUrl);

            var valuta = XmlHelper.ParseXml<Valuta>(xmlStr);

            if (valuta is null)
                return;

            foreach (var val in valuta.Item)
            {
                await _mediator.Send(new AddCurrencyCommand(val.ID, val.Name, val.EngName, Convert.ToInt32(val.Nominal), val.ParentCode));
            }
        }
    }
}