using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Word = Microsoft.Office.Interop.Word;

namespace Projet_Mines_Official
{
    public partial class Permis_Recherche : Window
    {
        ProjetMinesDBContext projetMinesDBContext;
        public Permis_Recherche(ProjetMinesDBContext projetMinesDBContext)
        {
            InitializeComponent();
            this.projetMinesDBContext = projetMinesDBContext;
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
            permis.Titulaire.Effictif = int.Parse(Effective.Text);

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
        //private void Enregistrer_DemmandeInfo_Click(object sender, RoutedEventArgs e)
        //{
        //    //si le permis deja exist on vas le modifier si non on va l'ajouter
        //    int numDemmande = int.Parse(Numero_Demande.Text);
        //    Permis permis=null;
        //    try
        //    {
        //        permis = projetMinesDBContext.Les_Permis.Where(p=>p.Num_Demmande==numDemmande).Single();
        //    }
        //    catch { }
        //    if (permis == null)
        //    {
        //        permis = new Permis();
        //        UpdateDemmandeInfo(permis);
        //        projetMinesDBContext.Les_Permis.Add(permis);
        //        //permis.Etat_Permis = projetMinesDBContext.Etats_Permis.Find(0);
        //        //permis.Type_Permis = projetMinesDBContext.Types_Permis.Find(0);
        //    }
        //    else
        //        UpdateDemmandeInfo(permis);
        //    projetMinesDBContext.SaveChanges();
        //}
        //private void Enregistrer_Titulaire_Info_Click(object sender, RoutedEventArgs e)
        //{
        //    int numDemmande = int.Parse(Numero_Demande.Text);
        //    Permis permis = projetMinesDBContext.Les_Permis.Where(p=>p.Num_Demmande==numDemmande).Single();
        //    UpdateTitulaireInfo(permis);
        //    projetMinesDBContext.SaveChanges();
        //}
        //private void Enregistrer_Area_Info_Click(object sender, RoutedEventArgs e)
        //{
        //    int numDemmande = int.Parse(Numero_Demande.Text);
        //    Permis permis = projetMinesDBContext.Les_Permis.Where(p=>p.Num_Demmande==numDemmande).Single();
        //    UpdateAreaInfo(permis);
        //    projetMinesDBContext.SaveChanges();
        //}
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            dw.Show();
            this.Close();
        }
    }
}
