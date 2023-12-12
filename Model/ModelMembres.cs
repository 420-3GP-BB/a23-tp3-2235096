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

        public ObservableCollection<Livres> ListeAttenteAdmin
        {
            set;
            get;
        }

        public ObservableCollection<Livres> ListeTraiteeAdmin
        {
            set;
            get;
        }

        private ModelLivres _modelLivres;
        public ModelMembres(Dictionary<string, Livres> dictionnaire, ModelLivres modelLivres)
        {
            ListeMembres = new ObservableCollection<Membres>();
            ListeCommandesAttente = new ObservableCollection<Livres>();
            ListeCommandesTraitee = new ObservableCollection<Livres>();
            _dictionnaire = dictionnaire;
            _modelLivres = modelLivres;
            ListeAttenteAdmin = new ObservableCollection<Livres>();
            ListeTraiteeAdmin = new ObservableCollection<Livres>(); 
        }




        public void ChargerMembresXml(string nomFichier)
        {
            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlElement racine = document.DocumentElement;

            //Source: Inspirer du solution module 7, exercice 2
            XmlElement unNoeud = racine["membres"];
            XmlNodeList lesMembresXML = unNoeud.GetElementsByTagName("membre");

            foreach (XmlElement unElement in lesMembresXML)
            {
                Membres unMembre = new Membres(unElement, _dictionnaire);

                //New code
                string admin = unElement.GetAttribute("administrateur");
                unMembre.Administrateur = bool.Parse(admin);
                //End new code

                XmlNodeList lesCommandes = unElement.GetElementsByTagName("commande");
                
                foreach (XmlElement unCommandes in lesCommandes)
                {
                    string isbnDuLivre = unCommandes.GetAttribute("ISBN-13");
                    string statut = unCommandes.GetAttribute("statut");

                    if (_dictionnaire.ContainsKey(isbnDuLivre))//Source: Chatgpt
                    {
                        Livres commandesMembre = _dictionnaire[isbnDuLivre];
                        if (statut == "Attente")
                        {
                            unMembre.ListeCommandesAttente.Add(commandesMembre);
                            ListeAttenteAdmin.Add(commandesMembre);
                        }
                        else
                        {
                            unMembre.ListeCommandesTraitee.Add(commandesMembre);
                            ListeTraiteeAdmin.Add(commandesMembre);
                        }
                    }
                }
                ListeMembres.Add(unMembre);
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

            XmlElement elementLivres = document.CreateElement("livres");
            racine.AppendChild(elementLivres);

            foreach (Livres unLivre in _modelLivres.ListeLivres) //Source chatgpt
            {
                XmlElement elementLivre = document.CreateElement("livre");
                elementLivre.SetAttribute("ISBN-13", unLivre.ISBN);

                XmlElement unTitre = document.CreateElement("titre");
                unTitre.InnerText = unLivre.Titre;
                elementLivre.AppendChild(unTitre);

                XmlElement unAuteur = document.CreateElement("auteur");
                unAuteur.InnerText = unLivre.Auteur;
                elementLivre.AppendChild(unAuteur);

                XmlElement unEditeur = document.CreateElement("editeur");
                unEditeur.InnerText = unLivre.Editeur;
                elementLivre.AppendChild (unEditeur);

                XmlElement uneAnnee = document.CreateElement("annee");
                uneAnnee.InnerText = unLivre.Annee.ToString();
                elementLivre.AppendChild(uneAnnee);

                elementLivres.AppendChild(elementLivre);
            }

            document.Save(nomFichier);
        }
    }
}
