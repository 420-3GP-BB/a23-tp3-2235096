using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class ModelLivres
    {
        private Dictionary<string, Livres> lesLivresDictionnaire;

        public ModelLivres()
        {
            lesLivresDictionnaire = new Dictionary<string, Livres>();
        }

        //Source: chatgpt
        public void AjouterLivres (string isbn, Livres livre)
        {
            if (!lesLivresDictionnaire.ContainsKey(isbn))
            {
                lesLivresDictionnaire[isbn] = livre;
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
                //LesLivres.Add(new Livres(livres));
            }
        }
    }
}
