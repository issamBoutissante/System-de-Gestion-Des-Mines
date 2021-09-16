using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for Recherche_Permis.xaml
    /// </summary>
    public partial class Recherche_Permis : Window
    {
        Button SelectedButton;
        string searchByText;
        public static Permis PermisResult;
        List<Permis> List_Permis;
        public Recherche_Permis(List<Permis> list_permis)
        {
            InitializeComponent();
            this.List_Permis = list_permis;
            SelectedButton = NumDemmandeBtn;
            RemplirDataGrid();
        }
        public static void ShowWindow(List<Permis> list_permis)
        {
            new Recherche_Permis(list_permis).ShowDialog();
        }
        public void RemplirDataGrid()
        {
            List<Permis> Les_Permis = this.List_Permis;
            Les_Permis.Reverse();
            this.DataGridPermis.ItemsSource = Les_Permis;
        }
        private void SearchPermis(string searchBy)
        {
            List<Permis> Les_Permis = Global.context.Les_Permis.ToList();
            switch (searchBy)
            {
                case "N° demmande":
                    int numDemande;
                    if (int.TryParse(Search.Text, out numDemande) == false) return;
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
            Brush BackColor = SelectedButton.Background;
            SelectedButton.Background = button.Background;
            button.Background = BackColor;

            //switch forecolor
            Brush foreColor = SelectedButton.Foreground;
            SelectedButton.Foreground = button.Foreground;
            button.Foreground = foreColor;
            SelectedButton = button;
        }
        private void Selectionner_Click(object sender, RoutedEventArgs e)
        {
            PermisResult = (Permis)DataGridPermis.SelectedItem;
            this.Close();
        }
    }
}
