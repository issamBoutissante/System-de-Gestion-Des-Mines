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
        Permis Permis;
        public Licence_Area(Permis permis)
        {
            InitializeComponent();
            this.Permis = Global.context.Les_Permis.Find(permis.PermisId);
            this.DataContext = this.Permis;
            InitializeControls();
            this.Closing += Licence_Area_Closing;
        }

        private void Licence_Area_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Global.context.SaveChanges();
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
            RemplirChevauchement();
            //Fill Bornes GroubBox
            RemplirBornes();

            Zone.SetBinding(TextBox.TextProperty, "Area.Zone");
            Abscisse.SetBinding(TextBox.TextProperty, "Area.Abscisse");
            Ordonne.SetBinding(TextBox.TextProperty, "Area.Ordonnee");
            //suivi decision information
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

        #region Chevauchement Code
        private void RemplirChevauchement()
        {
            List<Permis> chevauchement = this.Permis.Chevauchements.ToList();
            chevauchement.Reverse();
            ChevauchementGrid.ItemsSource = null;
            ChevauchementGrid.Items.Clear();
            ChevauchementGrid.ItemsSource = chevauchement;
            ChevauchementGrid.Items.Refresh();
        }
        private void AddChevauchement_Click(object sender, RoutedEventArgs e)
        {
            List<Permis> list_permis = Global.context.Les_Permis.Where(p => p.Etat_PermisId == EtatPermis.Permis).ToList();
            Recherche_Permis.ShowWindow(list_permis);
            Permis selectedPermis = Recherche_Permis.PermisResult;
            if (selectedPermis == null) return;
            if (this.Permis.Chevauchements.Any(p => p.PermisId == selectedPermis.PermisId))
            {
                MessageBox.Show("Ce chevauchement est deja exists", "Message");
                return;
            }
            this.Permis.Chevauchements.Add(selectedPermis);
            Global.context.SaveChanges();
            RemplirChevauchement();
        }
        private void Afficher_Permis_Click(object sender, RoutedEventArgs e)
        {
            Permis permis = (Permis)ChevauchementGrid.SelectedItem;
            switch (permis.Type_PermisId)
            {
                case TypePermis.PR:
                    Permis_Recherche.ShowExistingPermis(permis);
                    break;
                case TypePermis.PRR:
                    Permis_Recherche_Rennouvelle.ShowExistingPermis(permis);
                    break;
                case TypePermis.LE:
                    Licence_Exploitation.ShowExistingLicence(permis);
                    break;
                case TypePermis.LER:
                    Licence_Exploitation_Renouvelle.ShowExistingPermis(permis);
                    break;
            }
        }
        #endregion
        #region Bornes Code
        private void RemplirBornes()
        {
            List<Borne> bornes = this.Permis.Area.Bornes.ToList();
            BornesGrid.ItemsSource = null;
            BornesGrid.Items.Clear();
            BornesGrid.ItemsSource = bornes;
            BornesGrid.Items.Refresh();
        }
        private void AddBorne_Click(object sender, RoutedEventArgs e)
        {
            AddBorne.Show(this.Permis);
            RemplirBornes();
        }
        private void SupprimerBorne_Click(object sender, RoutedEventArgs e)
        {
            Borne borne = (Borne)BornesGrid.SelectedItem;
            this.Permis.Area.Bornes.Remove(borne);
            Global.context.SaveChanges();
            RemplirBornes();
        }
        #endregion
    }
}
