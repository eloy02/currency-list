using System.Collections.Generic;
using CurrencyApp.Model;
using MediatR;

namespace CurrencyApp.Queries.Handlers
{
    /// <summary>
    /// Запрос котировок валют
    /// </summary>
    internal class GetCurrenciesRatesQuery : IRequest<IEnumerable<CurrencyRate>>
    { }
}