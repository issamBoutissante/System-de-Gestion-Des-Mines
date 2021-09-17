using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        Button SelectedButton;
        string searchByText;
        public Home()
        {
            InitializeComponent();
            SelectedButton = NumDemmandeBtn;
            Global.InitializeHome(this);
            NotificationBar.Width = 0;
        }
        public static void ShowWindow()
        {
            new Home().Show();
        }
        public void RemplirDataGrid()
        {
            List<Permis> Les_Permis=Global.context.Les_Permis.ToList();
            Les_Permis.Reverse();
            this.DataGridPermis.ItemsSource = null;
            this.DataGridPermis.Items.Clear();
            this.DataGridPermis.ItemsSource = Les_Permis;
        }
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RemplirDataGrid();
        }
        private void Afficher_Click(object sender, RoutedEventArgs e)
        {
            Permis permis = (Permis)DataGridPermis.SelectedItem;
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
        private void SearchPermis(string searchBy)
        {
            List<Permis> Les_Permis = Global.context.Les_Permis.ToList();
            switch (searchBy)
            {
                case "N° demmande":
                    int numDemande;
                    if (int.TryParse(Search.Text, out numDemande)==false) return;
                    this.DataGridPermis.ItemsSource = Les_Permis.Where(p => p.Num_Demmande == numDemande).ToList();
                    break;
                case "Nom Societe":
                    this.DataGridPermis.ItemsSource = Les_Permis.Where(p => p.Titulaire.Nom_Societe.Contains(Search.Text)).ToList();
                    break;
                case "N° Permis":
                    int numPermis;
                    if (int.TryParse(Search.Text, out numPermis) == false) return;
                    this.DataGridPermis.ItemsSource = Les_Permis.Where(p => p.Num_Permis == numPermis).ToList();
                    break;
                case "Province":
                    this.DataGridPermis.ItemsSource = Les_Permis.Where(p => p.Area.Commune.Caidat.Province.Nom_Province.Contains(Search.Text)).ToList();
                    break;
                case "Commune":
                    this.DataGridPermis.ItemsSource = Les_Permis.Where(p => p.Area.Commune.Nom_Commune.Contains(Search.Text)).ToList();
                    break;
                case "Region":
                    this.DataGridPermis.ItemsSource = Les_Permis.Where(p => p.Area.Commune.Caidat.Province.Region.Nom_Region.Contains(Search.Text)).ToList();
                    break;
            }
        }

        private void Search_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(Search.Text)) 
            {
                if (DataGridPermis.Items.Count < 5)
                {
                    RemplirDataGrid();
                }
                return;
            };
            SearchPermis(SelectedButton.Content.ToString());
        }

        private void ChangeSearchCritire_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            searchByText = button.Content.ToString();

            //switch backcolor;
            Brush BackColor =SelectedButton.Background;
            SelectedButton.Background = button.Background;
            button.Background = BackColor;

            //switch forecolor
            Brush foreColor = SelectedButton.Foreground;
            SelectedButton.Foreground = button.Foreground;
            button.Foreground = foreColor;
            SelectedButton = button;
        }

        private void AjouterPermis_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Vous veullez ajouter nouveau permis de recherche", "Message",MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                SearchTitulaire.ShowWindow();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result=MessageBox.Show("Vous veullez deconnecte", "Deconnnection",MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No) return;
            Accueil.ShowWindow();
            Global.utilisateur.isLogedIn = false;
            Global.context.SaveChanges();
            this.Close();
        }

        private void Notifacation_Click(object sender, RoutedEventArgs e)
        {
            if (NotificationBar.Width == 0)
            {
                NotificationBar.Width = 300;
            }
            else
            {
                NotificationBar.Width = 0;
            }
        }
    }
}
