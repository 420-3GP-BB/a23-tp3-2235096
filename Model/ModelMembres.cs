using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;

namespace Model
{
    public class ModelMembres
    {
        private Dictionary<string, Livres> _dictionnaire;
        public ObservableCollection<Membres> ListeMembres
        {
            private set;
            get;
        }

        public ModelMembres(Dictionary<string, Livres> dictionnaire)
        {
            ListeMembres = new ObservableCollection<Membres>();
            _dictionnaire = dictionnaire;

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
                ListeMembres.Add(new Membres(unElement, _dictionnaire));
            }
        }

        public void SauvegarderFichierXml(string nomFichier)
        {
            XmlDocument document = new XmlDocument();
            XmlElement racine = document.CreateElement("bibliotheque");
            document.AppendChild(racine);

            XmlElement elementMembre = document.CreateElement("membres");
            racine.AppendChild(elementMembre);

            foreach (Membres unMembre in ListeMembres)
            {
                XmlElement element = unMembre.VersXML(document);
                elementMembre.AppendChild(element);
            }
            document.Save(nomFichier);
        }
    }
}
