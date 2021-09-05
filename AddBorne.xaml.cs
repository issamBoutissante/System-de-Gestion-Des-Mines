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
    /// Interaction logic for AddBorne.xaml
    /// </summary>
    public partial class AddBorne : Window
    {
        ProjetMinesDBContext context;
        int currentPermisId;
        Licence_Area Licence_Area;
        public AddBorne(Licence_Area licence_Area,int currentPermisId,ProjetMinesDBContext context)
        {
            InitializeComponent();
            this.currentPermisId = currentPermisId;
            this.Licence_Area = licence_Area;
            this.context = context;
        }
        public static void Show(Licence_Area licence_Area,int currentPermisId,ProjetMinesDBContext context)
        {
            new AddBorne(licence_Area,currentPermisId,context).ShowDialog();
        }
        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            Permis currentPermis = context.Les_Permis.Where(p => p.PermisId == currentPermisId).Single();
            string newBorne = $"X : {X_Borne.Text.Trim()} - Y : {Y_Borne.Text.Trim()}";
            bool isBorneExist = currentPermis.Area.Bornes.Any(b => $"X : {b.Borne_X.Trim()} - Y : {b.Borne_Y.Trim()}" == newBorne);
            if (isBorneExist)
            {
                ModalInfo.ShowMsg("Cette Borne est deja exist");
                return;
            }
            currentPermis.Area.Bornes.Add(new Borne()
            {
                Borne_X = X_Borne.Text.Trim(),
                Borne_Y = X_Borne.Text.Trim()
            });
            context.SaveChanges();
            Licence_Area.RemplirBornes();
            this.Close();
        }
    }
}