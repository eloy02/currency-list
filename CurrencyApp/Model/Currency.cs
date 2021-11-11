using System.Collections.Generic;

namespace CurrencyApp.Model
{
    /// <summary>
    /// Валюта
    /// </summary>
    public sealed class Currency
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? EngName { get; set; }
        public int Nominal { get; set; }
        public string ParentCode { get; set; }
        public string ISOCode { get; set; }

        public List<CurrencyRate> Rates { get; set; }
    }
}