using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Model;

namespace Model
{
    public class Membres : IConversionXML
    {
        public ObservableCollection<string> LesLivres 
        {
            private set;
            get;
        }

        public ObservableCollection<Commandes> LesCommandes
        {
            private set;
            get;
        }
         
        public string Nom
        {
            private set;
            get;
        }

        public bool? Administrateur
        {
            private set;
            get;
        }

        public Membres(string nom, bool administrateur)
        {
            Nom = nom;
            Administrateur = administrateur;
            LesLivres = new ObservableCollection<string>();
        }

        public Membres(XmlElement element)
        {
            Nom = element.GetAttribute("nom");
            LesLivres = new ObservableCollection<string>();
            DeXML(element); 
        }

        public override string ToString()
        {
            return Nom;
        }

        public XmlElement VersXML(XmlDocument doc)
        {
            XmlElement elementMembre = doc.CreateElement("membre");
            elementMembre.SetAttribute("nom", Nom);
            elementMembre.SetAttribute("administrateur", Administrateur.ToString());
            foreach(string livre in LesLivres) 
            {
                string isbnLivre = livre;
                XmlElement nouveauLivre = doc.CreateElement("livre");
                nouveauLivre.InnerText = isbnLivre;
                elementMembre.AppendChild(nouveauLivre);
            }
            return elementMembre;
        }

        public void DeXML(XmlElement elem)
        {
            XmlNodeList lesLivres = elem.GetElementsByTagName("livre");
            foreach (XmlElement elementLivre in lesLivres)
            {
                LesLivres.Add(elementLivre.GetAttribute("ISBN-13"));
            }
        }
    }
}
