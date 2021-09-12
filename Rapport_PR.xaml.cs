using System;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for Rapport_PR.xaml
    /// </summary>
    public partial class Rapport_PR : Window
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
        string DisEstO ;
        string date_decision;
        string date_plus_trois;
        string nomDemandeur;
        string nomCaidat;
        string nomProvince;
        public Rapport_PR(Permis permis)
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
            Dis_SN =permis.Area.Dis_n_s;
            DisEstO = permis.Area.Dis_e_o;
            date_decision = permis.Date_Decision.ToShortDateString();
            date_plus_trois = permis.Date_Decision.AddYears(3).ToShortDateString();
            nomDemandeur = permis.Titulaire.Nom_Demandeur;
            nomCaidat = permis.Area.Commune.Caidat.Nom_Caidat;
            nomProvince = permis.Area.Commune.Caidat.Province.Nom_Province;
        }
        internal static void Show(Permis permis)
        {
            new Rapport_PR(permis).ShowDialog();
        }
        private void BonAchat_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Bon_achat.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<abscisse>", abscisse);
                    DocumentGenerator.FindAndReplace(wordApp, "<ordonnee>", ordonnee);
                    DocumentGenerator.FindAndReplace(wordApp, "<carte>", carte);
                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void BultainVersement_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();      
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
        private void RegetDemande_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Revocation_PR.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<Num_PR>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void DicisionPR_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Decision_PR.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<abscisse>", abscisse);
                    DocumentGenerator.FindAndReplace(wordApp, "<ordonnee>", ordonnee);
                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<Num_PR>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<carte>", carte);
                    DocumentGenerator.FindAndReplace(wordApp, "<Dis_SN>", Dis_SN);
                    DocumentGenerator.FindAndReplace(wordApp, "<DisEstO>", DisEstO);
                    DocumentGenerator.FindAndReplace(wordApp, "<date_decision>", date_decision);
                    DocumentGenerator.FindAndReplace(wordApp, "<date_plus_trois>", date_plus_trois);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
            PermisState.updateEtat(this.Permis, EtatPermis.Decision);
        }
        private void LettreTransmissionTitulaire_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.lettre_transmission_PR.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<Num_PR>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<Num_DR>", NumeroDemande);
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
                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<Num_PR>", Num_PR);
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void BurdereauEnvoiDMH_Click(object sender, RoutedEventArgs e)
        {
            Generer_Bordereau_Denvoi(RapportPath.Bordereau_envoi_PR_DMH.Value);
        }
        private void BurdereauEnvoiDP_Click(object sender, RoutedEventArgs e)
        {
            Generer_Bordereau_Denvoi(RapportPath.Bordereau_envoi_PR_DP.Value);
        }
        private void BurdereauEnvoiConservation_Click(object sender, RoutedEventArgs e)
        {
            Generer_Bordereau_Denvoi(RapportPath.Bordereau_envoi_PR_Conservation.Value);
        }
        private void PremiereMiseDemeure_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.premier_mise_demeure.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<Num_PR>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void DeuxiemeMiseDemeure_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.deuxieme_mise_demeure.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void ProgrammeTravauxCanvas_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Programme_Traveux_Canvas.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<num_pr>", Num_PR);
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void LettreRevocationRenonciation_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Lettre_Revocation_Renonciation.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<num_pr>", Num_PR);
                    DocumentGenerator.FindAndReplace(wordApp, "<date>", $"{DateTime.Now.Day} / {DateTime.Now.Month} /{DateTime.Now.Year}");
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void LettreDecisionRevocation_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();
            DocumentGenerator.GenerateDocument(RapportPath.Lettre_Revocation_Renonciation.Value,
                (Word.Application wordApp) =>
                {
                    DocumentGenerator.FindAndReplace(wordApp, "<nom_societe>", NomSociete);
                    DocumentGenerator.FindAndReplace(wordApp, "<num_pr>", Num_PR);
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
                }
                , dw.documentsContainer, () => { dw.Show(); });
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
