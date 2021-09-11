﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for Selection_Permis_A_Renouveller.xaml
    /// </summary>
    public partial class Selection_Permis_A_Renouveller : Window
    {
        int NumeroExPermis=0;
        Home home;
        public Selection_Permis_A_Renouveller(Home home)
        {
            InitializeComponent();
            this.home = home;
            List<int?> numeroPermis=DataBase.context.Les_Permis.Where(p=>p.Etat_PermisId!=EtatPermis.Renouvelle && p.Etat_PermisId != EtatPermis.EnExploitation).Select(p => p.Num_Permis).ToList();
            numeroPermis.RemoveAll(n => n.Value == 0);
            PermisAutoCombo.ItemsSource = numeroPermis;

            PermisAutoCombo.SelectionChanged += PermisAutoCombo_SelectionChanged;
        }
        public static void Show(Home home)
        {
            new Selection_Permis_A_Renouveller(home).ShowDialog();
        }
        private void PermisAutoCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(PermisAutoCombo.Text)) return;
            NumeroExPermis = Convert.ToInt32(PermisAutoCombo.Text);

        }

        private void Afficher_Click(object sender, RoutedEventArgs e)
        {
            if (NumeroExPermis==0) return;
            int permisId = DataBase.context.Les_Permis.Where(p => p.Num_Permis == NumeroExPermis).Single().PermisId;
            Permis_Recherche.ShowExistingPermis(this.home,permisId);
        }

        private void Selectionner_Click(object sender, RoutedEventArgs e)
        {
            if (NumeroExPermis == 0) return;

            Permis ExPermis = DataBase.context.Les_Permis.Where(p => p.Num_Permis == NumeroExPermis).Single();
            Permis newPermis = new Permis(ExPermis.Area, ExPermis.Titulaire);
            newPermis.Ex_PermisId = ExPermis.PermisId;
            newPermis.Type_PermisId =TypePermis.PRR;
            ExPermis.Etat_PermisId = EtatPermis.Renouvelle;
            DataBase.context.Les_Permis.Add(newPermis);
            DataBase.context.SaveChanges();

            Permis_Recherche_Rennouvelle.ShowExistingPermis(home,newPermis.PermisId);
            //InitilializerLesDossierPermis.InitilizerDossiers(newPermis, TypePermis.PR);
            this.Close();
        }
    }
}
