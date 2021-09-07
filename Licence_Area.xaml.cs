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
using winForms=System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for Licence_Area.xaml
    /// </summary>
    public partial class Licence_Area : Window
    {
        ProjetMinesDBContext context=new ProjetMinesDBContext();
        Permis Permis;
        public Licence_Area(Permis permis)
        {
            InitializeComponent();
            this.Permis = this.context.Les_Permis.Find(permis.PermisId);
            this.DataContext = this.Permis;
            InitializeControls();
            this.Closing += Licence_Area_Closing;
        }

        private void Licence_Area_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.context.SaveChanges();
        }

        ////for test
        //public Licence_Area()
        //{
        //    InitializeComponent();
        //    this.Permis = context.Les_Permis.Where(p => p.Type_PermisId == TypePermis.LE).ToList().First();
        //    InitializeControls();
        //    this.Closing += Licence_Area_Closing;
        //}
        public static void Show(Permis permis)
        {
            new Licence_Area(permis).ShowDialog();
        }
        private void InitializeControls()
        {
            FillComboboxes();
            BindTextBoxes();
        }
        #region Fill data 
     

        private void FillComboboxes()
        {
            Carte.SetBinding(TextBox.TextProperty, "Area.Carte.Nom_carte");
            Region.SetBinding(TextBox.TextProperty, "Area.Commune.Caidat.Province.Region.Nom_Region");
            Province.SetBinding(TextBox.TextProperty, "Area.Commune.Caidat.Province.Nom_Province");
            Point_Pevot.SetBinding(TextBox.TextProperty, "Area.Point_Pivot.Nom_Point_Pevot");
            Commune.SetBinding(TextBox.TextProperty, "Area.Commune.Nom_Commune");
            Caidat.SetBinding(TextBox.TextProperty, "Area.Commune.Caidat.Nom_Caidat");

        }

        private void BindTextBoxes()
        {
            //Area Information
            Inscription_Conservation.SetBinding(CheckBox.IsCheckedProperty, "Inscription_Conservation");
            Superficie.SetBinding(TextBox.TextProperty, "Area.Superficie");
            //Fill Chevauchements GroubBox
            foreach (Permis chev in this.Permis.Chevauchements)
            {
                Chevauchements.Children.Add(GetChevauchementElement((int)chev.Num_Permis));
            }
            //Fill Bornes GroubBox
            RemplirBornes();

            Zone.SetBinding(TextBox.TextProperty, "Area.Zone");
            Abscisse.SetBinding(TextBox.TextProperty, "Area.Abscisse");
            Ordonne.SetBinding(TextBox.TextProperty, "Area.Ordonnee");
            //suivi decision information
        }
        public void RemplirBornes()
        {
            Bornes.Children.Clear();
            foreach (Borne b in this.Permis.Area.Bornes)
            {
                Bornes.Children.Add(GetBorne(b));
            }
        }
        #endregion
        #region Fill Chevauchement 
        private Button GetChevauchementElement(int NumPermis)
        {
            Button btn = new Button()
            {
                Content = NumPermis.ToString(),
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.White,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(10, 0, 0, 0),
                Foreground = Brushes.Gray
            };
            return btn;
        }
        #endregion
        #region Add Borne 
        private Button GetBorne(Borne borne)
        {
            Button btn = new Button()
            {
                Content = $"X : {borne.Borne_X} - Y : {borne.Borne_Y}",
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.White,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(10, 0, 0, 0),
                Foreground = Brushes.Gray
            };
            btn.Click += Btn_Click;
            return btn;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            winForms.DialogResult result=winForms.MessageBox.Show("vous voulez supprimer cette borne", "Attention",winForms.MessageBoxButtons.YesNo);
            if (result == winForms.DialogResult.Yes)
            {
                Button selectedButton=(Button)sender;
                Borne selectedBorne = this.Permis.Area.Bornes.Where(b => $"X : {b.Borne_X} - Y : {b.Borne_Y}" == selectedButton.Content.ToString()).First();
                this.Permis.Area.Bornes.Remove(selectedBorne);
                this.context.SaveChanges();
                Bornes.Children.Remove(selectedButton);
            }
        }
        #endregion
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

        private void AddBords_Click(object sender, RoutedEventArgs e)
        {
            AddBorne.Show(this,this.Permis.PermisId,context);
        }
    }
}
