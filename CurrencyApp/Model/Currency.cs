using System.Collections.Generic;

namespace CurrencyApp.Model
{
    /// <summary>
    /// Валюта
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="EngName"></param>
    /// <param name="Nominal"></param>
    /// <param name="ParentCode"></param>
    public sealed record Currency(string Id, string Name, string? EngName, int Nominal, string ParentCode, List<CurrencyRate> Rates);
}