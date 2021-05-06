using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        ProjetMinesDBContext projetMinesDBContext ;
        Permis_Recherche Permis_Recherche ;
        public Home()
        {
            InitializeComponent();
            projetMinesDBContext = new ProjetMinesDBContext();
            Permis_Recherche = new Permis_Recherche(projetMinesDBContext);
        }
        void RemplirDataGrid()
        {
            List<Permis> permis= this.projetMinesDBContext.Les_Permis.ToList();
            DataGridPermis.ItemsSource = permis;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Remplir_la_base_de_donne.Remplir();
            RemplirDataGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Permis_Recherche.DataContext = (Permis)DataGridPermis.SelectedItem;
            Permis_Recherche.Show();
            this.Close();
        }
    }
}
