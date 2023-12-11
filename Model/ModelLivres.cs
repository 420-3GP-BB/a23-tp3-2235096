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

        
        public void AjouterLivres (string isbn, Livres livre)
        {
            //Source: chatgpt
            if (!lesLivresDictionnaire.ContainsKey(isbn))
            {
                lesLivresDictionnaire[isbn] = livre;
                ListeLivres.Add(livre);
            }
        }

        //public Livres? AvoirLivre(string isbn)
        //{
        //    lesLivresDictionnaire.TryGetValue(isbn, out var livre);
        //    return livre;
        //}

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

        //public void SauvegarderLivresXml(XmlDocument document, XmlElement racine)
        //{
        //    XmlElement elementLivres = document.CreateElement("livres");
        //    racine.AppendChild(elementLivres);

        //    XmlElement unElementLivre = document.CreateElement("livre");

        //}



        //public bool LivreExiste(string nomLivre)
        //{
        //    bool livrePresente = false;
        //    foreach (Livres livre in ListeLivres)
        //    {
        //        if (livre.Titre == nomLivre)
        //        {
        //            livrePresente = true;
        //            break;
        //        }
        //    }
        //    return livrePresente;
        //}

        //public Livres CreerLivre(string isbn, string nomLivre, string auteur, string editeur, int annee)
        //{
        //    Livres? newLivre = null;
        //    if (!LivreExiste(nomLivre))
        //    {
        //        newLivre = new Livres(isbn, nomLivre, auteur, editeur, annee);
        //        ListeLivres.Add(newLivre);
        //    }
        //    return newLivre;
        //}
    }
}
