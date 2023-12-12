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

        // variable privé pour stocké valeur de MembresActive
        private Membres? unMembreActive; //Source: chatgpt
        public Membres? MembresActive
        {
            //Envoi la valeur actuelle de _membreActive
            get => unMembreActive;
            set
            {
                //Condition pour voir si la la propriété a changé
                if (unMembreActive != value)
                {
                    //Si vrai, attribut la nouvelle valeur
                    unMembreActive = value; 
                    OnPropertyChange(nameof(MembresActive));
                }
            }
            
        }

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

        public ObservableCollection<Livres> ListeCommandesAttente
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

        public ObservableCollection<Livres> ListeCommandesTraitee
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

        public ObservableCollection<Livres> ListeAttenteAdmin
        {
            get
            {
                if (MembresActive == null)
                {
                    return null;
                }
                else
                {
                    return _model.ListeAttenteAdmin;
                }
            }
        }

        public ObservableCollection<Livres> ListeTraiteeAdmin
        {
            get
            {
                if (MembresActive == null)
                {
                    return null;
                }
                else
                {
                    return _model.ListeTraiteeAdmin;
                }
            }
        }

        //new code

        public bool ModeAdmin
        {
            get
            {
                if (MembresActive == null)
                {
                    return false;
                }
                else
                {
                    return (bool)MembresActive.Administrateur;
                }
            }
        }


        public ViewModelMembres()
        {
            _modelLivres = new ModelLivres();
            _model = new ModelMembres(_modelLivres.DicLivres, _modelLivres);
            MembresActive = null;
            _nomFichier = null;
        }


        public void ChargerMembresEtLivres(string nomFichier)
        {
            _nomFichier = nomFichier;
            _modelLivres.ChargerLivres(_nomFichier);
            _model.ChargerMembresXml(_nomFichier);
            //_model.ChargerLesDonneesXml(_nomFichier);


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


        public void AjouterLivre(string leISBN, string Titre, string Auteur, string Editeur, int Annee)
        {
            if (MembresActive != null)
            {
                MembresActive.AjouterLivre(leISBN, Titre, Auteur, Editeur, Annee);
                SauvegarderModification();
            }
        }

        public void RetirerLivres(object livre)
        {
            if(MembresActive != null)
            {
                Livres leLivre = livre as Livres;
                MembresActive.RetirerLivre(leLivre.ISBN);
                SauvegarderModification();
            }
        }


        /**
         * Code pour transferer livre. Ce code n'est pas finit
         */

        //public void TransfererLivre(object livre)
        //{
            
        //    if(MembresActive != null)
        //    {
        //        AjouterLivre();
        //        Livres unLivre = livre as Livres;
        //        MembresActive.TransfererLivre(unLivre.ISBN, unLivre.Titre, unLivre.Auteur, unLivre.Editeur, unLivre.Annee);
        //        MembresActive.RetirerLivre();
        //        SauvegarderModification();
        //    }
        //}
    }
}
