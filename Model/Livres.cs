using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Livres
    {
        public string ISBN
        {
            private set; 
            get;
        }
        public string Titre
        {
            private set;
            get;
        }

        public string Auteur
        {
            private set;
            get;
        }

        public string Editeur
        {
            private set;
            get;
        }

        public int Annee
        {
            private set;
            get;
        }

        //public ObservableCollection<Livres> ListeCommandesAttente
        //{
        //    private set;
        //    get;
        //}

        //public ObservableCollection<Livres> ListeCommandesTraitee
        //{
        //    private set;
        //    get;
        //}

        public Livres(string isbn , string titre, string auteur, string editeur, int annee)
        {
            ISBN = isbn;
            Titre = titre;
            Auteur = auteur;
            Editeur = editeur;
            Annee = annee;
        }

        public override string ToString()
        {
            return $"{Titre}, {Auteur} ({Annee})";
            
        }
    }
}
