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
    /// Interaction logic for Selection_Permis_A_Renouveller.xaml
    /// </summary>
    public partial class Selection_Permis_A_Renouveller : Window
    {
        ProjetMinesDBContext context = new ProjetMinesDBContext();
        int NumeroExPermis=0;
        Home home;
        public Selection_Permis_A_Renouveller(Home home)
        {
            this.home = home;
            InitializeComponent();
            List<int?> numeroPermis=context.Les_Permis.Select(p => p.Num_Permis).ToList();
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
            int permisId = context.Les_Permis.Where(p => p.Num_Permis == NumeroExPermis).Single().PermisId;
            Permis_Recherche.ShowExistingPermis(this.home,permisId);
        }

        private void Selectionner_Click(object sender, RoutedEventArgs e)
        {
            if (NumeroExPermis == 0) return;
            ProjetMinesDBContext context = new ProjetMinesDBContext();

            //valider si le permis deja rennovelle
            bool isDejaRenouvelle=false;
            context.Les_Permis.ToList().ForEach(p =>
            {
                if (p.Ex_Permis != null)
                {
                    if (p.Ex_Permis.Num_Permis == NumeroExPermis)
                    {
                        ModalError.ShowMsg("Ce Permis a ete deja rennouvelle");
                        isDejaRenouvelle = true;
                    }
                }
            });
            if (isDejaRenouvelle) return;
            Permis ExPermis = context.Les_Permis.Where(p => p.Num_Permis == NumeroExPermis).Single();
            Permis newPermis = new Permis(ExPermis.Area, ExPermis.Titulaire);

            newPermis.Ex_PermisId = ExPermis.PermisId;
            newPermis.Type_PermisId =TypePermis.PRR;
            context.Les_Permis.Add(newPermis);
            context.SaveChanges();

            Permis_Recherche_Rennouvelle.ShowExistingPermis(home,newPermis.PermisId);
            //InitilializerLesDossierPermis.InitilizerDossiers(newPermis, TypePermis.PR);
            this.Close();
        }
    }
}
