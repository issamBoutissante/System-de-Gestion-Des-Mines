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
    /// Interaction logic for Permis_Recherche.xaml
    /// </summary>
    public partial class Permis_Recherche : Window
    {
        ProjetMinesDBContext projetMinesDBContext=new ProjetMinesDBContext();
        public Permis_Recherche()
        {
            InitializeComponent();
            RemplirPointPevot();
        }
        void RemplirPointPevot()
        {
            projetMinesDBContext.Point_Pivots.ToList().ForEach(point => Point_Pevot.Items.Add(point.Nom_Point_Pevot));
        }
        private void Enregistrer_DemmandeInfo_Click(object sender, RoutedEventArgs e)
        {
            //si le permis deja exist on vas le modifier si non on va l'ajouter
            Permis permis = projetMinesDBContext.Les_Permis.Find(int.Parse(Numero_Demande.Text));
            if (permis == null)
            {
                permis = new Permis();
                UpdateDemmandeInfo(permis);
                projetMinesDBContext.Les_Permis.Add(permis);
                //permis.Etat_Permis = projetMinesDBContext.Etats_Permis.Find(0);
                //permis.Type_Permis = projetMinesDBContext.Types_Permis.Find(0);
            }
            else
                UpdateDemmandeInfo(permis);
            projetMinesDBContext.SaveChanges();
        }
        void UpdateDemmandeInfo(Permis permis)
        {
            permis.Num_Demmande = int.Parse(Numero_Demande.Text);
            permis.Date_Depot = (DateTime)Date_Depot.SelectedDate;
        }
        void UpdateTitulaireInfo(Permis permis)
        {
            permis.Titulaire = new Titulaire()
            {
                Nom_Demandeur = Nom_Demandeur.Text,
                status_Demandeur = Status_Demandeur.Text,
                Raison_Social = Raison_Social.Text,
                Nom_Societe = Nom_Societe.Text,
                Numero_Cnss = Numero_CNSS.Text,
                Election_Domicile = Domicile_Demandeur.Text,
                Registre_Commerce = Registre_Commerce.Text,
                Taxe_Prof = Taxe_Prof.Text,
                Nom_Site = Nom_Site.Text,
                Effictif = int.Parse(Effective.Text),
            };
        }
        void UpdateAreaInfo(Permis permis)
        {
            //permis.Inscription_Conservation=
            //permis.Chevauchements
            permis.Area = new Area()
            {
                Dir_Est_ouest = Dir_e_o.Text[0],
                Dir_nord_sud=dir_n_s.Text[0],
                Dis_e_o=Dis_e_o.Text,
                Abscisse = Abscisse.Text,
                Ordonnee = Ordonne.Text,
                Point_Pivot = new Point_Pivot()
                {
                    Nom_Point_Pevot = Point_Pevot.SelectedItem.ToString()
                },
            };
        }
        private void Enregistrer_Titulaire_Info_Click(object sender, RoutedEventArgs e)
        {
            Permis permis = projetMinesDBContext.Les_Permis.Find(int.Parse(Numero_Demande.Text));
            if (permis == null)
            {
                MessageBox.Show("Cette Demmande N'existe Pas Essayer de Donne le numero de Demmande");
                return;
            }
            UpdateTitulaireInfo(permis);
            projetMinesDBContext.SaveChanges();
        }
        private void Enregistrer_Area_Info_Click(object sender, RoutedEventArgs e)
        {
            Permis permis = projetMinesDBContext.Les_Permis.Find(int.Parse(Numero_Demande.Text));
            if (permis == null)
            {
                MessageBox.Show("Cette Demmande N'existe Pas Essayer de Donne le numero de Demmande");
                return;
            }
            UpdateAreaInfo(permis);
            projetMinesDBContext.SaveChanges();
        }
    }
}
