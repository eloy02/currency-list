using MediatR;

namespace CurrencyApp.Commands
{
    internal class AddCurrencyCommand : IRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? EngName { get; set; }
        public int Nominal { get; set; }
        public string ParentCode { get; set; }
        public string ISOCode { get; set; }

        public AddCurrencyCommand(string id, string name, string? engName, int nominal, string parentCode, string isoCode)
        {
            Id = id;
            Name = name;
            EngName = engName;
            Nominal = nominal;
            ParentCode = parentCode;
            ISOCode = isoCode;
        }
    }
}