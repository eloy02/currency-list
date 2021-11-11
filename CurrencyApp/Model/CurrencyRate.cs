using System;

namespace CurrencyApp.Model
{
    /// <summary>
    /// Курс валюты
    /// </summary>
    public sealed class CurrencyRate
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }

        public Currency Currency { get; set; }
        public string CurrencyId { get; set; }
    }
}