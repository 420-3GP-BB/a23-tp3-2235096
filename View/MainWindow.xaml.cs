using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand ChangerMembre = new RoutedCommand();
        public static RoutedCommand ModeAdmin = new RoutedCommand();
        public static RoutedCommand Quitter = new RoutedCommand();
        public static RoutedCommand AfficherLivres = new RoutedCommand();
        public static RoutedCommand CommanderLivre = new RoutedCommand();
        public static RoutedCommand RetirerLivre = new RoutedCommand();
        public static RoutedCommand TransfererLivre = new RoutedCommand();


        private char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        private string pathFichier;

        private ViewModelMembres _viewModel;

        public MainWindow()
        { 
            InitializeComponent();
            pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "bibliotheque.xml";

            _viewModel = new ViewModelMembres();

            // Pour afficher les livres du utilisateur active dans listBox 
            ListBoxLivres.DataContext = _viewModel;
            _viewModel.ChargerMembresEtLivres(pathFichier);
            //Pour afficher le nom du utilisateur active
            dernierUtilisateur.DataContext = _viewModel;
            ListeBoxCommandesAttente.DataContext = _viewModel;
            ListeBoxCommandesTraitee.DataContext = _viewModel;
        }


        private void ChangerMembre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FenetreChoixUtilisateur fenetreChoixUtilisateur = new FenetreChoixUtilisateur(_viewModel);
            fenetreChoixUtilisateur.Show();
        }

        private void ChangerMode_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FenetreAdministration fenetreAdministration = new FenetreAdministration(_viewModel);
            fenetreAdministration.Show();
        }

        private void ChangerMode_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
                
        private void Quitter_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void CommanderLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FenetreCommandeLivre fenetreCommandeLivre = new FenetreCommandeLivre(_viewModel);
            fenetreCommandeLivre.Show();    
        }

        private void RetirerLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ListeBoxCommandesAttente.SelectedItem != null;
        }

        private void RetirerLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewModel.RetirerLivres(ListeBoxCommandesAttente.SelectedItem);
        }

        public void TransfererLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ListBoxLivres.SelectedItem != null;
        }

        public void TransfererLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FenetreChoixUtilisateur fenetreChoixUtilisateur = new FenetreChoixUtilisateur(_viewModel);
            fenetreChoixUtilisateur.Show();
        }

    }
}