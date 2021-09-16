using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for AddBorne.xaml
    /// </summary>
    public partial class AddBorne : Window
    {
        Permis Permis;
        public AddBorne(Permis permis)
        {
            InitializeComponent();
            this.Permis = permis;
        }
        public static void Show(Permis permis)
        {
            new AddBorne(permis).ShowDialog();
        }
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            Borne newBorne = new Borne() { Borne_X = X_Borne.Text.Trim(), Borne_Y = Y_Borne.Text.Trim() };
            bool isBorneExist = this.Permis.Area.Bornes.Any(b => b.Borne_Y==newBorne.Borne_Y && b.Borne_X==newBorne.Borne_X);
            if (isBorneExist)
            {
                MessageBox.Show("Cette Borne est deja exist","Message");
                return;
            }
            this.Permis.Area.Bornes.Add(newBorne);
            Global.context.SaveChanges();
            this.Close();
        }
        #region validation
        private void GetOnlyNumbers_KeyDown(object sender, KeyEventArgs e)
        {
            List<Key> keys = new List<Key>()
            {
                Key.D0,
                Key.D1,
                Key.D2,
                Key.D3,
                Key.D4,
                Key.D5,
                Key.D6,
                Key.D7,
                Key.D8,
                Key.D9,
                Key.NumPad0,
                Key.NumPad1,
                Key.NumPad2,
                Key.NumPad3,
                Key.NumPad4,
                Key.NumPad5,
                Key.NumPad6,
                Key.NumPad7,
                Key.NumPad8,
                Key.NumPad9,
                Key.Space
            };

            if (!keys.Contains(e.Key))
            {
                TextBox textBox = sender as TextBox;
                textBox.BorderBrush = Brushes.Red;
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(2000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        textBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF2196F3");
                    });
                });
                e.Handled = true;
            }
        }
        #endregion
    }
}