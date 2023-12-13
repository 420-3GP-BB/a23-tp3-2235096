using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public interface IConversionXML
    {
        // Methode VersXML
        public XmlElement VersXML(XmlDocument doc);
        // Methode DeXML
        public void DeXML(XmlElement elem);
    }// fin interface IConversionXML
}// fin namespace GTD
