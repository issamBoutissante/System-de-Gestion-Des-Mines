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
        ProjetMinesDBContext projetMinesDBContext =new ProjetMinesDBContext();
        string searchByText= "Numero demmande";
        Button SelectedButton;
        public Home()
        {
            InitializeComponent();
            SelectedButton = NumDemmandeBtn;
        }
        internal void RemplirDataGrid()
        {
            this.DataGridPermis.ItemsSource= this.projetMinesDBContext.Les_Permis.Take(5).ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Remplir_la_base_de_donne.Remplir();
            RemplirDataGrid();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            switch (menuItem.Header)
            {
                case "PR":
                    new Permis_Recherche(this, true);
                    this.Close();
                    break;
            }
        }

        private void Afficher_Click(object sender, RoutedEventArgs e)
        {
            Permis permis = (Permis)DataGridPermis.SelectedItem;
            new Permis_Recherche(this,false,permis.PermisId).Show();
            this.Close();
        }
        private void SearchPermis(string searchBy)
        {
            List<Permis> Les_Permis = this.projetMinesDBContext.Les_Permis.ToList();
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
    }
}
