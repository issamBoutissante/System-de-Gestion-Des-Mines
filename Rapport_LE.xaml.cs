using System;
using System.Linq;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;


namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for Rapport_LE.xaml
    /// </summary>
    public partial class Rapport_LE : Window
    {
        Permis Permis;
        string abscisse;
        string ordonnee;
        string carte;
        string NomSociete;
        string DomicileDemandeur;
        string RegistreCommerce;
        string NumeroCNSS;
        string TaxeProf;
        string NumeroDemande;
        string Num_PR;
        string Dis_SN;
        string DisEstO;
        string date_decision;
        string date_plus_trois;
        string nomDemandeur;
        string nomCaidat;
        string nomProvince;
        string nomCarte;
        string nomPointPevot;
        DateTime dateDemmande;
        string numExPermis;
        public Rapport_LE(Permis permis)
        {
            InitializeComponent();
            this.Permis = permis;
            abscisse = permis.Area.Abscisse;
            ordonnee = permis.Area.Ordonnee;
            carte = permis.Area.Carte.Nom_carte;
            NomSociete = permis.Titulaire.Nom_Societe;
            DomicileDemandeur = permis.Titulaire.Election_Domicile;
            RegistreCommerce = permis.Titulaire.Registre_Commerce;
            NumeroCNSS = permis.Titulaire.Numero_Cnss;
            TaxeProf = permis.Titulaire.Taxe_Prof;
            NumeroDemande = permis.Num_Demmande.ToString();
            Num_PR = permis.Num_Permis.ToString();
            Dis_SN = permis.Area.Dis_n_s;
            DisEstO = permis.Area.Dis_e_o;
            date_decision = permis.Date_Decision.ToShortDateString();
            date_plus_trois = permis.Date_Decision.AddYears(3).ToShortDateString();
            nomDemandeur = permis.Titulaire.Nom_Demandeur;
            nomCaidat = permis.Area.Commune.Caidat.Nom_Caidat;
            nomProvince = permis.Area.Commune.Caidat.Province.Nom_Province;
            nomCarte = permis.Area.Carte.Nom_carte;
            nomPointPevot = permis.Area.Point_Pivot.Nom_Point_Pevot;
            dateDemmande = permis.Date_Depot;
            //i will update it later
            numExPermis = permis.Licence_Permis.ToList().First().Num_Permis.ToString();
        }
        public static void Show(Permis permis)
        {
            new Rapport_LE(permis).ShowDialog();
        }
        private void PremierMiseDemeur_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Premier_Mise_Demeur_LE.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<num_pr>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void DeuxiemeMiseDemeur_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Deuxieme_Mise_Demeur_LE.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<num_pr>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void InvitationEnquete_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Invitation_Enquete_Transformation_LE.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<num_dm>", NumeroDemande);
                    DocumentGenerator.FindAndReplace(wordApp, "<date_dm>", $"{dateDemmande.Day} / {dateDemmande.Month} /{dateDemmande.Year}");
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void NoteServiceEnquete_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Note_Service.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<num_dm>", NumeroDemande);
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<Num_PR>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_carte>", nomCarte);
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_point_pevot>", nomPointPevot);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void LettreTransmissionDecision_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Note_Service.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<num_pr>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<num_ex_pr>", numExPermis);
                    DocumentGenerator.FindAndReplace(wordApp, "<date_dm>", $"{dateDemmande.Day} / {dateDemmande.Month} /{dateDemmande.Year}");
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void Generer_Bordereau_Denvoi(string Path)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(Path,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<num_pr>", Num_PR);
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void BurdereauEnvoiDMH_Click(object sender, RoutedEventArgs e)
        {
            Generer_Bordereau_Denvoi(RapportPath.Bordureu_Envoi_DMH_LE.Value);
        }
        private void BurdereauEnvoiDP_Click(object sender, RoutedEventArgs e)
        {
            Generer_Bordereau_Denvoi(RapportPath.Bordureu_Envoi_DP_LE.Value);
        }
        private void BurdereauEnvoiConservation_Click(object sender, RoutedEventArgs e)
        {
            Generer_Bordereau_Denvoi(RapportPath.Bordureu_Envoi_Conservation_LE.Value);
        }
        private void LettreTransmissionRevocationRnonciation_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Lettre_Revocation_Renonciation_LE.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<num_pr>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void LettreTransmissionRevocationRevocation_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Lettre_Transmission_Decision_Revocation_LE.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<num_pr>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void DeclarationTravaux_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Lettre_Revocation_Renonciation.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<num_pr>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_demandeur>", nomDemandeur);
                    DocumentGenerator.FindAndReplace(wordApp, "<num_cnss>", NumeroCNSS);
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_caidat>", nomCaidat);
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_province>", nomProvince);
                    string bornes = "";
                    this.Permis.Area.Bornes.ToList().ForEach(borne =>
                    {
                        bornes += $"                       X={borne.Borne_X}  Y={borne.Borne_Y}\n";
                    });
                    DocumentGenerator.FindAndReplace(wordApp, "<bornes>", bornes);
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
    }
}
