using System.Xml;
using System.Xml.Serialization;

namespace CurrencyApp.Jobs.XML
{
    internal static class XmlHelper
    {
        public static T? ParseXml<T>(string xmlString) where T : class
        {
            var reader = XmlReader.Create(xmlString.Trim(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }
    }
}