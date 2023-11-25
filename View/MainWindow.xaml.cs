﻿using System.Text;
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
        //private char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        //private string pathFichier;

        //private ViewModelMembres _viewModel;
        public MainWindow()
        {
            //pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            //DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "bibliotheque.xml";
            //_viewModel = new ViewModelMembres();
            InitializeComponent();
            //DataContext = _viewModel;

        }

        private void ChangerMembre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FenetreChoixUtilisateur fenetreChoixUtilisateur = new FenetreChoixUtilisateur();
            fenetreChoixUtilisateur.Show();
        }

        private void ChangerMode_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FenetreAdministration fenetreAdministration = new FenetreAdministration();
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

    }
}