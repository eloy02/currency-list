using System;

namespace CurrencyApp.Model
{
    /// <summary>
    /// Курс валюты
    /// </summary>
    /// <param name="Currency">Валюта</param>
    /// <param name="Value"></param>
    /// <param name="LastUpdateDate"></param>
    public sealed record CurrencyRate(Guid Id, Currency Currency, decimal Value, DateTimeOffset LastUpdateDate, string CurrencyId);
}