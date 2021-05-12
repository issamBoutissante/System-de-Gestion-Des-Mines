using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        ProjetMinesDBContext projetMinesDBContext =new ProjetMinesDBContext();
        public Home()
        {
            InitializeComponent();
        }
        void RemplirDataGrid()
        {
            DataGridPermis.ItemsSource=this.projetMinesDBContext.Les_Permis.ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Remplir_la_base_de_donne.Remplir();
            RemplirDataGrid();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            switch (menuItem.Header)
            {
                case "PR":
                    new Permis_Recherche(this, true).Show();
                    this.Hide();
                    break;
            }
        }

        private void Afficher_Click(object sender, RoutedEventArgs e)
        {
            Permis permis = (Permis)DataGridPermis.SelectedItem;
            new Permis_Recherche(this, false,permis.PermisId).Show();
            //this.Hide();
        }
    }
}
