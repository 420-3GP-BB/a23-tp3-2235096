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
        // Dictionnaire private qui va prendre les livres
        private Dictionary<string, Livres> _dictionnaire;

        // Propriété ObservableCollection pour ListeLivres
        public ObservableCollection<Livres> ListeLivres
        {
            private set;
            get;
        }// fin propriété ObservableCollection

        // Propriété ObservableCollection pour ListeCommandesAttente
        public ObservableCollection<Livres> ListeCommandesAttente
        {
            private set;
            get;
        }// fin propriété ObservableCollection

        // Propriété ObservableCollection pour ListeCommandesTraitee
        public ObservableCollection<Livres> ListeCommandesTraitee
        {
            private set;
            get;
        }// fin propriété ObservableCollection

        // Propriété ObservableCollection pour ListeAttenteAdmin
        public ObservableCollection<Livres> ListeAttenteAdmin
        {
            set;
            get;
        }// fin propriété ObservableCollection

        // Propriété ObservableCollection pour ListeTraiteeAdmin
        public ObservableCollection<Livres> ListeTraiteeAdmin
        {
            set;
            get;
        }// fin propriété ObservableCollection

        // Propriété Nom pour avoir le nom du membre
        public string Nom
        {
            private set;
            get;
        }// fin propriété Nom

        // Propriété Administrateur 
        public bool? Administrateur
        {
            set;
            get;
        }// fin propriété Administrateur

        /**
         * Constructeur Membres
         */
        public Membres(string nom, bool administrateur)
        {
            // Pour initialiser Nom
            Nom = nom;
            // Pour initialiser Administrateur
            Administrateur = administrateur;
            // Pour initialiser ListeLivres
            ListeLivres = new ObservableCollection<Livres>();
            // Pour initialiser ListeCommandesAttente
            ListeCommandesAttente = new ObservableCollection<Livres>();
            // Pour initialiser ListeCommandesTraitee
            ListeCommandesTraitee = new ObservableCollection<Livres>();
            // Pour initialiser ListeAttenteAdmin
            ListeAttenteAdmin = new ObservableCollection<Livres>();
            // Pour initialiser ListeTraiteeAdmin
            ListeTraiteeAdmin = new ObservableCollection<Livres>();
        }// fin constructeur Membres

        // Constructeur Membres
        public Membres(XmlElement element, Dictionary<string, Livres> dictionnaire)
        {
            // Pour initialiser Nom
            Nom = element.GetAttribute("nom");
            // Pour initialiser ListeLivres
            ListeLivres = new ObservableCollection<Livres>();
            // Pour initialiser _dictionnaire
            _dictionnaire = dictionnaire;
            // Appelle a la methode DeXML
            DeXML(element); 
        }// fin constructeur Membres


        public XmlElement VersXML(XmlDocument doc)
        {
            // Création du XmlElement
            XmlElement elementMembre = doc.CreateElement("membre");
            // Définit attribut nom
            elementMembre.SetAttribute("nom", Nom);
            // Définit attribut administrateur
            elementMembre.SetAttribute("administrateur", Administrateur.ToString());


            //Source: Inspirer du Module 7, exercice 2
            // Pour boucler a travers ListeLivres
            foreach (Livres livre in ListeLivres) 
            {
                // variable de type string pour prendre le isbn du livre
                string isbnLivre = livre.ISBN;
                // Création du XmlElement
                XmlElement nouveauLivre = doc.CreateElement("livre");
                // Définit attribut
                nouveauLivre.SetAttribute("ISBN-13", isbnLivre);
                // Pour ajouter element sous membre
                elementMembre.AppendChild(nouveauLivre);
            }// fin boucle foreach

            // Pour boucler a travers ListeCommandesAttente
            foreach (Livres attenteLivre in ListeCommandesAttente)
            {
                // variable de type string pour prendre le isbn du livre
                string isbnCommande = attenteLivre.ISBN;
                // Création du XmlElement
                XmlElement nouveauCommande = doc.CreateElement("commande");
                // Définit attribut
                nouveauCommande.SetAttribute("statut", "Attente");
                // Définit attribut
                nouveauCommande.SetAttribute("ISBN-13", isbnCommande);
                // Pour ajouter element sous membre
                elementMembre.AppendChild(nouveauCommande);
            }// fin boucle foreach

            // Pour boucler a travers ListeCommandesTraitee
            foreach (Livres traiteeLivre in ListeCommandesTraitee)
            {
                // variable de type string pour prendre le isbn du livre
                string isbnCommande = traiteeLivre.ISBN;
                // Création du XmlElement
                XmlElement nouveauCommande = doc.CreateElement("commande");
                // Définit attribut
                nouveauCommande.SetAttribute("statut", "Traitee");
                // Définit attribut
                nouveauCommande.SetAttribute("ISBN-13", isbnCommande);
                // Pour ajouter element sous membre
                elementMembre.AppendChild(nouveauCommande);
            }// fin boucle foreach

            // Retourne elementMembre
            return elementMembre;
        }// fin methode VersXML

        public void DeXML(XmlElement elem)
        {
            // Pour initialiser ListeCommandesAttente
            ListeCommandesAttente = new ObservableCollection<Livres>();
            // Pour initialiser ListeCommandesTraitee
            ListeCommandesTraitee = new ObservableCollection<Livres>();

            // Pour prendre les elements "livre"
            XmlNodeList lesLivres = elem.GetElementsByTagName("livre");
            // Pour boucler a travers lesLivres
            foreach (XmlElement elementLivre in lesLivres)
            {
                // Pour ajouter le livre 
                ListeLivres.Add(_dictionnaire[elementLivre.GetAttribute("ISBN-13")]);
            }// fin boucle foreach
        }// fin methode DeXML

        /**
         * Methode toString pour retourner le nom des membres
         */
        public override string ToString()
        {
            // Retourne le nom du membre
            return Nom;
        }// fin methode ToString

        /**
         * Methode AjouterLivre pour ajouter un nouveau livre dans la ListeCommandesAttente
         */
        public void AjouterLivre(string leISBN, string Titre, string Auteur, string Editeur, int Annee)
        {
            // Pour ajouter le livre dans ListeCommandesAttente
            ListeCommandesAttente.Add(new Livres(leISBN, Titre, Auteur, Editeur, Annee));
        }// fin methode AjouterLivre

        /**
         * Methode RetirerLivre pour supprimer un livre de liste attente
         */
        public void RetirerLivre(string isbn)
        {
            // Variable de type int initialisé à 0
            int index = 0;
            // Variable de type boolean initialisé à false 
            bool chercher = false;

            // Pour boucler a travers ListeCommandesAttente
            while (index < ListeCommandesAttente.Count && !chercher)
            {
                // Condition if qui vérifie si un livre en attente a le meme ISBN que celui indiquer
                if (ListeCommandesAttente[index].ISBN.Equals(isbn))
                {
                    // Si vrai, le livre est supprimer du liste
                    ListeCommandesAttente.RemoveAt(index);
                    // variable chercher mis a true
                    chercher = true;
                }// fin condition if

                // Augmente la valeur de index par 1
                index++;
            }// fin boucle while
        }// fin methode RetirerLivre

        /**
         * Methode TransfererLivre pour transferer un livre a un autre membre
         * Ce code n'est pas complet
         */

        //public void TransfererLivre(string leISBN, string Titre, string Auteur, string Editeur, int Annee)
        //{
        //    ListeLivres.Add(new Livres(leISBN, Titre, Auteur, Editeur, Annee));
        //}
    }
}
