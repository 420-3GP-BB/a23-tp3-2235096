using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;

namespace Model
{
    public class ModelMembres
    {
        
        public ObservableCollection<Membres> LesMembres
        {
            private set;
            get;
        }

        public ModelMembres()
        {
            LesMembres = new ObservableCollection<Membres>();
            LesLivres = new ObservableCollection<Livres>();
        }

        public ObservableCollection<Livres>? LesLivres
        {
            private set;
            get;
        }

        public void ChargerFichierXml(string nomFichier)
        {
            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlElement racine = document.DocumentElement;

            XmlElement unNoeud = racine["membres"];
            XmlNodeList lesMembresXML = unNoeud.GetElementsByTagName("membre");

            foreach (XmlElement unElement in lesMembresXML)
            {
                LesMembres.Add(new Membres(unElement));
            }
        }

        public void SauvegarderFichierXml(string nomFichier)
        {
            XmlDocument document = new XmlDocument();
            XmlElement racine = document.CreateElement("bibliotheque");
            document.AppendChild(racine);

            XmlElement elementMembre = document.CreateElement("membres");
            racine.AppendChild(elementMembre);

            foreach (Membres unMembre in LesMembres)
            {
                XmlElement element = unMembre.VersXML(document);
                elementMembre.AppendChild(element);
            }
            document.Save(nomFichier);
        }
    }
}
