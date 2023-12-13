using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class ModelLivres
    {
        // Dictionnaire qui va prendre les livres
        private Dictionary<string, Livres> lesLivresDictionnaire;

        // Propriété ObservableCollection pour ListeLivres
        public ObservableCollection<Livres>? ListeLivres
        {
            private set;
            get;
        }// fin propriété ObservableCollection

        // Propriété qui permet de acceder le dictionnaire pour les livres
        public Dictionary<string, Livres> DicLivres
        {
            // setter
            set
            {
                lesLivresDictionnaire = value;
            }
            // getter 
            get => lesLivresDictionnaire;
        }// fin propriété

        // Constructeur ModelLivres
        public ModelLivres()
        {
            // Pour initialiser lesLivresDictionnaire
            lesLivresDictionnaire = new Dictionary<string, Livres>();
            // Pour initialiser ListeLivres
            ListeLivres = new ObservableCollection<Livres>();
        }// fin constructeur ModelLivres

        /**
         * Methode AjouterLivres pour ajouter les livres dans le dictionnaire
         */
        public void AjouterLivres (string isbn, Livres livre)
        {
            // Condition if pour voire si le livre existe deja dans le dictionnaire
            if (!lesLivresDictionnaire.ContainsKey(isbn)) //Source: chatgpt
            {
                // Pour ajouter le livre dans le dictionnaire 
                lesLivresDictionnaire[isbn] = livre;
                // Pour ajouter le livre dans ListeLivres
                ListeLivres.Add(livre);
            }// fin condition if
        }// fin methode AjouterLivres

        /**
         * Methode ChargerLivres pour charger les livres à partir du fichier bibliotheque
         */
        public void ChargerLivres(string nomFichier)
        {
            // Création du objet XmlDocument
            XmlDocument document = new XmlDocument();
            // Pour charger le fichier xml
            document.Load(nomFichier);
            // Pour obtenir l'élément racine du xml
            XmlElement racine = document.DocumentElement;
            // Pour obtenir element "livres" avec la racine
            XmlElement unNoeudLivre = racine["livres"];
            // Pour prendre les éléments avec la balise "livre"
            XmlNodeList LivresXML = unNoeudLivre.GetElementsByTagName("livre");

            // Pour boucler a travers les livres
            foreach (XmlElement livres in LivresXML)
            {
                // Pour avoir le attribut ISBN-13 du livre
                string isbn = livres.GetAttribute("ISBN-13");

                //Source: Chatgpt (Pour les SelectSingleNode)
                // Pour prendre le texte de element titre 
                string titre = livres.SelectSingleNode("titre").InnerText;
                // Pour prendre le texte de element auteur 
                string auteur = livres.SelectSingleNode("auteur").InnerText;
                // Pour prendre le texte de element editeur 
                string editeur = livres.SelectSingleNode("editeur").InnerText;
                // Pour prendre le texte de element annee 
                int annee = int.Parse(livres.SelectSingleNode("annee").InnerText);
                
                // Pour crée un livre
                Livres unLivre = new Livres(isbn, titre, auteur, editeur, annee);
                // Appelle a la methode AjouterLivres pour ajouter le livre dans dictionnaire
                AjouterLivres(isbn, unLivre);
            }// fin boucle foreqch
        }// fin methode ChargerLivres

    }// fin class ModelLivres
}// fin namespace Model
