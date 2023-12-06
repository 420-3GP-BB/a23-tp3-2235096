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

namespace View
{
    /// <summary>
    /// Interaction logic for FenetreCommandeLivre.xaml
    /// </summary>
    public partial class FenetreCommandeLivre : Window
    {
        public static RoutedCommand ConfirmerBouton = new RoutedCommand();
        public FenetreCommandeLivre()
        {
            InitializeComponent();
        }

        private void ConfirmerLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void ConfirmerLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ISBN.Text != "" && Titre.Text != "" && Auteur.Text != ""
             && Éditeur.Text != "";
        }
    }
}
