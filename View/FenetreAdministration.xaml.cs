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
        private ViewModelMembres _viewModel;
        public FenetreAdministration(ViewModelMembres viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            DataContext = _viewModel;
            ListeCommandeAttente.DataContext = _viewModel;
            ListeCommandeTraitee.DataContext = _viewModel;
            
        }

        private void BoutonRevenir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
