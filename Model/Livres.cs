using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class Livres
    {
        // Propriété pour le ISBN du livre
        public string ISBN
        {
            //setter
            private set; 
            //getter
            get;
        }// fin propriété isbn

        // Propriété pour le titre du livre
        public string Titre
        {
            //setter
            private set;
            //getter
            get;
        }// fin propriété Titre

        // Propriété pour le Auteur du livre
        public string Auteur
        {
            //setter
            private set;
            //getter
            get;
        }// fin propriété Auteur

        // Propriété pour le Editeur du livre
        public string Editeur
        {
            //setter
            private set;
            //getter
            get;
        }// fin propriété Editeur

        // Propriété pour le Annee du livre
        public int Annee
        {
            //setter
            private set;
            //getter
            get;
        }// fin propriété Annee

        /**
         * Constructeur Livres
         */
        public Livres(string isbn , string titre, string auteur, string editeur, int annee)
        {
            // Pour initialiser ISBN
            ISBN = isbn;
            // Pour initialiser Titre
            Titre = titre;
            // Pour initialiser Auteur
            Auteur = auteur;
            // Pour initialiser Editeur
            Editeur = editeur;
            // Pour initialiser Annee
            Annee = annee;
        }// fin constructeur Livres

        /**
         * Methode toString qui retourne le Titre, auteur et le annee du livre
         */
        public override string ToString()
        {
            //Retourne titre, auteur et annee du livre
            return $"{Titre}, {Auteur} ({Annee})";
        }// fin methode toString
    }// fin class Livres
}// fin namespace Model
