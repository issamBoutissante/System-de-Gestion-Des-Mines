using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Media;

namespace Projet_Mines_Official
{
    public partial class Permis_Recherche : Window
    {
        ProjetMinesDBContext projetMinesDBContext=new ProjetMinesDBContext();
        public Permis Permis { get; set; }
        Home Home;
        public Permis_Recherche(Home home,bool isNewPermis,int PermisId=0)
        {
            InitializeComponent();
            Home = home;
            if (isNewPermis)
            {
                this.Permis = new Permis(new Area(), new Titulaire());
                this.projetMinesDBContext.Les_Permis.Add(this.Permis);
                this.projetMinesDBContext.SaveChanges();
            }else
                this.Permis = projetMinesDBContext.Les_Permis.Find(PermisId);
            this.DataContext = this.Permis;
            InitializeControls(isNewPermis);
            InitializeAutoCompleteCombo();
        }
        private void InitializeAutoCompleteCombo()
        {
            ChevauchementCombo.ItemsSource = this.projetMinesDBContext.Les_Permis.Select(p => p.Num_Permis ).ToList();
        }
        #region Fill data 
        private void FillComboboxes(bool isNewPermis)
        {
            Carte.ItemsSource = projetMinesDBContext.Cartes.ToList();
            Carte.SelectedValuePath = "CarteId";
            Carte.DisplayMemberPath = "Nom_carte";
            Point_Pevot.ItemsSource = projetMinesDBContext.Point_Pivots.ToList();
            Point_Pevot.SelectedValuePath = "Point_PivotId";
            Point_Pevot.DisplayMemberPath = "Nom_Point_Pevot";
            Region.ItemsSource = projetMinesDBContext.Regions.ToList();
            Region.SelectedValuePath = "RegionId";
            Region.DisplayMemberPath = "Nom_Region";
            //Province.SetBinding(ComboBox.ItemsSourceProperty,"";
            Province.ItemsSource = projetMinesDBContext.Provinces.ToList();
            Province.SelectedValuePath = "ProvinceId";
            Province.DisplayMemberPath = "Nom_Province";
            Caidat.ItemsSource = projetMinesDBContext.Caidats.ToList();
            Caidat.SelectedValuePath = "CaidatId";
            Caidat.DisplayMemberPath = "Nom_Caidat";
            Commune.ItemsSource = projetMinesDBContext.Communes.ToList();
            Commune.SelectedValuePath = "CommuneId";
            Commune.DisplayMemberPath = "Nom_Commune";
            //Carte.SetBinding(ComboBox.SelectedValueProperty, "Area.Carte.CarteId");
            //Region.SetBinding(ComboBox.SelectedValueProperty, "Area.Commune.Caidat.Province.Region.RegionId");
            //Province.SetBinding(ComboBox.SelectedValueProperty, "Area.Commune.Caidat.Province.ProvinceId");
            //Point_Pevot.SetBinding(ComboBox.SelectedValueProperty, "Area.Point_Pivot.Point_PevotId");
            //Commune.SetBinding(ComboBox.SelectedValueProperty, "Area.Commune.CommuneId");
            //Caidat.SetBinding(ComboBox.SelectedValueProperty, "Area.Commune.Caidat.CaidatId");

        }
        private void BindDatePickers(bool isNewPermis)
        {
           
            Date_Depot.SetBinding(DatePicker.SelectedDateProperty, "Date_Depot");
            Date_Decision.SetBinding(DatePicker.SelectedDateProperty, "Date_Decision");
            Date_Echeance.SetBinding(DatePicker.SelectedDateProperty, "Echeance");
            Date_Institision.SetBinding(DatePicker.SelectedDateProperty, "Date_Institition");
        }
        private void FillElementDossiers(bool isNewPermis)
        {
            if (isNewPermis)
            {
                InitilializerLesDossierPermis.InitilizerDossiers(this.Permis, TypePermis.PR);
            }
                //this.Permis.Permis_ElementDossiers.ToList().ForEach(e => MessageBox.Show($"{e.Element_DossierId} - {e.Element_Dossier.Element_DossierId} - {e.Element_Dossier.nom_dossier}"));
            InfoVerification.ItemsSource = this.Permis.Permis_ElementDossiers;
        }
        private void InitializeControls(bool isNewPermis)
        {
            FillComboboxes(isNewPermis);
            BindDatePickers(isNewPermis);
            FillElementDossiers(isNewPermis);
            if (isNewPermis)
            {
                RestJourProgramme.Text = "Rest : 180";
                RestJourDeclarationTravaux.Text = "Rest : 360";
                return;
            }
            BindTextBoxes(isNewPermis);
        }
        private void BindTextBoxes(bool isNewPermis)
        {
            //Set Binding For
            //Titulaire Information
            Numero_Demande.SetBinding(TextBox.TextProperty, "Num_Demmande");
            Nom_Demandeur.SetBinding(TextBox.TextProperty, "Titulaire.Nom_Demandeur");
            Status_Demandeur.SetBinding(TextBox.TextProperty, "Titulaire.status_Demandeur");
            Raison_Social.SetBinding(TextBox.TextProperty,"Titulaire.Raison_Social");
            Nom_Societe.SetBinding(TextBox.TextProperty, "Titulaire.Nom_Societe");
            Numero_CNSS.SetBinding(TextBox.TextProperty, "Titulaire.Numero_Cnss");
            Domicile_Demandeur.SetBinding(TextBox.TextProperty, "Titulaire.Election_Domicile");
            Registre_Commerce.SetBinding(TextBox.TextProperty, "Titulaire.Registre_Commerce");
            Taxe_Prof.SetBinding(TextBox.TextProperty, "Titulaire.Taxe_Prof");
            Nom_Site.SetBinding(TextBox.TextProperty, "Titulaire.Nom_Site");
            Effective.SetBinding(TextBox.TextProperty, "Titulaire.Effictif");
            //Area Information
            Inscription_Conservation.SetBinding(CheckBox.IsCheckedProperty, "Inscription_Conservation");
            Dir_e_o.SetBinding(TextBox.TextProperty, "Area.Dis_e_o");
            Superficie.SetBinding(TextBox.TextProperty, "Area.Superficie");
            dir_n_s.SetBinding(TextBox.TextProperty, "Area.Dis_n_s");
            Dis_n_s.SetBinding(TextBox.TextProperty, "Area.Dis_n_s");
            Dis_e_o.SetBinding(TextBox.TextProperty, "Area.Dis_e_o");
            foreach(Permis chev in this.Permis.Chevauchements)
            {
                Chevauchements.Children.Add(GetChevauchementElement((int)chev.Num_Permis));
            }
            Zone.SetBinding(TextBox.TextProperty,"Area.Zone");
            Abscisse.SetBinding(TextBox.TextProperty, "Area.Abscisse");
            Ordonne.SetBinding(TextBox.TextProperty, "Area.Ordonnee");
            //suivi decision information
            Numero_Permis.SetBinding(TextBox.TextProperty, "Num_Permis");
            investisement_realise.SetBinding(TextBox.TextProperty, "Investisement_Realise");
            occupation_temporaire.SetBinding(TextBox.TextProperty, "Occupation_Temporaire");

            //
            if (isNewPermis) return;
            double daysPassed = (DateTime.Now.Date - this.Permis.Date_Decision.Date).TotalDays;
            RestJourProgramme.Text = $"Rest : {180-daysPassed}";
            RestJourDeclarationTravaux.Text = $"Rest : {360-daysPassed}";
        }
        #endregion
        #region update data
        private void UpdateChevauchements()
        {
            this.Permis.Chevauchements.Clear();
            ElementsLooper.GetElements(Chevauchements, typeof(Button), (dynamic obj) =>
              {
                  Button b = (Button)obj;
                  int numero = Convert.ToInt32(b.Content);
                  this.Permis.Chevauchements.Add(this.projetMinesDBContext.Les_Permis.Single(p=>p.Num_Permis==numero));
              });
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UpdateChevauchements();

            this.projetMinesDBContext.SaveChanges();
            Home.RemplirDataGrid();
            Home.Show();
        }
        #endregion
        private void addChevauchement_Click(object sender, RoutedEventArgs e)
        {
            Chevauchements.Children.Add(GetChevauchementElement(Convert.ToInt32(ChevauchementCombo.Text))); 
        }
        private Button GetChevauchementElement(int NumPermis)
        {
            Button btn = new Button()
            {
                Content = NumPermis.ToString(),
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.White,
                BorderBrush=Brushes.Black,
                BorderThickness=new Thickness(1),
                Margin=new Thickness(10,0,0,0),
                Foreground=Brushes.Gray
            };
            btn.Click += Btn_Click;
            return btn;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Chevauchements.Children.Remove((Button)sender);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Decision_PR.Value,
                (Word.Application wordApp) =>
                {
                    //DocumentGenerator.FindAndReplace(wordApp, "<anne>", DateTime.Now.Year.ToString());
                    //DocumentGenerator.FindAndReplace(wordApp, "<societe>", Nom_Societe.Text);
                    //DocumentGenerator.FindAndReplace(wordApp, "<registreCommerce>", Registre_Commerce.Text);
                    //DocumentGenerator.FindAndReplace(wordApp, "<cnss>", Numero_CNSS.Text);
                    //DocumentGenerator.FindAndReplace(wordApp, "<taxeProf>", Taxe_Prof.Text);
                    //DocumentGenerator.FindAndReplace(wordApp, "<numeroDemande>", Numero_Demande.Text);
                    //DocumentGenerator.FindAndReplace(wordApp, "<domicile>", Domicile_Demandeur.Text);
                    //DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                ,dw.documentsContainer,()=> { dw.Show(); });
        }
    }
}
