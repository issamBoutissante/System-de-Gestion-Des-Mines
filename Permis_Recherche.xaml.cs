using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace Projet_Mines_Official
{
    public partial class Permis_Recherche : Window
    {
        ProjetMinesDBContext projetMinesDBContext=new ProjetMinesDBContext();
        public Permis Permis { get; set; }
        public int PermisId { get; set; }
        Window Home;
        public Permis_Recherche(Window window,bool isNewPermis)
        {
            InitializeComponent();
            this.Height = 700;
            Home = window;
            this.Permis = projetMinesDBContext.Les_Permis.Find(1);
            InitializeControls(isNewPermis);
        }
        private void FillComboBoxes(bool isNewPermis)
        {
            Carte.ItemsSource = projetMinesDBContext.Cartes.ToList();
            Region.ItemsSource = projetMinesDBContext.Regions.ToList();
            Province.ItemsSource = projetMinesDBContext.Provinces.ToList();
            Point_Pevot.ItemsSource = projetMinesDBContext.Point_Pivots.ToList();
            Commune.ItemsSource = projetMinesDBContext.Communes.ToList();
            Caidat.ItemsSource = projetMinesDBContext.Caidats.ToList();
            if (isNewPermis) return;
            Carte.SelectedValue = this.Permis.Area.Carte.Nom_carte;
            Region.SelectedValue = this.Permis.Area.Commune.Caidat.Province.Region.Nom_Region;
            Province.SelectedValue = this.Permis.Area.Commune.Caidat.Province.Nom_Province;
            Point_Pevot.SelectedValue = this.Permis.Area.Point_Pivot.Nom_Point_Pevot;
            Commune.SelectedValue = this.Permis.Area.Commune.Nom_Commune;
            Caidat.SelectedValue = this.Permis.Area.Commune.Caidat.Nom_Caidat;
        }
        private void FillDatePickers(bool isNewPermis)
        {
            if (isNewPermis)
            {
                Date_Depot.SelectedDate = DateTime.Now.Date;
                Date_Decision.SelectedDate = DateTime.Now.Date;
                Date_Echeance.SelectedDate = DateTime.Now.Date;
                Date_Institision.SelectedDate = DateTime.Now.Date;
                return;
            }
            Date_Depot.SelectedDate = this.Permis.Date_Depot;
            Date_Decision.SelectedDate = this.Permis.Date_Decision;
            Date_Echeance.SelectedDate = this.Permis.Echeance;
            Date_Institision.SelectedDate = this.Permis.Date_Institition;
        }
        private void InitializeControls(bool isNewPermis)
        {
            FillComboBoxes(isNewPermis);
            FillDatePickers(isNewPermis);
            if (isNewPermis)
            {
                RestJourProgramme.Text = "Rest : 180";
                RestJourProgramme.Text = "Rest : 360";
                return;
            }
            FillTextBoxesWithExistingData();
        }
        private void FillTextBoxesWithExistingData()
        {
            //Titulaire Information
            Numero_Demande.Text = this.Permis.Num_Demmande.ToString();
            Nom_Demandeur.Text = this.Permis.Titulaire.Nom_Demandeur;
            Status_Demandeur.Text = this.Permis.Titulaire.status_Demandeur;
            Raison_Social.Text = this.Permis.Titulaire.Raison_Social;
            Nom_Societe.Text = this.Permis.Titulaire.Nom_Societe;
            Numero_CNSS.Text = this.Permis.Titulaire.Numero_Cnss;
            Domicile_Demandeur.Text = this.Permis.Titulaire.Election_Domicile;
            Registre_Commerce.Text = this.Permis.Titulaire.Registre_Commerce;
            Taxe_Prof.Text = this.Permis.Titulaire.Taxe_Prof;
            Nom_Site.Text = this.Permis.Titulaire.Nom_Site;
            Effective.Text = this.Permis.Titulaire.Effictif;
            //Area Information
            Inscription_Conservation.IsChecked = this.Permis.Inscription_Conservation;
            Dir_e_o.Text = this.Permis.Area.Dis_e_o;
            Superficie.Text = this.Permis.Area.Superficie.ToString();
            dir_n_s.Text = this.Permis.Area.Dis_n_s;
            Dis_n_s.Text = this.Permis.Area.Dis_n_s;
            Dis_e_o.Text = this.Permis.Area.Dis_e_o;
            foreach(Permis permis in this.Permis.Chevauchements)
            {
                Chevauchements.Children.Add(new Border()
                {
                    Child = new TextBlock() { Text = permis.Num_Permis.ToString(), VerticalAlignment = VerticalAlignment.Center }
                });
            }
            Zone.Text = this.Permis.Area.Zone;
            Abscisse.Text = this.Permis.Area.Abscisse.ToString();
            Ordonne.Text = this.Permis.Area.Ordonnee.ToString();
            //suivi decision information
            Numero_Permis.Text = this.Permis.Num_Permis.ToString();
            investisement_realise.Text = this.Permis.Investisement_Realise.ToString();
            occupation_temporaire.Text = this.Permis.Occupation_Temporaire;
            double daysPassed = (DateTime.Now.Date - this.Permis.Date_Decision.Date).TotalDays;
            RestJourProgramme.Text = $"Rest : {180-daysPassed}";
            RestJourProgramme.Text = $"Rest : {360-daysPassed}";
        }
        #region This is the second method that i will use in the next version
        private void UpdateWhenLeave()
        {
            new ElementsLooper().GetElements(MainPage,typeof(TextBox),(dynamic obj)=>
            {
                TextBox textBox = ((TextBox)obj);
                textBox.MouseLeave += (object sender, MouseEventArgs e) =>
                {
                    MessageBox.Show(((Permis)this.DataContext).Num_Demmande.ToString());
                    this.projetMinesDBContext.SaveChanges();
                };
            }
            );
            new ElementsLooper().GetElements(MainPage, typeof(ComboBox), (dynamic obj) =>
            {
                ComboBox comboBox = ((ComboBox)obj);
                comboBox.MouseLeave += (object sender, MouseEventArgs e) =>
                {
                    this.projetMinesDBContext.SaveChanges();
                };
            }
           );
        }
        #endregion
        #region this is the first method i use but i'm trying a new method with binding
        void UpdateDemmandeInfo(Permis permis)
        {
            if (permis == null)
                MessageBox.Show("null reference");
            permis.Num_Demmande = int.Parse(Numero_Demande.Text);
            permis.Date_Depot = (DateTime)Date_Depot.SelectedDate;
        }
        void UpdateTitulaireInfo(Permis permis)
        {
            if (permis == null)
                MessageBox.Show("null reference");
            if (permis.Titulaire == null)
                permis.Titulaire = new Titulaire();
            permis.Titulaire.Nom_Demandeur = Nom_Demandeur.Text;
            permis.Titulaire.status_Demandeur = Status_Demandeur.Text;
            permis.Titulaire.Raison_Social = Raison_Social.Text;
            permis.Titulaire.Nom_Societe = Nom_Societe.Text;
            permis.Titulaire.Numero_Cnss = Numero_CNSS.Text;
            permis.Titulaire.Election_Domicile = Domicile_Demandeur.Text;
            permis.Titulaire.Registre_Commerce = Registre_Commerce.Text;
            permis.Titulaire.Taxe_Prof = Taxe_Prof.Text;
            permis.Titulaire.Nom_Site = Nom_Site.Text;
            permis.Titulaire.Effictif = Effective.Text;

        }
        void UpdateAreaInfo(Permis permis)
        {
            permis.Inscription_Conservation = (bool)Inscription_Conservation.IsChecked;

            permis.Chevauchements = new List<Permis>();
            permis.Area = new Area()
            {
                Dir_Est_ouest = Dir_e_o.Text[0],
                Dir_nord_sud = dir_n_s.Text[0],
                Dis_e_o = Dis_e_o.Text,
                Abscisse = Abscisse.Text,
                Ordonnee = Ordonne.Text,
                Point_Pivot = new Point_Pivot()
                {
                    Nom_Point_Pevot = Point_Pevot.SelectedItem.ToString()
                },
            };
        }
        private void Enregistrer_DemmandeInfo_Click(object sender, RoutedEventArgs e)
        {
            //si le permis deja exist on vas le modifier si non on va l'ajouter
            int numDemmande = int.Parse(Numero_Demande.Text);
            Permis permis = null;
            try
            {
                permis = projetMinesDBContext.Les_Permis.Where(p => p.Num_Demmande == numDemmande).Single();
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
            Permis permis = projetMinesDBContext.Les_Permis.Where(p => p.Num_Demmande == numDemmande).Single();
            UpdateTitulaireInfo(permis);
            projetMinesDBContext.SaveChanges();
        }
        private void Enregistrer_Area_Info_Click(object sender, RoutedEventArgs e)
        {
            int numDemmande = int.Parse(Numero_Demande.Text);
            Permis permis = projetMinesDBContext.Les_Permis.Where(p => p.Num_Demmande == numDemmande).Single();
            UpdateAreaInfo(permis);
            projetMinesDBContext.SaveChanges();
        }
        #endregion
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

        private void addChevauchement_Click(object sender, RoutedEventArgs e)
        {
            Chevauchements.Children.Add(new Border()
            {
                Child = new TextBlock() { Text = Chevauchement.Text, VerticalAlignment = VerticalAlignment.Center }
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Home.Show();
        }
    }
}
