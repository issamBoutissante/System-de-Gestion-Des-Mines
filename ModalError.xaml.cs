using System;
using System.Windows;

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for ModalError.xaml
    /// </summary>
    public partial class ModalError : Window
    {
        public ModalError()
        {
            InitializeComponent();
        }
        internal static Action<string> ShowMsg { get => (message) =>
        {
            new ModalError().ShowMessage(message);
        }; }
        internal void ShowMessage(string message)
        {
            this.Message.Text = message;
            this.Show();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
