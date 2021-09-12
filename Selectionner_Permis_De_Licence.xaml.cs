using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for Selectionner_Permis_De_Licence.xaml
    /// </summary>
    /// 
    public partial class Selectionner_Permis_De_Licence : Window
    {
        int NumeroExPermis = 0;
        Home home;
        Permis currentPermis;
        Licence_Exploitation licence_Exploitation;
        public Selectionner_Permis_De_Licence(Home home,Permis currentPermis,Licence_Exploitation licence_Exploitation)
        {
            InitializeComponent();
            this.currentPermis = currentPermis;
            this.home = home;
            this.licence_Exploitation = licence_Exploitation;
            List<int?> numeroPermis = DataBase.context.Les_Permis.Where(p => p.Etat_PermisId != EtatPermis.EnExploitation && p.Type_PermisId!=TypePermis.LE).ToList().Select(p => p.Num_Permis).ToList();
            numeroPermis.RemoveAll(n => n.Value == 0);
            PermisAutoCombo.ItemsSource = numeroPermis;

            PermisAutoCombo.SelectionChanged += PermisAutoCombo_SelectionChanged;
        }
        public static void Show(Home home,Permis currentPermis,Licence_Exploitation licence_Exploitation)
        {
            new Selectionner_Permis_De_Licence(home,currentPermis,licence_Exploitation).ShowDialog();
        }
        private void PermisAutoCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(PermisAutoCombo.Text)) return;
            NumeroExPermis = Convert.ToInt32(PermisAutoCombo.Text);
        }
        private void Afficher_Click(object sender, RoutedEventArgs e)
        {
            if (NumeroExPermis == 0) return;
            Permis selectedPermis = DataBase.context.Les_Permis.Where(p => p.Num_Permis == NumeroExPermis).Single();
            if (selectedPermis.Type_PermisId == TypePermis.PR)
            {
                Permis_Recherche.ShowExistingPermis(this.home, selectedPermis.PermisId);
            }
            else
            {
                Permis_Recherche_Rennouvelle.ShowExistingPermis(this.home, selectedPermis.PermisId);
            }
            this.Close();
        }

        private void Selectionner_Click(object sender, RoutedEventArgs e)
        {
            if (NumeroExPermis == 0) return;
            Permis selectedPermis = DataBase.context.Les_Permis.Where(p => p.Num_Permis == NumeroExPermis).Single();
            selectedPermis.Etat_PermisId = EtatPermis.EnExploitation;
            currentPermis.Licence_Permis.Add(selectedPermis);
            DataBase.context.SaveChanges();
            licence_Exploitation.RemplirLicence_PermisGrid();
            this.Close();
        }
    }
}