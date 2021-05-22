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
    /// Interaction logic for ModalInfo.xaml
    /// </summary>
    public partial class ModalInfo : Window
    {
        public ModalInfo()
        {
            InitializeComponent();
        }
        internal static Action<string> ShowMsg
        {
            get => (message) =>
            {
                new ModalError().ShowMessage(message);
            };
        }
        internal void ShowMessage(string message)
        {
            this.Message.Text = message;
            this.Show();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
