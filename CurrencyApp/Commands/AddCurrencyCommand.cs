using MediatR;

namespace CurrencyApp.Commands
{
    internal class AddCurrencyCommand : IRequest
    {
        public AddCurrencyCommand(string id, string name, string? engName, int nominal, string parentCode, string isoCode) =>
            (Id, Name, EngName, Nominal, ParentCode, ISOCode) = (id, name, engName, nominal, parentCode, isoCode);

        public string Id { get; set; }
        public string Name { get; set; }
        public string? EngName { get; set; }
        public int Nominal { get; set; }
        public string ParentCode { get; set; }
        public string ISOCode { get; set; }
    }
}