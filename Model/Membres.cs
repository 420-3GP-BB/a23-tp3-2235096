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

        private Dictionary<string, Livres> _dictionnaire;
        public ObservableCollection<Livres> ListeLivres
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

        public Membres(string nom, bool administrateur) //Dictionairy, ISBNs dans user dans parathèses
        {
            Nom = nom;
            Administrateur = administrateur;
            ListeLivres = new ObservableCollection<Livres>();

            //if (dictionnaire.ContainsKey(isbn))
            //{
            //    ListeLivres.Add(dictionnaire[isbn]);
            //}

            //if dictionairy contain key
            //add ListeLivres
        }

        public Membres(XmlElement element, Dictionary<string, Livres> dictionnaire)
        {
            Nom = element.GetAttribute("nom");
            ListeLivres = new ObservableCollection<Livres>();
            _dictionnaire = dictionnaire;
            DeXML(element); 
        }

        

        public XmlElement VersXML(XmlDocument doc)
        {
            XmlElement elementMembre = doc.CreateElement("membre");
            elementMembre.SetAttribute("nom", Nom);
            elementMembre.SetAttribute("administrateur", Administrateur.ToString());
            foreach(Livres livre in ListeLivres) 
            {
                string isbnLivre = livre.ISBN;
                XmlElement nouveauLivre = doc.CreateElement("livre");
                nouveauLivre.InnerText = isbnLivre;
                elementMembre.AppendChild(nouveauLivre);
            }
            return elementMembre;
        }

        public void DeXML(XmlElement elem)
        {
            //Nom = elem.GetAttribute("nom");
            //Administrateur = elem.GetAttribute("administrateur");

            ListeLivres = new ObservableCollection<Livres>();
            XmlNodeList lesLivres = elem.GetElementsByTagName("livre");
            foreach (XmlElement elementLivre in lesLivres)
            {
                //string isbn = elementLivre.GetAttribute("ISBN-13");
                //if (_dictionnaire.ContainsKey(isbn))
                //{
                //    ListeLivres.Add(_dictionnaire[isbn]);
                //}

                ListeLivres.Add(_dictionnaire[elementLivre.GetAttribute("ISBN-13")]);
            }
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
