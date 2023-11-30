using System.Collections.ObjectModel;
using System.ComponentModel;
using Model;

namespace ViewModel
{
    public class ViewModelMembres : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        //private char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        private ModelMembres _model;
        private string? _nomFichier;

        public Membres? MembresActive
        {
            get => _membresActive;
            set
            {
                if (_membresActive != value)
                {
                    _membresActive = value;
                    OnPropertyChange(nameof(MembresActive));
                }
            }
            
        }
        private Membres? _membresActive; //Source: chatgpt



        // Maybe will need this

        //public string DernierUser
        //{
        //    get;
        //    set;
        //}



        public ObservableCollection<Membres> LesMembres
        {
            get
            {
                return _model.LesMembres;
            }
        }

        public ObservableCollection<string>? LesLivres
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

        public ViewModelMembres()
        {
            _model = new ModelMembres();
            MembresActive = null;
            _nomFichier = null;
            
            //_nomFichier = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            //DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "bibliotheque.xml";

            //_model.ChargerFichierXml(_nomFichier);
        }


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

        //public void ChangerMembres(Object obj)
        //{
        //    MembresActive = obj as Membres;
        //    OnPropertyChange("");
        //}

        //public void ChangerMembres(Object obj)
        //{
        //    MembresActive = obj as Membres;
        //    OnPropertyChange("");
        //}

        private void OnPropertyChange(string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
