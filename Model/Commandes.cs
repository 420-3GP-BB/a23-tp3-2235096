using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Commandes
    {
        public string Statut 
        { 
            get;
            set;
        }
        public string ISBN_Livre 
        { 
            get;
            set;
        }

        
        public Commandes(string statut, string isbn_livre) 
        {
            Statut = statut;
            ISBN_Livre = isbn_livre;
        }

        public override string ToString()
        {
            return Statut;
        }
    }
}
