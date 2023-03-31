using RvM2.GameClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RvM2.UtilityClasses
{
    [Serializable]
    public class XMLHandler
    {
        private string _Ledger;
        public string Ledger
        {
            get { return this._Ledger; }
            set
            {
                this._Ledger = value;
            }
        }

        private List<Army> _Armies;
        public List<Army> Armies
        {
            get { return this._Armies; }
            set { this._Armies = value; }
        }

        public string ObjectToXML(string filename)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(this.GetType());
            StringBuilder retVal = new StringBuilder("");
            System.IO.StringWriter s = new System.IO.StringWriter(retVal);
            serializer.Serialize(s, this);
            File.WriteAllText(filename, retVal.ToString());
            return retVal.ToString();
        }

        public override string ToString()
        {
            string retval = "The armies are:\n\r";
            foreach (Army a in Armies)
            {
                retval += a.ToString() + "\n\r";
            }
            return retval;
        }

        public XMLHandler()
        {
            this.Armies = new List<Army>();
            this.Ledger = "";
        }
        public XMLHandler(string FileName)
            : this()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(this.GetType());

                string test = System.IO.File.ReadAllText(FileName);

                System.IO.StringReader read = new System.IO.StringReader(test);

                XMLHandler armies2 = serializer.Deserialize(read) as XMLHandler;

                this.Armies.AddRange(armies2.Armies);
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message);
            }
        }
        public XMLHandler(List<Army> armies, string xml)
        {
            this.Armies = armies;
            Ledger = xml;
        }
    }
}
