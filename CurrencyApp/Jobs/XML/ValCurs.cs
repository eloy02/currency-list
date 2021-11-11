namespace CurrencyApp.Jobs.XML
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class ValCurs
    {
        private ValCursValute[] valuteField;

        private string dateField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Valute")]
        public ValCursValute[] Valute
        {
            get
            {
                return valuteField;
            }
            set
            {
                valuteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Date
        {
            get
            {
                return dateField;
            }
            set
            {
                dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ValCursValute
    {
        private ushort numCodeField;

        private string charCodeField;

        private uint nominalField;

        private string nameField;

        private string valueField;

        private string idField;

        /// <remarks/>
        public ushort NumCode
        {
            get
            {
                return numCodeField;
            }
            set
            {
                numCodeField = value;
            }
        }

        /// <remarks/>
        public string CharCode
        {
            get
            {
                return charCodeField;
            }
            set
            {
                charCodeField = value;
            }
        }

        /// <remarks/>
        public uint Nominal
        {
            get
            {
                return nominalField;
            }
            set
            {
                nominalField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;
            }
        }

        /// <remarks/>
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return idField;
            }
            set
            {
                idField = value;
            }
        }
    }
}