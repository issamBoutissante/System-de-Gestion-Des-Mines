﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Media;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using ClosedXML.Excel;
using System.Data;

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for Permis_Recherche_Rennouvelle.xaml
    /// </summary>
    public partial class Permis_Recherche_Rennouvelle : Window
    {

        ProjetMinesDBContext projetMinesDBContext = new ProjetMinesDBContext();
        public Permis Permis { get; set; }
        Home Home;
        int? CurrentNumeroDemmand;
        int? CurrentNumeroPermis;
        public Permis_Recherche_Rennouvelle(Home home, int PermisId)
        {
            InitializeComponent();
            this.Home = home;
            this.Permis = projetMinesDBContext.Les_Permis.Find(PermisId);
            this.DataContext = this.Permis;
            InitializeControls();
            this.CurrentNumeroDemmand = this.Permis.Num_Demmande;
            this.CurrentNumeroPermis = this.Permis.Num_Permis;
        }
        internal static void ShowExistingPermis(Home home, int permisId)
        {
            new Permis_Recherche_Rennouvelle(home, permisId).ShowDialog();
        }

        private void InitializeControls()
        {
            FillComboboxes();
            BindDatePickers();
            FillElementDossiers();
            BindTextBoxes();

        }
       
        #region Fill data 
        private void FillComboboxes()
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
            CheckBox checkBox = new CheckBox();
            Grid grid = new Grid()
            {
                Margin = new Thickness(5, 10, 0, 5)
            };
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.5, GridUnitType.Star) });
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
            NumeroExPermis.SetBinding(TextBox.TextProperty, "Ex_Permis.Num_Permis");
            Numero_Demande.SetBinding(TextBox.TextProperty, "Num_Demmande");
            Nom_Demandeur.SetBinding(TextBox.TextProperty, "Titulaire.Nom_Demandeur");
            Status_Demandeur.SetBinding(TextBox.TextProperty, "Titulaire.status_Demandeur");
            Raison_Social.SetBinding(TextBox.TextProperty, "Titulaire.Raison_Social");
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
            Dir_e_o.SetBinding(TextBox.TextProperty, "Area.Dir_Est_ouest");
            dir_n_s.SetBinding(TextBox.TextProperty, "Area.Dir_nord_sud");
            Dis_n_s.SetBinding(TextBox.TextProperty, "Area.Dis_n_s");
            Dis_e_o.SetBinding(TextBox.TextProperty, "Area.Dis_e_o");
            foreach (Permis chev in this.Permis.Chevauchements)
            {
                Chevauchements.Children.Add(GetChevauchementElement((int)chev.Num_Permis));
            }
            Zone.SetBinding(TextBox.TextProperty, "Area.Zone");
            Abscisse.SetBinding(TextBox.TextProperty, "Area.Abscisse");
            Ordonne.SetBinding(TextBox.TextProperty, "Area.Ordonnee");
            //suivi decision information
            Numero_Permis.SetBinding(TextBox.TextProperty, "Num_Permis");
            investisement_realise.SetBinding(TextBox.TextProperty, "Investisement_Realise");
            occupation_temporaire.SetBinding(TextBox.TextProperty, "Occupation_Temporaire");

            double daysPassed = (DateTime.Now.Date - this.Permis.Date_Decision.Date).TotalDays;
            RestJourProgramme.Text = $"Rest : {180 - daysPassed}";
            RestJourDeclarationTravaux.Text = $"Rest : {360 - daysPassed}";
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
                this.Permis.Chevauchements.Add(this.projetMinesDBContext.Les_Permis.Single(p => p.Num_Permis == numero));
            });
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Numero_Demande.Focus();
            UpdateChevauchements();
            //update Etat Permis
            if (Numero_Permis.Text != "0")
                this.Permis.Etat_PermisId = (int)EtatPermis.Permis;
            this.projetMinesDBContext.SaveChanges();
            this.Home.RemplirDataGrid();
        }
        #endregion
        #region Chevauchemnet area
        private Button GetChevauchementElement(int NumPermis)
        {
            Button btn = new Button()
            {
                Content = NumPermis.ToString(),
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.White,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(10, 0, 0, 0),
                Foreground = Brushes.Gray
            };
            return btn;
        }
        #endregion
        #region Generation Des Rapport
        private void GenererBLV_PR_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();

            string NomSociete = Nom_Societe.Text;
            string RegistreCommerce = Registre_Commerce.Text;
            string NumeroCNSS = Numero_CNSS.Text;
            string TaxeProf = Taxe_Prof.Text;
            string NumeroDemande = Numero_Demande.Text;
            string DomicileDemandeur = Domicile_Demandeur.Text;
            DocumentGenerator.GenerateDocument(RapportPath.Bulletin_Versement_PR.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<anne>", DateTime.Now.Year.ToString());
                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<registreCommerce>", RegistreCommerce);
                    DocumentGenerator.FindAndReplace(wordApp, "<cnss>", NumeroCNSS);
                    DocumentGenerator.FindAndReplace(wordApp, "<taxeProf>", TaxeProf);
                    DocumentGenerator.FindAndReplace(wordApp, "<numeroDemande>", NumeroDemande);
                    DocumentGenerator.FindAndReplace(wordApp, "<domicile>", DomicileDemandeur);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }

                , dw.documentsContainer, () => { dw.Show(); });
        }

        private void Generer_Bon_achat_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();


            string abscisse = Abscisse.Text;
            string ordonnee = Ordonne.Text;
            string carte = Carte.Text;
            string societe = Nom_Societe.Text;
            string DomicileDemandeur = Domicile_Demandeur.Text;
            DocumentGenerator.GenerateDocument(RapportPath.Bon_achat.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<abscisse>", abscisse);
                    DocumentGenerator.FindAndReplace(wordApp, "<ordonnee>", ordonnee);
                    DocumentGenerator.FindAndReplace(wordApp, "<carte>", carte);
                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", societe);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }

                , dw.documentsContainer, () => { dw.Show(); });
        }

        private void Mise_demeur_TP_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();

            string societe = Nom_Societe.Text;
            string Num_PR = Numero_Permis.Text;
            DocumentGenerator.GenerateDocument(RapportPath.premier_mise_demeure.Value,
                (Word.Application wordApp) =>
                {

                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", societe);
                    DocumentGenerator.FindAndReplace(wordApp, "<Num_PR>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }

                , dw.documentsContainer, () => { dw.Show(); });
        }

        private void Mise_demeur_OuvertureTravaux_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();

            string societe = Nom_Societe.Text;
            string Num_PR = Numero_Permis.Text;
            DocumentGenerator.GenerateDocument(RapportPath.deuxieme_mise_demeure.Value,
                (Word.Application wordApp) =>
                {

                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", societe);
                    DocumentGenerator.FindAndReplace(wordApp, "<Num_PR>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }

                , dw.documentsContainer, () => { dw.Show(); });
        }

        private void Generer_Decision_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            string abscisse = Abscisse.Text;
            string ordonnee = Ordonne.Text;
            string societe = Nom_Societe.Text;
            string Num_PR = Numero_Permis.Text;
            string carte = Carte.SelectedValue.ToString();
            string Dis_SN = Dis_n_s.Text;
            string DisEstO = Dis_e_o.Text;
            string date_decision = Date_Decision.Text;
            string date_plus_trois = Date_Decision.SelectedDate.Value.AddYears(3).ToString();
            DocumentGenerator.GenerateDocument(RapportPath.Decision_PR.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<abscisse>", abscisse);
                    DocumentGenerator.FindAndReplace(wordApp, "<ordonnee>", ordonnee);
                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", societe);
                    DocumentGenerator.FindAndReplace(wordApp, "<Num_PR>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<carte>", carte);
                    DocumentGenerator.FindAndReplace(wordApp, "<Dis_SN>", Dis_SN);
                    DocumentGenerator.FindAndReplace(wordApp, "<DisEstO>", DisEstO);
                    DocumentGenerator.FindAndReplace(wordApp, "<date_decision>", date_decision);
                    DocumentGenerator.FindAndReplace(wordApp, "<date_plus_trois>", date_plus_trois);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }

                , dw.documentsContainer, () => { dw.Show(); });
            this.Permis.Etat_PermisId = (int)EtatPermis.Decision;
        }

        private void Generer_Lettre_Transmission_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();

            string societe = Nom_Societe.Text;
            string Num_PR = Numero_Permis.Text;
            DocumentGenerator.GenerateDocument(RapportPath.lettre_transmission_PR.Value,
                (Word.Application wordApp) =>
                {

                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", societe);
                    DocumentGenerator.FindAndReplace(wordApp, "<Num_PR>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }

                , dw.documentsContainer, () => { dw.Show(); });
        }

        private void Generer_Bordereau_envoi_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();

            string societe = Nom_Societe.Text;
            string Num_PR = Numero_Permis.Text;
            DocumentGenerator.GenerateDocument(RapportPath.Bordereau_envoi_PR.Value,
                (Word.Application wordApp) =>
                {

                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", societe);
                    DocumentGenerator.FindAndReplace(wordApp, "<Num_PR>", Num_PR);
                }

                , dw.documentsContainer, () => { dw.Show(); });
        }

        private void Rejet_demande_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();

            string societe = Nom_Societe.Text;
            string Num_PR = Numero_Permis.Text;
            DocumentGenerator.GenerateDocument(RapportPath.Revocation_PR.Value,
                (Word.Application wordApp) =>
                {

                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", societe);
                    DocumentGenerator.FindAndReplace(wordApp, "<Num_PR>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }

                , dw.documentsContainer, () => { dw.Show(); });
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
                string path = "";
                var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                if (dialog.ShowDialog(this).GetValueOrDefault())
                {
                    path = dialog.SelectedPath;
                }
                using (IXLWorkbook xL = new XLWorkbook())
                {
                    var data = this.projetMinesDBContext.Les_Permis.Select(p => new {
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
                    var details = xL.AddWorksheet("Style");
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
            catch (Exception ex)
            {
                ModalError.ShowMsg(ex.Message);
            };
        }

        private void Numero_Demande_MouseLeave(object sender, MouseEventArgs e)
        {
            List<int?> numerosDemmandes = projetMinesDBContext.Les_Permis.Select(p => p.Num_Demmande).ToList();
            numerosDemmandes.Remove(CurrentNumeroDemmand);
            if (string.IsNullOrEmpty(Numero_Demande.Text))
            {
                Numero_Demande.Text = CurrentNumeroDemmand.ToString();
                return;
            }
            int EnteredNumeroDemmand = Convert.ToInt32(Numero_Demande.Text);
            if (numerosDemmandes.Contains(EnteredNumeroDemmand))
            {
                ModalError.ShowMsg("Ce Numero De Demmande Deja Exist .");
                Numero_Demande.Text = CurrentNumeroDemmand.ToString();
            }
        }

        private void Numero_Permis_MouseLeave(object sender, MouseEventArgs e)
        {
            List<int?> numerosPermis = projetMinesDBContext.Les_Permis.Select(p => p.Num_Permis).Distinct().ToList();
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
        #region Le Code De PRR
        private void AfficherExPermis_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
