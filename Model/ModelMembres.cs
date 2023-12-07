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

        public ModelMembres(Dictionary<string, Livres> dictionnaire)
        {
            ListeMembres = new ObservableCollection<Membres>();
            ListeCommandesAttente = new ObservableCollection<Livres>();
            ListeCommandesTraitee = new ObservableCollection<Livres>();
            _dictionnaire = dictionnaire;

        }


        public void ChargerMembresXml(string nomFichier)
        {
            ListeMembres = new ObservableCollection<Membres>();
            ListeCommandesAttente = new ObservableCollection<Livres>();
            ListeCommandesTraitee = new ObservableCollection<Livres>();

            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlElement racine = document.DocumentElement;

            XmlElement unNoeud = racine["membres"];
            XmlNodeList lesMembresXML = unNoeud.GetElementsByTagName("membre");

            foreach (XmlElement unElement in lesMembresXML)
            {
                Membres unMembre = new Membres(unElement, _dictionnaire);
                XmlNodeList lesCommandes = unElement.GetElementsByTagName("commande");

                foreach (XmlElement commandes in lesCommandes)
                {
                    string isbn = commandes.GetAttribute("ISBN-13");
                    string statut = commandes.GetAttribute("statut");

                    //Source: Chatgpt
                    if(_dictionnaire.ContainsKey(isbn))
                    {
                        Livres commandesMembre = _dictionnaire[isbn];
                        if (statut == "Attente")
                        {
                            unMembre.ListeCommandesAttente.Add(commandesMembre);
                            ListeCommandesAttente.Add(commandesMembre);
                        }
                        else
                        {
                            unMembre.ListeCommandesTraitee.Add(commandesMembre);
                            ListeCommandesTraitee.Add((commandesMembre));
                        }
                    }
                }

                //XmlNodeList lesCommandes = unElement.GetElementsByTagName("commande");
                //foreach (XmlElement commandes in lesCommandes)
                //{
                //    string isbn = commandes.GetAttribute("ISBN-13");
                //    string statut = commandes.GetAttribute("statut");

                //Livres commandeMembre = new Livres(isbn, statut, "", "", 0);

                //    if (_dictionnaire.ContainsKey(isbn))
                //    {
                //        if (statut == "attente")
                //        {
                //            unMembre.ListeCommandesAttente.Add(commandeMembre);
                //            ListeCommandesAttente.Add(commandeMembre);
                //        }
                //        else
                //        {   unMembre.ListeCommandesTraitee.Add(commandeMembre);
                //            ListeCommandesTraitee.Add(commandeMembre);
                //        }
                //    }
                //}
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
            document.Save(nomFichier);
        }
    }
}
