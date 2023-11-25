using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Livres
    {
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

        public Livres( string titre, string auteur, string editeur, int annee)
        {
            Titre = titre;
            Auteur = auteur;
            Editeur = editeur;
            Annee = annee;
        }

        public override string ToString()
        {
            return Titre;
        }
    }
}
