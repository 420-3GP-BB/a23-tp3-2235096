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
    /// Interaction logic for FenetreAdministration.xaml
    /// </summary>
    public partial class FenetreAdministration : Window
    {
        // Instance _viewModel 
        private ViewModelMembres _viewModel;
        public FenetreAdministration(ViewModelMembres viewModel)
        {
            // Pour initialiser _viewModel
            _viewModel = viewModel;
            // Pour initialiser la fenetre
            InitializeComponent();
            // Pour initialiser DataContext
            DataContext = _viewModel;
            // Pour afficher les livres en attentes de tous les membres
            ListeCommandeAttente.DataContext = _viewModel;
            // Pour afficher les livres traitée de tous les membres
            ListeCommandeTraitee.DataContext = _viewModel;
        }// fin FenetreAdministration

        /**
         * Methode BoutonRevenir_Click pour fermer la fenetre
         */
        private void BoutonRevenir_Click(object sender, RoutedEventArgs e)
        {
            // Pour fermer la fenetre
            Close();
        }// fin methode BoutonRevenir_Click

    }
}
