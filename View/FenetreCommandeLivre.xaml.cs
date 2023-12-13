using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for FenetreCommandeLivre.xaml
    /// </summary>
    public partial class FenetreCommandeLivre : Window
    {
        // RoutedCommand pour ajouter livre
        public static RoutedCommand ConfirmerBouton = new RoutedCommand();
        // Instance _viewModel 
        private ViewModelMembres _viewModel;
        public FenetreCommandeLivre(ViewModelMembres viewModel)
        {
            // Pour initialiser _viewModel
            _viewModel = viewModel;
            // Pour initialiser la fenetre
            InitializeComponent();
            // Pour initialiser DataContext
            DataContext = _viewModel;
        }// fin FenetreCommandeLivre

        /**
         * Methode ConfirmerLivre_Executed pour ajouter un nouveau livre
         */
        private void ConfirmerLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Variable de type int pour stocké le année du livre
            int AnneeLivre;

            // Condition if qui verifie le variable annee pour que ca soit un nombre
            if (int.TryParse(Annee.Text, out AnneeLivre))
            {
                // Appelle la methode AjouterLivre
                _viewModel.AjouterLivre(ISBN.Text, Titre.Text, Auteur.Text, Éditeur.Text, AnneeLivre);
            }// fin condition if
            // Pour fermer la fenetre
            Close();
        }// fin methode ConfirmerLivre_Executed

        /**
         * Methode ConfirmerLivre_CanExecute qui détermine si le bouton peut etre exécuté pour ajouter un livre
         */
        private void ConfirmerLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Variable de type int pour stocké le année du livre
            int AnneeLivre;

            // Condition if qui verifie le variable annee pour que ca soit un nombre
            if (int.TryParse(Annee.Text, out AnneeLivre))
            {
                // Pour indiquer que le bouton peut etre exécuté si les conditions sont vrai
                e.CanExecute = ISBN.Text.Length == 13 && Titre.Text != "" && Auteur.Text != ""
                && Éditeur.Text != "" && AnneeLivre > -3000;
            }// fin condition if
        }// fin methode ConfirmerLivre_CanExecute
    }// fin class FenetreCommandeLivre
}// fin namespace View
