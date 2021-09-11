using System;
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
using System.Collections.ObjectModel;

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for Rapport_PR.xaml
    /// </summary>
    public partial class Rapport_PR : Window
    {
        Permis Permis;
        public Rapport_PR(Permis permis)
        {
            InitializeComponent();
            this.Permis = permis;
        }
        private void BonAchat_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();


            string abscisse = this.Permis.Area.Abscisse;
            string ordonnee = this.Permis.Area.Ordonnee;
            string carte = this.Permis.Area.Carte.Nom_carte;
            string societe = this.Permis.Titulaire.Nom_Societe;
            string DomicileDemandeur = this.Permis.Titulaire.Election_Domicile;
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

        private void BultainVersement_Click(object sender, RoutedEventArgs e)
        {
            documentsWord dw = new documentsWord();

            string NomSociete = this.Permis.Titulaire.Nom_Societe;
            string RegistreCommerce = this.Permis.Titulaire.Registre_Commerce;
            string NumeroCNSS = this.Permis.Titulaire.Numero_Cnss;
            string TaxeProf = this.Permis.Titulaire.Taxe_Prof;
            string NumeroDemande = this.Permis.Num_Demmande.ToString();
            string DomicileDemandeur = this.Permis.Titulaire.Election_Domicile;
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
    }
}
