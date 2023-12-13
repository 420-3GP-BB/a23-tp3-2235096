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
        // RoutedCommand pour changer le membre
        public static RoutedCommand ChangerMembre = new RoutedCommand();
        // RoutedCommand pour ouvrir le fenetre Adminsitrateur
        public static RoutedCommand ModeAdmin = new RoutedCommand();
        // RoutedCommand pour quitter le program
        public static RoutedCommand Quitter = new RoutedCommand();
        // RoutedCommand pour afficher les livres
        public static RoutedCommand AfficherLivres = new RoutedCommand();
        // RoutedCommand pour ouvrir la fenetre de commande
        public static RoutedCommand CommanderLivre = new RoutedCommand();
        // RoutedCommand pour supprimer un livre en attente
        public static RoutedCommand RetirerLivre = new RoutedCommand();
        // RoutedCommand pour transferer un livre
        public static RoutedCommand TransfererLivre = new RoutedCommand();

        // Séparateur de répertoire de fichier
        private char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        // Chemin du fichier xml
        private string pathFichier;
        // Instance _viewModel 
        private ViewModelMembres _viewModel;

        public MainWindow()
        {
            // Donne le chemin du fichier xml
            pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                          DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "bibliotheque.xml";
            // Pour initialiser _viewModel
            _viewModel = new ViewModelMembres();
            // Pour initialiser la fenetre principale
            InitializeComponent();
            _viewModel.ChargerMembresEtLivres(pathFichier);
            // Pour afficher les livres du utilisateur active dans listBox 
            ListBoxLivres.DataContext = _viewModel;
            // Pour afficher le nom du utilisateur active
            dernierUtilisateur.DataContext = _viewModel;
            // Pour afficher les livres en attente du utilisateur active
            ListeBoxCommandesAttente.DataContext = _viewModel;
            // Pour afficher les livres traitée du utilisateur active
            ListeBoxCommandesTraitee.DataContext = _viewModel;
        }// fin MainWindow

        /**
         * Methode ChangerMembre_Executed qui permet de ouvrir la fenetre choix utilisateur
         */
        private void ChangerMembre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Création du instance de fenetre pour changer de utilisateur qui prend en parametre le _viewModel
            FenetreChoixUtilisateur fenetreChoixUtilisateur = new FenetreChoixUtilisateur(_viewModel);
            // Pour afficher la fenetre
            fenetreChoixUtilisateur.Show();
        }// fin methode ChangerMembre_Executed

        /**
         * Methode ChangerMode_Executed qui permet de ouvrir la fenetre administration
         */
        private void ChangerMode_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Création du instance de fenetre pour afficher les livres en attente et traitée dans fenetre admin qui prend en parametre le _viewModel
            FenetreAdministration fenetreAdministration = new FenetreAdministration(_viewModel);
            // Pour afficher la fenetre
            fenetreAdministration.Show();
        }// fin methode ChangerMode_Executed

        /**
         * Methode ChangerMode_CanExecute qui détermine si la fenetreAdministration peut etre exécutée.
         */
        private void ChangerMode_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Pour indiquer que la fenetre peut etre exécuté si ModeAdmin est true
            e.CanExecute = _viewModel.ModeAdmin == true;
        }// fin methode ChangerMode_CanExecute

        /**
         * Methode Quitter_Executed qui quitte le program
         */
        private void Quitter_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Ferme les fenetre et arrete le exécution
            Close();
        }// fin methode Quitter_Executed

        /**
         * Methode CommanderLivre_Executed qui ouvre la fenetre pour ajouter un livre 
         */
        private void CommanderLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Création du instance de fenetre commander livre , prend en parametre le _viewModel
            FenetreCommandeLivre fenetreCommandeLivre = new FenetreCommandeLivre(_viewModel);
            // Ouvre la fenetre
            fenetreCommandeLivre.Show();
        }// fin methode CommanderLivre_Executed

        /**
         * Methode RetirerLivre_CanExecute qui détermine si le bouton peut etre exécutée.
         */
        private void RetirerLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Pour indiquer que le bouton peut etre exécuté si selectedItem dans listBox n'est pas null
            e.CanExecute = ListeBoxCommandesAttente.SelectedItem != null;
        }// fin methode RetirerLivre_CanExecute

        /**
         * Methode RetirerLivre_Executed qui permet de retirer les livres
         */
        private void RetirerLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Appelle la maethode RetirerLivres
            _viewModel.RetirerLivres(ListeBoxCommandesAttente.SelectedItem);
        }// fin methode RetirerLivre_Executed

        /**
         * Methode TransfererLivre_CanExecute détermine si le bouton peut etre exécutée.
         */
        public void TransfererLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Pour indiquer que le bouton peut etre exécuté si selectedItem dans listBox n'est pas null
            e.CanExecute = ListBoxLivres.SelectedItem != null;
        }// fin methode TransfererLivre_CanExecute

        /**
        * Methode pour transferer des livres. Ce code est incomplet.
        */
        public void TransfererLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Création du instance de fenetre choixUtilisateur, prend en parametre le _viewModel
            FenetreChoixUtilisateur fenetreChoixUtilisateur = new FenetreChoixUtilisateur(_viewModel);
            // Affiche la fenetre
            fenetreChoixUtilisateur.Show();

            //Ce code est pour transfer le livre mais incomplet
            //_viewModel.TransfererLivre(ListBoxLivres.SelectedItem);
        }// fin methode TransfererLivre_Executed

    }// fin class MainWindow
}// fin namespace View