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
            bool exists = DataBase.context.Utilisateurs.Any(u => u.NomUtilisateur == NomUtilisateur.Text && u.MotPass == MotPass.Password);
            if (!exists)
            {
                ErrorMessage.Text = "Le nom d'utilisateur ou mot de passe incorrect";
                return;
            }
            new Home().Show();
            this.Close();
        }
    }
}
