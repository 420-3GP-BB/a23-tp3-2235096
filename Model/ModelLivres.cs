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
        private Dictionary<string, Livres> lesLivresDictionnaire;

        public ObservableCollection<Livres>? ListeLivres
        {
            private set;
            get;
        }

        public Dictionary<string, Livres> DicLivres
        {
            set
            {
                lesLivresDictionnaire = value;
            }
            get => lesLivresDictionnaire;
        }

        public ModelLivres()
        {
            lesLivresDictionnaire = new Dictionary<string, Livres>();
            ListeLivres = new ObservableCollection<Livres>();
        }

        //Source: chatgpt
        public void AjouterLivres (string isbn, Livres livre)
        {
            if (!lesLivresDictionnaire.ContainsKey(isbn))
            {
                lesLivresDictionnaire[isbn] = livre;
                ListeLivres.Add(livre);
            }
        }

        public void ChargerLivres(string nomFichier)
        {
            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlElement racine = document.DocumentElement;

            XmlElement unNoeudLivre = racine["livres"];
            XmlNodeList LivresXML = unNoeudLivre.GetElementsByTagName("livre");

            foreach (XmlElement livres in LivresXML)
            {
                string isbn = livres.GetAttribute("ISBN-13");
                //Source: Chatgpt
                string titre = livres.SelectSingleNode("titre").InnerText;
                string auteur = livres.SelectSingleNode("auteur").InnerText;
                string editeur = livres.SelectSingleNode("editeur").InnerText;
                int annee = int.Parse(livres.SelectSingleNode("annee").InnerText);
                
                
                Livres unLivre = new Livres(isbn, titre, auteur, editeur, annee);
                AjouterLivres(isbn, unLivre);
            }

        }
    }
}
