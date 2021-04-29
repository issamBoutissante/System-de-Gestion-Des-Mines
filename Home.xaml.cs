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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        ProjetMinesDBContext projetMinesDBContext = new ProjetMinesDBContext();
        List<Permis> permis;
        public Home()
        {
            InitializeComponent();

        }
        void RemplirDataGrid()
        {
            permis= this.projetMinesDBContext.Les_Permis.ToList();
            DataGridPermis.ItemsSource = permis;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Remplir_la_base_de_donne.Remplir();
            RemplirDataGrid();
        }
    }
}
