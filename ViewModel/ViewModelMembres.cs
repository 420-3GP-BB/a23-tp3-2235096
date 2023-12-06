using System.Collections.ObjectModel;
using System.ComponentModel;
using Model;

namespace ViewModel
{
    public class ViewModelMembres : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ModelLivres _modelLivres;
        // Variable privé de type ModelMembres
        private ModelMembres _model;
        // Variable de type string pour chemin du fichier xml
        private string? _nomFichier;

        public Membres? MembresActive
        {
            //Envoi la valeur actuelle de _membreActive
            get => _membresActive;
            set
            {
                //Condition pour voir si la la propriété a changé
                if (_membresActive != value)
                {
                    //Si vrai, attribut la nouvelle valeur
                    _membresActive = value; 
                    OnPropertyChange(nameof(MembresActive));
                }
            }
            
        }
        // variable privé pour stocké valeur de MembresActive
        private Membres? _membresActive; //Source: chatgpt



        // Maybe will need this

        //public string DernierUser
        //{
        //    get;
        //    set;
        //}



        public ObservableCollection<Membres> ListeMembres
        {
            get
            {
                return _model.ListeMembres;
            }
        }

        public ObservableCollection<Livres>? ListeLivres
        {
            get
            {
                if (MembresActive == null)
                {
                    return null;
                }
                else
                {
                    return MembresActive.ListeLivres;
                    //return _modelLivres.ListeLivres; //Pour afficher tout les livres
                }
            }
        }

        public ObservableCollection<Commandes> ListeCommandesAttente
        {
            get
            {
                if (MembresActive == null)
                {
                    return null;
                }
                else
                {
                    return MembresActive.ListeCommandesAttente;
                }
            }
        }

        public ObservableCollection<Commandes> ListeCommandesTraitee
        {
            get
            {
                if (MembresActive == null)
                {
                    return null;
                }
                else
                {
                    return MembresActive.ListeCommandesTraitee;
                }
            }
        }



        public ViewModelMembres()
        {
            _modelLivres = new ModelLivres();
            _model = new ModelMembres(_modelLivres.DicLivres);
            MembresActive = null;
            _nomFichier = null;
        }


        public void ChargerMembresEtLivres(string nomFichier)
        {
            _nomFichier = nomFichier;
            _modelLivres.ChargerLivres(_nomFichier);
            _model.ChargerMembresXml(_nomFichier);
            

            if (ListeMembres != null && ListeMembres.Count > 0)
            {
                MembresActive = ListeMembres[0];
            }
            OnPropertyChange("");

        }

        public void ChangerMembres(Object obj)
        {
            MembresActive = obj as Membres;
            OnPropertyChange("");
        }

        //public void ChangerMembres(Object obj)
        //{
        //    MembresActive = obj as Membres;
        //    OnPropertyChange("");
        //}

        private void OnPropertyChange(string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void SauvegarderModification()
        {
            if (_nomFichier != null)
            {
                _model.SauvegarderFichierXml( _nomFichier );
            }
        }
    }
}
