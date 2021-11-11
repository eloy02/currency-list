using System.Collections.Generic;
using CurrencyApp.Model;
using MediatR;

namespace CurrencyApp.Queries
{
    /// <summary>
    /// Запрос списка валют
    /// </summary>
    internal class GetCurrencyListQuery : IRequest<IEnumerable<Currency>>
    {
    }
}