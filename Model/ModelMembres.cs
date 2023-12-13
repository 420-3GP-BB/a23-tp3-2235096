using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;

namespace Model
{
    public class ModelMembres
    {
        // Dictionnaire qui va prendre les livres
        private Dictionary<string, Livres> _dictionnaire;

        // Liste qui va contenir les membres
        public ObservableCollection<Membres> ListeMembres
        {
            private set;
            get;
        }// fin

        // Liste qui va contenir les commandes en attentes
        public ObservableCollection<Livres> ListeCommandesAttente
        {
            private set;
            get;
        }// fin

        // Liste qui va contenir les commamdes traitée
        public ObservableCollection<Livres> ListeCommandesTraitee
        {
            private set;
            get;
        }// fin

        // Liste qui va contenir tout les commandes en attente pour afficher dans fenetre admin
        public ObservableCollection<Livres> ListeAttenteAdmin
        {
            set;
            get;
        }// fin

        // liste qui va contenir les commandes traitée pour afficher dams fenetre admin
        public ObservableCollection<Livres> ListeTraiteeAdmin
        {
            set;
            get;
        }// fin

        // Instance de ModelLivres
        private ModelLivres _modelLivres;

        // Constructeur ModelMembres qui prend en parametre le dictionnaire et ModelLivres
        public ModelMembres(Dictionary<string, Livres> dictionnaire, ModelLivres modelLivres)
        {
            // Pour initialiser ListeMembres
            ListeMembres = new ObservableCollection<Membres>();
            // Pour initialiser ListeCommandesAttente
            ListeCommandesAttente = new ObservableCollection<Livres>();
            // Pour initialiser ListeCommandesTraitee
            ListeCommandesTraitee = new ObservableCollection<Livres>();
            // Pour initialiser le dictionnaire
            _dictionnaire = dictionnaire;
            // Pour initialiser _modelLivres
            _modelLivres = modelLivres;
            // Pour initialiser ListeAttenteAdmin
            ListeAttenteAdmin = new ObservableCollection<Livres>();
            // Pour initialiser ListeTraiteeAdmin
            ListeTraiteeAdmin = new ObservableCollection<Livres>();
        }// fin constructeur ModelMembres


        /**
         * Methode ChargerMembresXml qui charge les membres et les commandes 
         */
        public void ChargerMembresXml(string nomFichier)
        {
            // Création du objet XmlDocument
            XmlDocument document = new XmlDocument();
            // Pour charger le fichier xml
            document.Load(nomFichier);
            // Pour obtenir l'élément racine du xml
            XmlElement racine = document.DocumentElement;

            // Source: Inspirer du solution module 7, exercice 2
            // Pour obtenir element membres avec la racine
            XmlElement unNoeud = racine["membres"];
            // Pour prendre les éléments avec la balise "membre"
            XmlNodeList lesMembresXML = unNoeud.GetElementsByTagName("membre");

            // Pour boucler a travers les membres
            foreach (XmlElement unElement in lesMembresXML)
            {
                // Création du objet Membres qui prend en parametre unElement et le dictionnaire
                Membres unMembre = new Membres(unElement, _dictionnaire);

                // Declaration du variable type string pour le attribut administrateur
                string admin = unElement.GetAttribute("administrateur");
                // Pour prendre la valeur du administration (True ou False)
                unMembre.Administrateur = bool.Parse(admin);

                // Pour prendre les éléments avec la balise "commande"
                XmlNodeList lesCommandes = unElement.GetElementsByTagName("commande");

                // Pour boucler a travers les commandes
                foreach (XmlElement unCommandes in lesCommandes)
                {
                    // Declaration du variable de type string pour prendre le attribut ISBN-13
                    string isbnDuLivre = unCommandes.GetAttribute("ISBN-13");
                    // Declaration du variable de type string pour prendre le attribut statut
                    string statut = unCommandes.GetAttribute("statut");

                    // Condition if pour voire si le dictionnaire a le livre en regardant le isbn du livre
                    if (_dictionnaire.ContainsKey(isbnDuLivre))//Source: Chatgpt
                    {
                        // Pour prendre le livre du dictionnaire
                        Livres commandesMembre = _dictionnaire[isbnDuLivre];

                        // Condition if pour voire le statut du livre est en Attente
                        if (statut == "Attente")
                        {
                            // Si vrai, ajoute la commande dans le ListeCommandesAttente
                            unMembre.ListeCommandesAttente.Add(commandesMembre);
                            // Ajoute le livre dans ListeAttenteAdmin pour afficher dans fenetre admin
                            ListeAttenteAdmin.Add(commandesMembre);
                        }
                        else
                        {
                            // Sinon, ajoute la commande dans le ListeCommandesTraitee
                            unMembre.ListeCommandesTraitee.Add(commandesMembre);
                            // Ajoute le livre dans ListeTraiteeAdmin pour afficher dans fenetre admin
                            ListeTraiteeAdmin.Add(commandesMembre);
                        }// fin condition if 
                    }// fin condition if
                }// fin boucle foreach

                // Pour ajouter le membre dans ListeMembres
                ListeMembres.Add(unMembre);
            }// fin boucle foreach
        }// fin methode ChargerMembresXml


