﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using Model;

namespace ViewModel
{
    public class ViewModelMembres : INotifyPropertyChanged
    {
        // Event pour changement du propriété
        public event PropertyChangedEventHandler? PropertyChanged;
        // Instance pour les livres
        private ModelLivres _modelLivres;
        // Instance pour les mmembres
        private ModelMembres _model;
        // Propriété de type string pour chemin du fichier xml
        private string? _nomFichier;

        // Instance privé pour stocké valeur de MembresActive
        private Membres? unMembreActive; //Source: Inspirer du chatgpt
        
        // Propriété qui va aller chercher le membre active et le modifier
        public Membres? MembresActive
        {
            // Envoi la valeur actuelle de _membreActive
            get => unMembreActive;
            set
            {
                // Condition pour voir si la la propriété a changé
                if (unMembreActive != value)
                {
                    // Si vrai, attribut la nouvelle valeur
                    unMembreActive = value; 
                    OnPropertyChange(nameof(MembresActive));
                } // Fin condition if
            } //fin set
        } // fin propriété MembresActive

        // Propriété Observable liste pour les membres
        public ObservableCollection<Membres> ListeMembres
        {
            get
            {
                // Retourne liste des membres
                return _model.ListeMembres;
            } //fin get
        } // fin propriété


        // Propriété Observable des livres
        public ObservableCollection<Livres>? ListeLivres
        {
            get
            {
                // Condition if qui vérifie si membre active est null
                if (MembresActive == null)
                {
                    // Si vrai, retourne null
                    return null;
                }
                else
                {
                    // Sinon, retourne les livres du membres active 
                    return MembresActive.ListeLivres;
                }// fin condition if
            }
        }// fin propriété Observable des livres

        // Propriété Observable des livres pour les commandes en attente
        public ObservableCollection<Livres> ListeCommandesAttente
        {
            get
            {
                // Condition if qui vérifie si membre active est null
                if (MembresActive == null)
                {
                    // Si vrai, retourne null
                    return null;
                }
                else
                {
                    // Sinon, retourne les livres en attente du membre active
                    return MembresActive.ListeCommandesAttente;
                }// fin condition if
            }
        }// fin propriété Observable des livres en attente

        // Propriété Observable des livres pour les commandes traitee
        public ObservableCollection<Livres> ListeCommandesTraitee
        {
            get
            {
                // Condition if qui vérifie si membre active est null
                if (MembresActive == null)
                {
                    // Si vrai, retourne null
                    return null;
                }
                else
                {
                    // Sinon, retourne les livres traitee du membre active
                    return MembresActive.ListeCommandesTraitee;
                } //fin condition if
            }
        }// fin propriété Observable des livres traitee

        // Propriété Observable des livres pour les commandes en attente pour le mode admin
        public ObservableCollection<Livres> ListeAttenteAdmin
        {
            get
            {
                // Condition if qui vérifie si membre active est null
                if (MembresActive == null)
                {
                    // Si vrai, retourne null
                    return null;
                }
                else
                {
                    // Sinon, retourne les livres en attente de toutes les membres
                    return _model.ListeAttenteAdmin;
                } //fin condition if 
            }
        } // fin propriété Observable des livres en attente pour fenetre admin

        // Propriété Observable des livres pour les commandes traitee pour le mode admin
        public ObservableCollection<Livres> ListeTraiteeAdmin
        {
            get
            {
                // Condition if qui vérifie si membre active est null
                if (MembresActive == null)
                {
                    // Si vrai, retourne null
                    return null;
                }
                else
                {
                    // Sinon, retourne les livres traitee de toutes les membres
                    return _model.ListeTraiteeAdmin;
                } //fin conditio if
            }
        } // fin propriété Observable des livres en attente pour fenetre admin

        // Propriété pour avoir si le membre est un admin ou non
        public bool ModeAdmin
        {
            get
            {
                // Condition if qui vérifie si membre active est null
                if (MembresActive == null)
                {
                    // Si vrai, retourne null
                    return false;
                }
                else
                {
                    // Sinon, retourne la valeur de type bool
                    return (bool)MembresActive.Administrateur;
                }// fin condition if 
            }
        } // fin propriété ModeAdmin 

