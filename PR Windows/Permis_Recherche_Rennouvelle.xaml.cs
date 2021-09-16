using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Data;

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for Permis_Recherche_Rennouvelle.xaml
    /// </summary>
    public partial class Permis_Recherche_Rennouvelle : Window
    {

        public Permis Permis { get; set; }
        int? CurrentNumeroDemmand;
        int? CurrentNumeroPermis;
        public Permis_Recherche_Rennouvelle(Permis permis)
        {
            InitializeComponent();
            this.Permis = permis;
            this.DataContext = this.Permis;
            InitializeControls();
            this.CurrentNumeroDemmand = this.Permis.Num_Demmande;
            this.CurrentNumeroPermis = this.Permis.Num_Permis;
            NavigationList.Height = 0;
        }
        internal static void ShowExistingPermis(Permis permis)
        {
            new Permis_Recherche_Rennouvelle(permis).ShowDialog();
        }

        private void InitializeControls()
        {
            BindDatePickers();
            BindTextBoxes();
            RemplirBornes();
            RemplirChevauchement();
        }
       
        #region Fill data 
        private void BindDatePickers()
        {
            Date_Depot.SetBinding(DatePicker.SelectedDateProperty, "Date_Depot");
            Date_Decision.SetBinding(DatePicker.SelectedDateProperty, "Date_Decision");
            Date_Echeance.SetBinding(DatePicker.SelectedDateProperty, "Echeance");
            Date_Institision.SetBinding(DatePicker.SelectedDateProperty, "Date_Institition");
        }
        private Border GetElementDossierTemplate(Permis_ElementDossier dataSource)
        {
            TextBlock textBlock = new TextBlock()
            {
                FontSize = 14,
                FontFamily = new FontFamily("Arial"),
                TextWrapping = TextWrapping.Wrap,
            };
            CheckBox checkBox = new CheckBox();
            Grid grid = new Grid()
            {
                Margin = new Thickness(5, 10, 0, 5)
            };
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });
            grid.Children.Add(textBlock);
            grid.Children.Add(checkBox);
            textBlock.SetValue(Grid.ColumnProperty, 0);
            checkBox.SetValue(Grid.ColumnProperty, 1);
            Border border = new Border()
            {
                DataContext = dataSource,
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Gray,
                Margin = new Thickness(5, 10, 5, 0),
                Padding = new Thickness(10, 10, 10, 10),
                Child = grid
            };

            textBlock.SetBinding(TextBlock.TextProperty, "Element_Dossier.nom_dossier");
            checkBox.SetBinding(CheckBox.IsCheckedProperty, "isExist");
            return border;
        }
        private void BindTextBoxes()
        {
            //Set Binding For
            //Titulaire Information
            NumeroExPermis.SetBinding(TextBox.TextProperty, "Ex_Permis.Num_Permis");
            Numero_Demande.SetBinding(TextBox.TextProperty, "Num_Demmande");
            Nom_Demandeur.SetBinding(TextBox.TextProperty, "Titulaire.Nom_Demandeur");
            Status_Demandeur.SetBinding(TextBox.TextProperty, "Titulaire.status_Demandeur");
            Raison_Social.SetBinding(TextBox.TextProperty, "Titulaire.Raison_Social");
            Nom_Societe.SetBinding(TextBox.TextProperty, "Titulaire.Nom_Societe");
            Numero_CNSS.SetBinding(TextBox.TextProperty, "Titulaire.Numero_Cnss");
            Domicile_Demandeur.SetBinding(TextBox.TextProperty, "Titulaire.Election_Domicile");
            Registre_Commerce.SetBinding(TextBox.TextProperty, "Titulaire.Registre_Commerce");
            Taxe_Prof.SetBinding(TextBox.TextProperty, "Titulaire.Taxe_Prof");
            Nom_Site.SetBinding(TextBox.TextProperty, "Titulaire.Nom_Site");
            Effective.SetBinding(TextBox.TextProperty, "Titulaire.Effictif");
            //Area Information
            Inscription_Conservation.SetBinding(CheckBox.IsCheckedProperty, "Inscription_Conservation");
            NumPermisCheckBox.SetBinding(CheckBox.IsCheckedProperty, "isDecisionSigne");
            Superficie.SetBinding(TextBox.TextProperty, "Area.Superficie");
            if (this.Permis.Area.Dir_Est_ouest == "e")
            {
                Dir_e_o.Text = "Est";
            }
            else
            {
                Dir_e_o.Text = "Ouest";
            }
            if (this.Permis.Area.Dir_nord_sud == "n")
            {
                dir_n_s.Text = "Nord";
            }
            else
            {
                dir_n_s.Text = "Sud";
            }
            Dis_n_s.SetBinding(TextBox.TextProperty, "Area.Dis_n_s");
            Dis_e_o.SetBinding(TextBox.TextProperty, "Area.Dis_e_o");
            
            Zone.SetBinding(TextBox.TextProperty, "Area.Zone");
            Abscisse.SetBinding(TextBox.TextProperty, "Area.Abscisse");
            Ordonne.SetBinding(TextBox.TextProperty, "Area.Ordonnee");
            Carte.SetBinding(TextBox.TextProperty, "Area.Carte.Nom_carte");
            Region.SetBinding(TextBox.TextProperty, "Area.Commune.Caidat.Province.Region.Nom_Region");
            Province.SetBinding(TextBox.TextProperty, "Area.Commune.Caidat.Province.Nom_Province");
            Point_Pevot.SetBinding(TextBox.TextProperty, "Area.Point_Pivot.Nom_Point_Pevot");
            Commune.SetBinding(TextBox.TextProperty, "Area.Commune.Nom_Commune");
            Caidat.SetBinding(TextBox.TextProperty, "Area.Commune.Caidat.Nom_Caidat");
            //suivi decision information
            Numero_Permis.SetBinding(TextBox.TextProperty, "Num_Permis");
        }
        #endregion
        #region update data
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Numero_Demande.Focus();
            //update Etat Permis
            if (Numero_Permis.Text != "0")
                PermisState.updateEtat(this.Permis, EtatPermis.Permis);
            Global.context.SaveChanges();
            Global.Home.RemplirDataGrid();
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

        private void Exporter_excel_Click(object sender, RoutedEventArgs e)
        {
            ExcelGenerator.ExportExcel(this,this.Permis);
        }

        private void Numero_Demande_MouseLeave(object sender, MouseEventArgs e)
        {
            List<int?> numerosDemmandes = Global.context.Les_Permis.Select(p => p.Num_Demmande).ToList();
            numerosDemmandes.Remove(CurrentNumeroDemmand);
            if (string.IsNullOrEmpty(Numero_Demande.Text))
            {
                Numero_Demande.Text = CurrentNumeroDemmand.ToString();
                return;
            }
            int EnteredNumeroDemmand = Convert.ToInt32(Numero_Demande.Text);
            if (numerosDemmandes.Contains(EnteredNumeroDemmand))
            {
                MessageBox.Show("Ce Numero De Demmande Deja Exist .","Message");
                Numero_Demande.Text = CurrentNumeroDemmand.ToString();
            }
        }

        private void Numero_Permis_MouseLeave(object sender, MouseEventArgs e)
        {
            List<int?> numerosPermis = Global.context.Les_Permis.Select(p => p.Num_Permis).Distinct().ToList();
            numerosPermis.Remove(CurrentNumeroPermis);
            numerosPermis.Remove(0);
            if (string.IsNullOrEmpty(Numero_Permis.Text))
            {
                Numero_Permis.Text = CurrentNumeroPermis.ToString();
                return;
            }
            int EnteredNumeroPermis = Convert.ToInt32(Numero_Permis.Text);
            if (numerosPermis.Contains(EnteredNumeroPermis))
            {
                MessageBox.Show("Ce Numero De Permis Deja Exist .","Message");
                Numero_Permis.Text = CurrentNumeroPermis.ToString();
            }
        }
        #region Le Code De PRR
        private void AfficherExPermis_Click(object sender, RoutedEventArgs e)
        {
            int permisId = Global.context.Les_Permis.Where(p => p.Num_Permis == this.Permis.Ex_Permis.Num_Permis).Single().PermisId;
            Permis_Recherche.ShowExistingPermis(this.Permis);
        }
        #endregion
        private void GenererRapport_Click(object sender, RoutedEventArgs e)
        {
            Numero_Demande.Focus();
            Superficie.Focus();
            Global.context.SaveChanges();
            Rapport_PRR.Show(this.Permis);
        }
        bool isNavigationListClosed = true;
        private void NaviagationList_Click(object sender, RoutedEventArgs e)
        {
            if (isNavigationListClosed)
            {
                NavigationList.Height = Double.NaN;
                isNavigationListClosed = false;
            }
            else
            {
                NavigationList.Height = 0;
                isNavigationListClosed = true;
            }
        }
        private void Transferer_Click(object sender, RoutedEventArgs e)
        {
            if (Numero_Permis.Text == "0")
            {
                MessageBox.Show("il faut ajouter numero permis avant de renouveller", "Message");
                return;
            }
            if (this.Permis.Etat_PermisId == EtatPermis.EnExploitation)
            {
                MessageBox.Show("Deja en exploitation", "Message");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Vous veullez Transferer se permis au licence", "Transferer", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.Permis.Etat_PermisId = EtatPermis.EnExploitation;
                Global.context.SaveChanges();

                Permis newPermis = new Permis(new Area(), new Titulaire());
                newPermis.Licence_Permis.Add(this.Permis);
                newPermis.Type_PermisId = TypePermis.LE;
                Global.context.Les_Permis.Add(newPermis);
                Global.context.SaveChanges();
                InitilializerLesDossierPermis.InitilizerDossiers(newPermis, TypePermis.LE);
                Licence_Exploitation.ShowExistingLicence(newPermis);
            }
        }
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
        #endregion
    }
}
