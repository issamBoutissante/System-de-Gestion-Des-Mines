using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;

namespace Projet_Mines_Official
{
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
        void UpdateDemmandeInfo(Permis permis)
        {
            permis.Num_Demmande = int.Parse(Numero_Demande.Text);
            permis.Date_Depot = (DateTime)Date_Depot.SelectedDate;
        }
        void UpdateTitulaireInfo(Permis permis)
        {
            if(permis==null)
                System.Windows.MessageBox.Show("null reference");
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
        private void VerifierDonne()
        {

        }
        private void Enregistrer_DemmandeInfo_Click(object sender, RoutedEventArgs e)
        {
            //si le permis deja exist on vas le modifier si non on va l'ajouter
            int numDemmande = int.Parse(Numero_Demande.Text);
            Permis permis=null;
            try
            {
                permis = projetMinesDBContext.Les_Permis.Where(p=>p.Num_Demmande==numDemmande).Single();
            }
            catch { }
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
        private void Enregistrer_Titulaire_Info_Click(object sender, RoutedEventArgs e)
        {
            int numDemmande = int.Parse(Numero_Demande.Text);
            Permis permis = projetMinesDBContext.Les_Permis.Where(p=>p.Num_Demmande==numDemmande).Single();
            UpdateTitulaireInfo(permis);
            projetMinesDBContext.SaveChanges();
        }
        private void Enregistrer_Area_Info_Click(object sender, RoutedEventArgs e)
        {
            int numDemmande = int.Parse(Numero_Demande.Text);
            Permis permis = projetMinesDBContext.Les_Permis.Where(p=>p.Num_Demmande==numDemmande).Single();
            UpdateAreaInfo(permis);
            projetMinesDBContext.SaveChanges();
        }
        private void ConvertToEcxel()
        {
            //using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            //{
            //    if (sfd.ShowDialog() == Di)
            //    {
            //        using(XLWorkbook w=new XLWorkbook())
            //        {
            //            work
            //        }
            //    }
            //}
        }

        private void GenererBultainVersement_Click(object sender, RoutedEventArgs e)
        {
            DocumentGenerator.CreateWordDocument(@"C:\Users\ISSAM\Desktop\PFF\Projet Mines Official\Rapports\Bulletin de versement PR.docx",
                $@"C:\Users\ISSAM\Desktop\PFF\Projet Mines Official\Rapports\Bulletin de versement PR {Nom_Societe.Text}.docx",
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<anne>", DateTime.Now.Year.ToString());
                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", Nom_Societe.Text);
                    DocumentGenerator.FindAndReplace(wordApp, "<registreCommerce>", Registre_Commerce.Text);
                    DocumentGenerator.FindAndReplace(wordApp, "<cnss>", Numero_CNSS.Text);
                    DocumentGenerator.FindAndReplace(wordApp, "<taxeProf>", Taxe_Prof.Text);
                    DocumentGenerator.FindAndReplace(wordApp, "<numeroDemande>", Numero_Demande.Text);
                    DocumentGenerator.FindAndReplace(wordApp, "<domicile>",Domicile_Demandeur.Text);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                );
        }
    }
}