        /**
         * Constructeur ViewModelMembres
         */
        public ViewModelMembres()
        {
            // Initialise _modelLivres
            _modelLivres = new ModelLivres();
            // Initialise _model avec dictionnaire et _modelLivres en parametre
            _model = new ModelMembres(_modelLivres.DicLivres, _modelLivres);
            // Initialise MembresActive
            MembresActive = null;
            // Initialise _nomFichier
            _nomFichier = null;
        }// fin constructeur ViewModelMembres

        /**
         * Methode ChargerMembresEtLivres qui appel des methodes pour charger 
         * les livres et les membres du fichier Bibliotheque
         */
        public void ChargerMembresEtLivres(string nomFichier)
        {
            // Pour donner le nom du fichier a _nomFichier
            _nomFichier = nomFichier;
            // Pour charger les livres
            _modelLivres.ChargerLivres(_nomFichier);
            // Pour charger les membres
            _model.ChargerMembresXml(_nomFichier);

            // Condition if pour verifier si ListeMembres n'est pas null et ListeMembres n'est pas vide
            if (ListeMembres != null && ListeMembres.Count > 0)
            {
                //Si vrai, le premier membre est charger dans MembresActive
                MembresActive = ListeMembres[0];
            } // fin condition if

            // Appelle a la methode OnPropertyChange
            OnPropertyChange("");

        }// fin methode ChargerMembresEtLivres

        /**
         * Methode ChangerMembres pour changer les utilisateurs
         */

        public void ChangerMembres(Object obj)
        {
            // Source: Inspirer du module 7, exercice 2

            // Pour convertir objet en Membre
            MembresActive = obj as Membres;
            // Appelle a la methpde OnPropertyChange
            OnPropertyChange("");
        }// fin methode ChangerMembres

        /**
         * Methode OnPropertyChange pour le event PropertyChanged 
         */
        private void OnPropertyChange(string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }// fin methode OnPropertyChange

        /**
         * Methode SauvegarderModification pour sauvegarder les modifications
         * dans le fichier bibliotheque. La methode fais un sauvegarde automatique
         */
        public void SauvegarderModification()
        {
            // Condition if pour verifier si _nomFichier est pas null
            if (_nomFichier != null)
            {
                // Appelle le methode SauvegarderFichierXml
                _model.SauvegarderFichierXml( _nomFichier );
            }// fin condition if
        } //fin methode SauvegarderModification


        /**
         * Methode AjouterLivre pour ajouter des nouveau livre a un membre.
         * La methode fais un sauvegarde automatique.
         */
        public void AjouterLivre(string leISBN, string Titre, string Auteur, string Editeur, int Annee)
        {
            // Condition if pour voire si MembresActive est pas null
            if (MembresActive != null)
            {
                // Si vrai, appelle a la methode AjouterLivre du classe Membres
                MembresActive.AjouterLivre(leISBN, Titre, Auteur, Editeur, Annee);
                // Appelle a la methode SauvegarderModification pour faire sauvegarde automatique
                SauvegarderModification();
            }// fin condition if
        }// fin methode AjouterLivre

        /**
         * Methode RetirerLivres pour supprimer un livre du membre.
         * Ce methode fais un sauvegarde automatique
         */
        public void RetirerLivres(object livre)
        {
            // Condition if pour voire si MembresActive est pas null
            if (MembresActive != null)
            {
                // Pour convertir livre en Livres
                Livres leLivre = livre as Livres;
                // Pour faire un appelle a la methode RetirerLivre dans classe Membres
                MembresActive.RetirerLivre(leLivre.ISBN);
                // Appelle a la methode SauvegarderModification pour faire un sauvegarde automatique
                SauvegarderModification();
            }// fin condition if
        }// fin methode RetirerLivres


        /**
         * Methode TransfererLivre pour transferer des livres entre les membres.
         * Ce code n'est pas complet
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

    } //fin class ViewModelMembres
}// fin namespace ViewModel
