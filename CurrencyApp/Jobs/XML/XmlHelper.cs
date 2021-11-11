using System.Xml;
using System.Xml.Serialization;

namespace CurrencyApp.Jobs.XML
{
    internal static class XmlHelper
    {
        public static T? ParseXml<T>(string xmlString) where T : class
        {
            var reader = XmlReader.Create(xmlString, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Auto });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }
    }
}