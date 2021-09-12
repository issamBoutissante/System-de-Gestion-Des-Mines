﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.Data;
using winform=System.Windows.Forms;

namespace Projet_Mines_Official
{
    public partial class Permis_Recherche : Window
    {
        public Permis Permis { get; set; }
        Home Home;
        int? CurrentNumeroDemmand;
        int? CurrentNumeroPermis;
        public Permis_Recherche(Home home, int PermisId)
        {
            InitializeComponent();
            this.Height = 630;
            Home = home;
            this.Permis = DataBase.context.Les_Permis.Find(PermisId);
            this.DataContext = this.Permis;
            InitializeControls();
            InitializeAutoCompleteCombo();
            this.CurrentNumeroDemmand = this.Permis.Num_Demmande;
            this.CurrentNumeroPermis = this.Permis.Num_Permis;
            NavigationList.Height = 0;
        }
        internal static void ShowNewPermis(Home home)
        {
            Permis newPermis = new Permis(new Area(), new Titulaire());
            DataBase.context.Les_Permis.Add(newPermis);
            DataBase.context.SaveChanges();

            InitilializerLesDossierPermis.InitilizerDossiers(newPermis, TypePermis.PR);
            new Permis_Recherche(home,newPermis.PermisId).Show();
        }
        internal static void ShowExistingPermis(Home home,int permisId)
        {
            new Permis_Recherche(home, permisId).Show();
        }
        private void InitializeControls()
        {
            FillComboboxes();
            BindDatePickers();
            FillElementDossiers();
            BindTextBoxes();

        }
        private void InitializeAutoCompleteCombo()
        {
            ChevauchementCombo.ItemsSource = DataBase.context.Les_Permis.Select(p => p.Num_Permis).ToList();
        }
        #region Fill data 
        class DirInfo
        {
            public string DirName { get; set; }
            public string DirValue { get ; set; }
            public DirInfo(string dirName,string dirValue)
            {
                this.DirName = dirName;
                this.DirValue = dirValue;
            }
        }
        
        private void FillComboboxes()
        {
            ICollection<DirInfo> DirEOList = new List<DirInfo>() {
                new DirInfo("Est","e"),
                new DirInfo("Ouest","o")
            };
            Dir_e_o.ItemsSource = DirEOList;
            Dir_e_o.SelectedValuePath = "DirValue";
            Dir_e_o.DisplayMemberPath = "DirName";
            Dir_e_o.SetBinding(ComboBox.SelectedValueProperty, "Area.Dir_Est_ouest");


            ICollection<DirInfo> DirNSList = new List<DirInfo>() {
                new DirInfo("Nord","n"),
                new DirInfo("Sud","s")
            };
            dir_n_s.ItemsSource = DirNSList;
            dir_n_s.SelectedValuePath = "DirValue";
            dir_n_s.DisplayMemberPath = "DirName";
            dir_n_s.SetBinding(ComboBox.SelectedValueProperty, "Area.Dir_nord_sud");



            Carte.ItemsSource = DataBase.context.Cartes.ToList();
            Carte.SelectedValuePath = "CarteId";
            Carte.DisplayMemberPath = "Nom_carte";
            Point_Pevot.ItemsSource = DataBase.context.Point_Pivots.ToList();
            Point_Pevot.SelectedValuePath = "Point_PivotId";
            Point_Pevot.DisplayMemberPath = "Nom_Point_Pevot";
            Region.ItemsSource = DataBase.context.Regions.ToList();
            Region.SelectedValuePath = "RegionId";
            Region.DisplayMemberPath = "Nom_Region";
            //Province.SetBinding(ComboBox.ItemsSourceProperty,"";
            Province.ItemsSource = DataBase.context.Provinces.ToList();
            Province.SelectedValuePath = "ProvinceId";
            Province.DisplayMemberPath = "Nom_Province";
            Caidat.ItemsSource = DataBase.context.Caidats.ToList();
            Caidat.SelectedValuePath = "CaidatId";
            Caidat.DisplayMemberPath = "Nom_Caidat";
            Commune.ItemsSource = DataBase.context.Communes.ToList();
            Commune.SelectedValuePath = "CommuneId";
            Commune.DisplayMemberPath = "Nom_Commune";
            //Bind them
            Carte.SetBinding(ComboBox.SelectedValueProperty, "Area.CarteId");
            Region.SetBinding(ComboBox.SelectedValueProperty, "Area.Commune.Caidat.Province.RegionId");
            Province.SetBinding(ComboBox.SelectedValueProperty, "Area.Commune.Caidat.ProvinceId");
            Point_Pevot.SetBinding(ComboBox.SelectedValueProperty, "Area.Point_PivotId");
            Commune.SetBinding(ComboBox.SelectedValueProperty, "Area.CommuneId");
            Caidat.SetBinding(ComboBox.SelectedValueProperty, "Area.Commune.CaidatId");

        }

