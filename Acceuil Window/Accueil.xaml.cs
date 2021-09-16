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

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for Accueil.xaml
    /// </summary>
    public partial class Accueil : Window
    {
        public Accueil()
        {
            if (Global.context.Utilisateurs.Any(u => u.isLogedIn == true))
            {
                Global.utilisateur=Global.context.Utilisateurs.Single(u => u.isLogedIn == true);
                Home.ShowWindow();
                this.Close();
            }
            InitializeComponent();
            this.Loaded += Accueil_Loaded;
        }

        private void Accueil_Loaded(object sender, RoutedEventArgs e)
        {
            Remplir_la_base_de_donne.Remplir();
        }

        private void TbPresentation_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new Presentation().ShowDialog();
        }

        private void TbStrategie_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new STRATEGIE().ShowDialog();
        }

        private void OpenAuthentifier_Click(object sender, RoutedEventArgs e)
        {
            Login.ShowWindow();
            this.Close();
        }
    }
}
