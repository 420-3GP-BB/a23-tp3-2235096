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
        private ViewModelMembres _viewModel;
        public FenetreChoixUtilisateur(ViewModelMembres viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void ComboBoxUtilisateur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BoutonConfirmer_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxMembres.SelectedItem != null)
            {
                _viewModel.ChangerMembres(ComboBoxMembres.SelectedItem);
                Close();
            }
        }
    }
}
