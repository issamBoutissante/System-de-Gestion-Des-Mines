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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void S_authentifier_Click(object sender, RoutedEventArgs e)
        {
            new Home().Show();
            this.Close();
        }
        private Task GoToMain()
        {
            return Task.Factory.StartNew(() =>
            {

                

            });
        }
    }
}
