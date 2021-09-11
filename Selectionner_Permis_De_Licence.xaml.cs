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
    /// Interaction logic for Selectionner_Permis_De_Licence.xaml
    /// </summary>
    /// 
    public  enum OperationType
    {
        New,
        Add
    }
    public partial class Selectionner_Permis_De_Licence : Window
    {
        int NumeroExPermis = 0;
        Home home;
        OperationType operationType;
        Permis currentPermis;
        Licence_Exploitation licence_Exploitation;
        public Selectionner_Permis_De_Licence(Home home,OperationType operationType,Permis currentPermis=null,Licence_Exploitation licence_Exploitation=null)
        {
            InitializeComponent();
            this.operationType = operationType;
            this.currentPermis = currentPermis;
            this.home = home;
            this.licence_Exploitation = licence_Exploitation;
            List<int?> numeroPermis = DataBase.context.Les_Permis.Where(p => p.Etat_PermisId != EtatPermis.EnExploitation && p.Type_PermisId!=TypePermis.LE).ToList().Select(p => p.Num_Permis).ToList();
            numeroPermis.RemoveAll(n => n.Value == 0);
            PermisAutoCombo.ItemsSource = numeroPermis;

            PermisAutoCombo.SelectionChanged += PermisAutoCombo_SelectionChanged;
        }
        public static void Show(Home home,OperationType operationType,Permis currentPermis=null,Licence_Exploitation licence_Exploitation=null)
        {
            new Selectionner_Permis_De_Licence(home, operationType,currentPermis,licence_Exploitation).ShowDialog();
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
            if (operationType == OperationType.New)
            {
                newPermis();
            }
            else
            {
                addPermis();
            }
            this.Close();
        }
        private void newPermis()
        {
            Permis selectedPermis = DataBase.context.Les_Permis.Where(p => p.Num_Permis == NumeroExPermis).Single();
            selectedPermis.Etat_PermisId = EtatPermis.EnExploitation;
            DataBase.context.SaveChanges();

            Permis newPermis = new Permis(new Area(), new Titulaire());
            newPermis.Licence_Permis.Add(selectedPermis);
            newPermis.Type_PermisId = TypePermis.LE;
            DataBase.context.Les_Permis.Add(newPermis);
            DataBase.context.SaveChanges();
            InitilializerLesDossierPermis.InitilizerDossiers(newPermis, TypePermis.LE);
            Licence_Exploitation.ShowExistingLicence(this.home, newPermis.PermisId);
        }
        private void addPermis()
        {

            Permis selectedPermis = DataBase.context.Les_Permis.Where(p => p.Num_Permis == NumeroExPermis).Single();
            selectedPermis.Etat_PermisId = EtatPermis.EnExploitation;
            currentPermis.Licence_Permis.Add(selectedPermis);
            DataBase.context.SaveChanges();
            licence_Exploitation.RemplirLicence_PermisGrid();
        }
    }
}