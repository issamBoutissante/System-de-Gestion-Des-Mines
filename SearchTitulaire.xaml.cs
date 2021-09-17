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
    /// Interaction logic for SearchTitulaire.xaml
    /// </summary>
    public partial class SearchTitulaire : Window
    {
        Permis Permis;
        bool isTitulaireSelected=false;
        public SearchTitulaire()
        {
            InitializeComponent();
            this.Permis = new Permis(new Area(),new Titulaire());
            this.DataContext = this.Permis;
            List<string> list_societe = Global.context.Titulaires.Where(t => string.IsNullOrEmpty(t.Nom_Societe) == false).Select(t => t.Nom_Societe).ToList();
            SocieteToSearch.ItemsSource = list_societe;
        }
        internal static void ShowWindow()
        {
            new SearchTitulaire().ShowDialog();
        }
        private void Nouveua_Click(object sender, RoutedEventArgs e)
        {
            Permis_Recherche.ShowNewPermis();
            this.Close();
        }
        private void SearchTitulaire_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nomSociete = SocieteToSearch.Text;
            if (Global.context.Titulaires.Any(t => t.Nom_Societe == nomSociete) == false) return;
            Titulaire selectedTitulaire = Global.context.Titulaires.Single(t => t.Nom_Societe == nomSociete);
            this.Permis.Titulaire = selectedTitulaire;
            BindTitulaire();
            isTitulaireSelected = true;
        }
        private void Selectionner_Click(object sender, RoutedEventArgs e)
        {
            if (isTitulaireSelected == false)
            {
                MessageBox.Show("rechercher la societe avant de selectionner", "Message");
                return;
            }
            Permis_Recherche.ShowNewPermis(this.Permis.Titulaire);
            this.Close();
        }
        private void BindTitulaire()
        {
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
