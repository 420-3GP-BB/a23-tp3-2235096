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
        // RoutedCommand pour changer le membre
        public static RoutedCommand ConfirmerBouton = new RoutedCommand();
        private ViewModelMembres _viewModel;
        public FenetreCommandeLivre(ViewModelMembres viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void ConfirmerLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int AnneeLivre;
            if (int.TryParse(Annee.Text, out AnneeLivre))
            {
                 _viewModel.AjouterLivre(ISBN.Text, Titre.Text, Auteur.Text, Éditeur.Text, AnneeLivre);
            }
            Close();
        }

        private void ConfirmerLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            int AnneeLivre;
            if (int.TryParse(Annee.Text, out AnneeLivre))
            {
                e.CanExecute = ISBN.Text.Length == 13 && Titre.Text != "" && Auteur.Text != ""
                && Éditeur.Text != "" && AnneeLivre > -3000;
            }
        }
    }
}
