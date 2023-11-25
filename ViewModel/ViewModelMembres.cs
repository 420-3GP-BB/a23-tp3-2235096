using System.Collections.ObjectModel;
using System.ComponentModel;
using Model;

namespace ViewModel
{
    public class ViewModelMembres : INotifyPropertyChanged
    {
        private char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        private string _nomFichier;
        private ModelMembres _model;

        public ViewModelMembres()
        {
            _nomFichier = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "bibliotheque.xml";
            _model = new ModelMembres();
            _model.ChargerFichierXml(_nomFichier);
        }

        public ObservableCollection<Membres> LesMembres
        {
            get
            {
                return _model.LesMembres;  
            }
        }

        public Membres? MembresActive
        {
            set;
            get;
        }

        public ObservableCollection<Livres>? LesLivres
        {
            get
            {
                if (MembresActive == null)
                {
                    return null;
                }
                else
                {
                    return MembresActive.LesLivres;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void ChargerMembres(string nomFichier)
        {
            _nomFichier = nomFichier;
            _model.ChargerFichierXml(_nomFichier);
            if (LesMembres != null && LesMembres.Count > 0)
            {
                MembresActive = LesMembres[0];
            }
            OnPropertyChange("");
        }

        public void ChangerMembres(Object obj)
        {
            MembresActive = obj as Membres;
            OnPropertyChange("");
        }

        private void OnPropertyChange(string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
