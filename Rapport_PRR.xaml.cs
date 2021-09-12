using System;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for Rapport_PRR.xaml
    /// </summary>
    public partial class Rapport_PRR : Window
    {
       
        string NomSociete;
        string NumeroDemande;
        string Num_PR;
        DateTime dateDemmande;
        public Rapport_PRR(Permis permis)
        {
            InitializeComponent();
            NomSociete = permis.Titulaire.Nom_Societe;
            NumeroDemande = permis.Num_Demmande.ToString();
            Num_PR = permis.Num_Permis.ToString();
            dateDemmande = permis.Date_Depot;
        }
        public void Show(Permis permis)
        {
            new Rapport_PRR(permis).ShowDialog();
        }
        private void InvitationEnqueteRenouvelement_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Invitatation_Enquete_Rennouvelement.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<num_pr>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<num_dm>", NumeroDemande);
                    DocumentGenerator.FindAndReplace(wordApp, "<date_dm>", $"{dateDemmande.Day} / {dateDemmande.Month} /{dateDemmande.Year}");
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void Lettre_Transmission_Decision_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Lettre_Transmission_Decision_PRR.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<num_pr>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<date_dm>", $"{dateDemmande.Day} / {dateDemmande.Month} /{dateDemmande.Year}");
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
