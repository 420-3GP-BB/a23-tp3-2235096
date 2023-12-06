using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;

namespace Model
{
    public class ModelMembres
    {
        private Dictionary<string, Livres> _dictionnaire;
        public ObservableCollection<Membres> ListeMembres
        {
            private set;
            get;
        }

        public ObservableCollection<Commandes> ListeCommandesAttente
        {
            private set;
            get;
        }

        public ObservableCollection<Commandes> ListeCommandesTraitee
        {
            private set;
            get;
        }

        public ModelMembres(Dictionary<string, Livres> dictionnaire)
        {
            ListeMembres = new ObservableCollection<Membres>();
            _dictionnaire = dictionnaire;

        }


        public void ChargerMembresXml(string nomFichier)
        {
            ListeMembres = new ObservableCollection<Membres>();
            ListeCommandesAttente = new ObservableCollection<Commandes>();
            ListeCommandesTraitee = new ObservableCollection<Commandes>();

            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlElement racine = document.DocumentElement;

            XmlElement unNoeud = racine["membres"];
            XmlNodeList lesMembresXML = unNoeud.GetElementsByTagName("membre");

            foreach (XmlElement unElement in lesMembresXML)
            {
                Membres unMembre = new Membres(unElement, _dictionnaire);

                XmlNodeList lesCommandesMembres = unElement.GetElementsByTagName("commande");
                foreach(XmlElement unCommande in lesCommandesMembres)
                {
                    //Source: chatgpt
                    string statut = unCommande.GetAttribute("statut");
                    string isbn = unCommande.GetAttribute("ISBN-13");

                    Commandes desCommandes = new Commandes(statut, isbn);

                    if (statut == "Attente")
                    {
                        unMembre.ListeCommandesAttente.Add(desCommandes);
                    }
                    else
                    {
                        unMembre.ListeCommandesTraitee.Add(desCommandes);
                    }
                    
                }
                ListeMembres.Add(unMembre);
                //ListeMembres.Add(new Membres(unElement, _dictionnaire));
            }
        }

        public void SauvegarderFichierXml(string nomFichier)
        {
            XmlDocument document = new XmlDocument();
            XmlElement racine = document.CreateElement("bibliotheque");
            document.AppendChild(racine);

            XmlElement elementMembre = document.CreateElement("membres");
            racine.AppendChild(elementMembre);

            foreach (Membres unMembre in ListeMembres)
            {
                XmlElement element = unMembre.VersXML(document);
                elementMembre.AppendChild(element);
            }
            document.Save(nomFichier);
        }
    }
}