        private void BindDatePickers()
        {
            Date_Depot.SetBinding(DatePicker.SelectedDateProperty, "Date_Depot");
            Date_Decision.SetBinding(DatePicker.SelectedDateProperty, "Date_Decision");
            Date_Echeance.SetBinding(DatePicker.SelectedDateProperty, "Echeance");
            Date_Institision.SetBinding(DatePicker.SelectedDateProperty, "Date_Institition");
        }
        private void FillElementDossiers()
        {
            SetVerificationDossier();
        }
        private void SetVerificationDossier()
        {
            this.Permis.Permis_ElementDossiers.ToList().ForEach(ED =>
            {
                ElementsDossierStack.Children.Add(GetElementDossierTemplate(ED));
            });

        }
        private Border GetElementDossierTemplate(Permis_ElementDossier dataSource)
        {
            TextBlock textBlock = new TextBlock()
            {
                FontSize = 14,
                FontFamily = new FontFamily("Arial"),
                TextWrapping = TextWrapping.Wrap,
            };
            CheckBox checkBox = new CheckBox() ;
            Grid grid = new Grid()
            {
                Margin = new Thickness(5, 10, 0, 5)
            };
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10,GridUnitType.Star) }) ;
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) }) ;
            grid.Children.Add(textBlock);
            grid.Children.Add(checkBox);
            textBlock.SetValue(Grid.ColumnProperty, 0);
            checkBox.SetValue(Grid.ColumnProperty, 1);
            Border border = new Border()
            {
                DataContext = dataSource,
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Gray,
                Margin = new Thickness(5, 10, 5, 0),
                Padding = new Thickness(10, 10, 10, 10),
                Child = grid
            };
            
            textBlock.SetBinding(TextBlock.TextProperty, "Element_Dossier.nom_dossier");
            checkBox.SetBinding(CheckBox.IsCheckedProperty, "isExist");
            return border;
        }
        private void BindTextBoxes()
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
            NumPermisCheckBox.SetBinding(CheckBox.IsCheckedProperty, "isDecisionSigne");
            ProgrammeTravauxCheckBox.SetBinding(CheckBox.IsCheckedProperty, "isProgrammeTravauxExist");
            DeclarationTravauxCheckBox.SetBinding(CheckBox.IsCheckedProperty, "isDeclarationOuverture");
            Superficie.SetBinding(TextBox.TextProperty, "Area.Superficie");
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
                  this.Permis.Chevauchements.Add(DataBase.context.Les_Permis.Single(p=>p.Num_Permis==numero));
              });
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //I will focus on two diffrent Textboxes to terminate the binding
            Numero_Demande.Focus();
            Superficie.Focus();
            //====
            UpdateChevauchements();
            //update Etat Permis
            if (Numero_Permis.Text != "0")
                PermisState.updateEtat(this.Permis, EtatPermis.Permis);
            DataBase.context.SaveChanges();
            this.Home.RemplirDataGrid();
        }
       
        #endregion
        #region add Chevauchemnet area
        private void addChevauchement_Click(object sender, RoutedEventArgs e)
        {
            int num_Permis=Convert.ToInt32(ChevauchementCombo.Text);
            bool isExist=DataBase.context.Les_Permis.Any(p => p.Num_Permis == num_Permis);
            if (isExist
                )
            {
                Chevauchements.Children.Add(GetChevauchementElement(num_Permis));
                return;
            }
            ChevauchementCombo.BorderBrush = Brushes.Red;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ChevauchementCombo.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF2196F3");
                });
            });

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
        #endregion
        #region validation
        private void GetOnlyNumbers_KeyDown(object sender, KeyEventArgs e)
        {
            List<Key> keys = new List<Key>()
            {
                Key.D0,
                Key.D1,
                Key.D2,
                Key.D3,
                Key.D4,
                Key.D5,
                Key.D6,
                Key.D7,
                Key.D8,
                Key.D9,
                Key.NumPad0,
                Key.NumPad1,
                Key.NumPad2,
                Key.NumPad3,
                Key.NumPad4,
                Key.NumPad5,
                Key.NumPad6,
                Key.NumPad7,
                Key.NumPad8,
                Key.NumPad9,
            };
            
            if (!keys.Contains(e.Key))
            {
                TextBox textBox = sender as TextBox;
                textBox.BorderBrush = Brushes.Red;
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(2000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        textBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF2196F3");
                    });
                });
                e.Handled = true;
            }
        }
        #endregion

        private void Exporter_excel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path="";
                var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                if (dialog.ShowDialog(this).GetValueOrDefault())
                {
                    path = dialog.SelectedPath;
                }
                using (IXLWorkbook xL = new XLWorkbook())
                {
                    var data = DataBase.context.Les_Permis.Select(p => new {
                        numeroPermis = p.Num_Permis.ToString(),
                        numeroDemmande = p.Num_Demmande.ToString(),
                        dateDepotDemmande = p.Date_Depot.ToString(),
                        type = p.Type_Permis.Type.ToString(),
                        ex_permis = p.Ex_Permis.Num_Permis.ToString(),
                        //chevauchement = p.Chevauchements.Select(c => c.Num_Permis).ToArray().ToString(),
                        Superficie = p.Area.Superficie.ToString(),
                        pointPivot = p.Area.Point_Pivot.Nom_Point_Pevot.ToString(),
                        Abscisse = p.Area.Abscisse.ToString(),
                        Ordonne = p.Area.Ordonnee.ToString(),
                        //DirEastOuest=p.Area.Dir_Est_ouest.ToString(),
                        //DirNordSud=p.Area.Dir_nord_sud.ToString(),
                        //DisEastOust=p.Area.Dis_e_o.ToString(),
                        //DisNorSud=p.Area.Dis_n_s.ToString(),
                        //Bornes
                        Zone = p.Area.Zone.ToString(),
                        RaisonSocial = p.Titulaire.Raison_Social.ToString(),
                        Titulaire = p.Titulaire.Nom_Societe,
                        Region = p.Area.Commune.Caidat.Province.Region.Nom_Region.ToString(),
                        Province = p.Area.Commune.Caidat.Province.Nom_Province.ToString(),
                        CodeProvince = p.Area.Commune.Caidat.Province.code_Province.ToString(),
                        Commune = p.Area.Commune.Nom_Commune.ToString(),
                        Caidat = p.Area.Commune.Caidat.Nom_Caidat.ToString(),
                        Date_Institision = p.Date_Institition.ToString(),
                        Carte = p.Area.Carte.Nom_carte.ToString(),
                        Echeance = p.Echeance.ToString(),
                        //demmandeRenouvelomentPR
                        //demmandeDeLE
                        //demmandeDeRenouvelementLE
                        //observation
                        //anneDeLacte
                        //substanceRechercheOuExploite
                        NomSite = p.Titulaire.Nom_Site.ToString(),
                        //CorrespondanceSEP
                        DateDepart_CRI_CF_DMH_DR = p.Date_Depart_CRI.ToString(),
                        DateReutor_CRI = p.Date_Retour_CRI.ToString(),
                        Date_Decision = p.Date_Decision.ToString(),
                        Date_Enquete = p.Date_Enquete.ToString(),
                        //Date_Rapport_Enquete
                        Election_Domicile = p.Titulaire.Election_Domicile.ToString(),
                        Effective = p.Titulaire.Effictif.ToString(),
                        Investisement_Realise = p.Investisement_Realise.ToString(),
                        Investisement_Projete = p.Investisement_Projet.ToString(),
                        occupation_temporaire = p.Occupation_Temporaire.ToString(),
                        Inscription_Conservation = p.Inscription_Conservation.ToString()
                    });
                    xL.Worksheets.Add(data.CopyToDataTable(), "LesPermis");
                    var details=xL.AddWorksheet("Style");
                    xL.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    xL.Style.Font.Bold = true;
                    details.Columns().AdjustToContents();
                    details.Rows().AdjustToContents();
                    details.Columns().Style.Fill.SetBackgroundColor(XLColor.BlueBell);
                    path += @"\Les Permis Excel.xlsx";
                    xL.SaveAs(path);
                    ModalInfo.ShowMsg("Les Informations ont ete sauvegarder sous format excel");
                  }
                }
            catch(Exception ex)
            {
                ModalError.ShowMsg(ex.Message);
            };
        }

        private void Numero_Demande_MouseLeave(object sender, MouseEventArgs e)
        {
            List<int?> numerosDemmandes = DataBase.context.Les_Permis.Select(p => p.Num_Demmande).ToList();
            numerosDemmandes.Remove(CurrentNumeroDemmand);
            if (string.IsNullOrEmpty(Numero_Demande.Text))
            {
                Numero_Demande.Text = CurrentNumeroDemmand.ToString();
                return;
            }
            int EnteredNumeroDemmand =Convert.ToInt32(Numero_Demande.Text);
            if (numerosDemmandes.Contains(EnteredNumeroDemmand))
            {
                ModalError.ShowMsg("Ce Numero De Demmande Deja Exist .");
                Numero_Demande.Text = CurrentNumeroDemmand.ToString();
            }
        }

        private void Numero_Permis_MouseLeave(object sender, MouseEventArgs e)
        {
            List<int?> numerosPermis = DataBase.context.Les_Permis.Select(p => p.Num_Permis).Distinct().ToList();
            numerosPermis.Remove(CurrentNumeroPermis);
            numerosPermis.Remove(0);
            if (string.IsNullOrEmpty(Numero_Permis.Text))
            {
                Numero_Permis.Text = CurrentNumeroPermis.ToString();
                return;
            }
            int EnteredNumeroPermis = Convert.ToInt32(Numero_Permis.Text);
            if (numerosPermis.Contains(EnteredNumeroPermis))
            {
                ModalError.ShowMsg("Ce Numero De Permis Deja Exist .");
                Numero_Permis.Text = CurrentNumeroPermis.ToString();
            }
        }

        private void GenererRapport_Click(object sender, RoutedEventArgs e)
        {
            Numero_Demande.Focus();
            Superficie.Focus();
            DataBase.context.SaveChanges();
            Rapport_PR.Show(this.Permis);
        }
        bool isNavigationListClosed = true;
        private void NaviagationList_Click(object sender, RoutedEventArgs e)
        {
            if (isNavigationListClosed)
            {
                NavigationList.Height = Double.NaN;
                isNavigationListClosed = false;
            }
            else
            {
                NavigationList.Height = 0;
                isNavigationListClosed = true;
            }
        }
        private void Renouveller_Click(object sender, RoutedEventArgs e)
        {
            if (this.Permis.Etat_PermisId == EtatPermis.Renouvelle)
            {
                MessageBox.Show("deja Rennouvelle", "Message");
                return;
            }
            winform.DialogResult result = winform.MessageBox.Show("Vous veullez renouveller se permis", "Rennouvellement", winform.MessageBoxButtons.YesNo);
            if (result == winform.DialogResult.Yes)
            {
                Permis newPermis = new Permis(this.Permis.Area, this.Permis.Titulaire);
                newPermis.Ex_PermisId = this.Permis.PermisId;
                newPermis.Type_PermisId = TypePermis.PRR;
                this.Permis.Etat_PermisId = EtatPermis.Renouvelle;
                DataBase.context.Les_Permis.Add(newPermis);
                DataBase.context.SaveChanges();
                Permis_Recherche_Rennouvelle.ShowExistingPermis(this.Home, newPermis.PermisId);
                //InitilializerLesDossierPermis.InitilizerDossiers(newPermis, TypePermis.PR);
            }
        }
    }
}
