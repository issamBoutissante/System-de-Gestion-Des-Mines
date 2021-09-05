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
            this.Permis = permis;
            InitializeControls();
        }
        //for test
        public Licence_Area()
        {
            InitializeComponent();
            this.Permis = context.Les_Permis.Where(p => p.Type_PermisId == TypePermis.LE).ToList().First();
            InitializeControls();
            MessageBox.Show(this.Permis.Area.Bornes.Count.ToString());
        }
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
        class DirInfo
        {
            public string DirName { get; set; }
            public string DirValue { get; set; }
            public DirInfo(string dirName, string dirValue)
            {
                this.DirName = dirName;
                this.DirValue = dirValue;
            }
        }

        private void FillComboboxes()
        {
            ICollection<DirInfo> DirEOList = new List<DirInfo>() {
                new DirInfo("Est","e"),
                new DirInfo("Ouest","o")
            };
            Dir_e_o.ItemsSource = DirEOList;
            Dir_e_o.SelectedValuePath = "DirValue";
            Dir_e_o.DisplayMemberPath = "DirName";
            Dir_e_o.SetBinding(ComboBox.SelectedValueProperty, "Area.Dir_Est_ouest");
            Dir_e_o.SelectionChanged += Dir_SelectionChanged;


            ICollection<DirInfo> DirNSList = new List<DirInfo>() {
                new DirInfo("Nord","n"),
                new DirInfo("Sud","s")
            };
            dir_n_s.ItemsSource = DirNSList;
            dir_n_s.SelectedValuePath = "DirValue";
            dir_n_s.DisplayMemberPath = "DirName";
            dir_n_s.SetBinding(ComboBox.SelectedValueProperty, "Area.Dir_nord_sud");
            dir_n_s.SelectionChanged += Dir_SelectionChanged;



            Carte.ItemsSource = context.Cartes.ToList();
            Carte.SelectedValuePath = "CarteId";
            Carte.DisplayMemberPath = "Nom_carte";
            Point_Pevot.ItemsSource = context.Point_Pivots.ToList();
            Point_Pevot.SelectedValuePath = "Point_PivotId";
            Point_Pevot.DisplayMemberPath = "Nom_Point_Pevot";
            Region.ItemsSource = context.Regions.ToList();
            Region.SelectedValuePath = "RegionId";
            Region.DisplayMemberPath = "Nom_Region";
            //Province.SetBinding(ComboBox.ItemsSourceProperty,"";
            Province.ItemsSource = context.Provinces.ToList();
            Province.SelectedValuePath = "ProvinceId";
            Province.DisplayMemberPath = "Nom_Province";
            Caidat.ItemsSource = context.Caidats.ToList();
            Caidat.SelectedValuePath = "CaidatId";
            Caidat.DisplayMemberPath = "Nom_Caidat";
            Commune.ItemsSource = context.Communes.ToList();
            Commune.SelectedValuePath = "CommuneId";
            Commune.DisplayMemberPath = "Nom_Commune";
            //Bind them
            Carte.SetBinding(ComboBox.SelectedValueProperty, "Area.CarteId");
            Region.SetBinding(ComboBox.SelectedValueProperty, "Area.Commune.Caidat.Province.RegionId");
            Province.SetBinding(ComboBox.SelectedValueProperty, "Area.Commune.Caidat.ProvinceId");
            Point_Pevot.SetBinding(ComboBox.SelectedValueProperty, "Area.Point_PivotId");
            Commune.SetBinding(ComboBox.SelectedValueProperty, "Area.CommuneId");
            Caidat.SetBinding(ComboBox.SelectedValueProperty, "Area.Commune.CaidatId");

        }

        private void Dir_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(((ComboBox)sender).SelectedValue.ToString());
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
            MessageBox.Show(this.Permis.Area.Bornes.Count.ToString());
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