        /**
         * Methode SauvegarderFichierXml pour sauvegarder les modifications dans le fichier bibliotheque
         */
        public void SauvegarderFichierXml(string nomFichier)
        {
            // Création du objet XmlDocument
            XmlDocument document = new XmlDocument();
            // Création du racine "bibliotheque"
            XmlElement racine = document.CreateElement("bibliotheque");
            // Pour ajouter element racine
            document.AppendChild(racine);

            // Pour crée element membres
            XmlElement elementMembre = document.CreateElement("membres");
            // Pour ajouter element racine 
            racine.AppendChild(elementMembre);

            // Pour boucler a travers les ListeMembres
            foreach (Membres unMembre in ListeMembres)
            {
                // Pour prendre le membre et le convertir dans XmlElement
                XmlElement element = unMembre.VersXML(document);
                elementMembre.AppendChild(element);
            }// fin boucle foreach

            // Pour crée element membres
            XmlElement elementLivres = document.CreateElement("livres");
            // Pour ajouter element racine 
            racine.AppendChild(elementLivres);

            // Pour boucler a travers ListeLivres
            foreach (Livres unLivre in _modelLivres.ListeLivres) //Source: chatgpt
            {
                // Pour crée element livre
                XmlElement elementLivre = document.CreateElement("livre");
                // Pour prendre le attribut ISBN-13 et le ajouter dans elementLivre
                elementLivre.SetAttribute("ISBN-13", unLivre.ISBN);

                // Pour crée element titre
                XmlElement unTitre = document.CreateElement("titre");
                // Pour prendre le texte de element titre
                unTitre.InnerText = unLivre.Titre;
                // Pour ajouter element titre sous livre
                elementLivre.AppendChild(unTitre);

                // Pour crée element auteur
                XmlElement unAuteur = document.CreateElement("auteur");
                // Pour prendre le texte de element auteur
                unAuteur.InnerText = unLivre.Auteur;
                // Pour ajouter element auteur sous livre
                elementLivre.AppendChild(unAuteur);

                // Pour crée element editeur
                XmlElement unEditeur = document.CreateElement("editeur");
                // Pour prendre le texte de element editeur
                unEditeur.InnerText = unLivre.Editeur;
                // Pour ajouter element editeur sous livre
                elementLivre.AppendChild (unEditeur);

                // Pour crée element annee
                XmlElement uneAnnee = document.CreateElement("annee");
                // Pour prendre le texte de element annee
                uneAnnee.InnerText = unLivre.Annee.ToString();
                // Pour ajouter element annee sous livre
                elementLivre.AppendChild(uneAnnee);

                // Pour ajouter element "livre" a "livres"
                elementLivres.AppendChild(elementLivre);
            }// fin boucle foreach

            // Pour sauvegarder le document dans le endroit spécifique
            document.Save(nomFichier);
        }// fin methode SauvegarderFichierXml

    }// fin classe ModelMembres
}// fin namespace Model
