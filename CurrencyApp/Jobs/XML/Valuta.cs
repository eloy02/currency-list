namespace CurrencyApp.Jobs.XML
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Valuta
    {
        private ValutaItem[] itemField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Item")]
        public ValutaItem[] Item
        {
            get
            {
                return itemField;
            }
            set
            {
                itemField = value;
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
    public partial class ValutaItem
    {
        private string nameField;

        private string engNameField;

        private uint nominalField;

        private string parentCodeField;

        private string iSO_Num_CodeField;

        private string iSO_Char_CodeField;

        private string idField;

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
        public string EngName
        {
            get
            {
                return engNameField;
            }
            set
            {
                engNameField = value;
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
        public string ParentCode
        {
            get
            {
                return parentCodeField;
            }
            set
            {
                parentCodeField = value;
            }
        }

        /// <remarks/>
        public string ISO_Num_Code
        {
            get
            {
                return iSO_Num_CodeField;
            }
            set
            {
                iSO_Num_CodeField = value;
            }
        }

        /// <remarks/>
        public string ISO_Char_Code
        {
            get
            {
                return iSO_Char_CodeField;
            }
            set
            {
                iSO_Char_CodeField = value;
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