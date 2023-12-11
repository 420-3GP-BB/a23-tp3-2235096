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

        public ObservableCollection<Livres> ListeCommandesAttente
        {
            private set;
            get;
        }

        public ObservableCollection<Livres> ListeCommandesTraitee
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
            ListeLivres = new ObservableCollection<Livres>();
            ListeCommandesAttente = new ObservableCollection<Livres>();
            ListeCommandesTraitee = new ObservableCollection<Livres>();
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


            //Source: Inspirer du Module 7, exercice 2
            foreach(Livres livre in ListeLivres) 
            {
                string isbnLivre = livre.ISBN;
                XmlElement nouveauLivre = doc.CreateElement("livre");
                nouveauLivre.SetAttribute("ISBN-13", isbnLivre);
                elementMembre.AppendChild(nouveauLivre);
            }

            foreach (Livres attenteLivre in ListeCommandesAttente)
            {
                string isbnCommande = attenteLivre.ISBN;
                XmlElement nouveauCommande = doc.CreateElement("commande");
                nouveauCommande.SetAttribute("statut", "Attente");
                nouveauCommande.SetAttribute("ISBN-13", isbnCommande);
                elementMembre.AppendChild(nouveauCommande);
            }

            foreach(Livres traiteeLivre in ListeCommandesTraitee)
            {
                string isbnCommande = traiteeLivre.ISBN;
                XmlElement nouveauCommande = doc.CreateElement("commande");
                nouveauCommande.SetAttribute("statut", "Traitee");
                nouveauCommande.SetAttribute("ISBN-13", isbnCommande);
                elementMembre.AppendChild(nouveauCommande);
            }

            return elementMembre;
        }

        public void DeXML(XmlElement elem)
        {
            //ListeLivres = new ObservableCollection<Livres>();
            ListeCommandesAttente = new ObservableCollection<Livres>();
            ListeCommandesTraitee = new ObservableCollection<Livres>();

            XmlNodeList lesLivres = elem.GetElementsByTagName("livre");
            foreach (XmlElement elementLivre in lesLivres)
            {
                ListeLivres.Add(_dictionnaire[elementLivre.GetAttribute("ISBN-13")]);
            }
        }

        public override string ToString()
        {
            return Nom;
        }

        public void AjouterLivre(string leISBN, string Titre, string Auteur, string Editeur, int Annee)
        {
            ListeCommandesAttente.Add(new Livres(leISBN, Titre, Auteur, Editeur, Annee));
            
        }

        public void RetirerLivre(string isbn)
        {
            int index = 0;
            bool chercher = false;

            while(index < ListeCommandesAttente.Count && !chercher)
            {
                if (ListeCommandesAttente[index].ISBN.Equals(isbn))
                {
                    ListeCommandesAttente.RemoveAt(index);
                    chercher = true;
                }
                index++;
            }
        }
    }
}
