using System;
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
    /// Interaction logic for Selectionner_Licence_Renouvelle.xaml
    /// </summary>
    public partial class Selectionner_Licence_Renouvelle : Window
    {
        int NumeroExPermis = 0;
        Home home;
        public Selectionner_Licence_Renouvelle(Home home)
        {
            InitializeComponent();
            this.home = home;
            List<int?> numeroPermis = DataBase.context.Les_Permis.Where(p => p.Type_PermisId==TypePermis.LE ||p.Type_PermisId==TypePermis.LER).Select(p => p.Num_Permis).ToList();
            numeroPermis.RemoveAll(n => n.Value == 0);
            PermisAutoCombo.ItemsSource = numeroPermis;
            
            PermisAutoCombo.SelectionChanged += PermisAutoCombo_SelectionChanged;
     
        }

        public static void Show(Home home)
        {
            new Selectionner_Licence_Renouvelle(home).ShowDialog();
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
            if (selectedPermis.Type_PermisId == TypePermis.LE)
            {
                Licence_Exploitation.ShowExistingLicence(this.home,selectedPermis.PermisId);
            }
            else
            {
                Licence_Exploitation_Renouvelle.ShowExistingPermis(this.home, selectedPermis.PermisId);
            }
        }

        private void Selectionner_Click(object sender, RoutedEventArgs e)
        {
            if (NumeroExPermis == 0) return;

            Permis ExPermis = DataBase.context.Les_Permis.Where(p => p.Num_Permis == NumeroExPermis).Single();
            Permis newPermis = new Permis(ExPermis.Area, ExPermis.Titulaire);
            DataBase.context.Les_Permis.Add(newPermis);
            DataBase.context.SaveChanges();
            newPermis.Licence_Permis = ExPermis.Licence_Permis;
            newPermis.Ex_PermisId = ExPermis.PermisId;
            newPermis.Type_PermisId = TypePermis.LER;
            ExPermis.Etat_PermisId = EtatPermis.Renouvelle;
            DataBase.context.SaveChanges();

            Licence_Exploitation_Renouvelle.ShowExistingPermis(this.home, newPermis.PermisId);
            this.Close();
        }
    }
}
