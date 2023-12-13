using Model;
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
    /// Interaction logic for FenetreChoixUtilisateur.xaml
    /// </summary>
    public partial class FenetreChoixUtilisateur : Window
    {
        // Instance _viewModel 
        private ViewModelMembres _viewModel;
        public FenetreChoixUtilisateur(ViewModelMembres viewModel)
        {
            // Pour initialiser _viewModel
            _viewModel = viewModel;
            // Pour initialiser la fenetre
            InitializeComponent();
            // Pour initialiser DataContext
            DataContext = _viewModel;
        }// fin FenetreChoixUtilisateur

        /**
         * Methode BoutonConfirmer_Click pour confirmer le membre choisi
         */
        private void BoutonConfirmer_Click(object sender, RoutedEventArgs e)
        {
            // Condition if pour voir si un membre est selectioner ou pas
            if (ComboBoxMembres.SelectedItem != null)
            {
                // Appelle la methode ChangerMembres
                _viewModel.ChangerMembres(ComboBoxMembres.SelectedItem);
                // Pour fermer la fenetre
                Close();
            }// fin condition if
        }// fin methode BoutonConfirmer_Click
    }// fin class FenetreChoixUtilisateur
} // fin namespace View
