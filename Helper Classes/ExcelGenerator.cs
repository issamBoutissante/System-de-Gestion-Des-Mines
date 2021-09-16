using System.Windows;
using System.Linq;
using ClosedXML.Excel;
using System.Data;

namespace Projet_Mines_Official
{
    class ExcelGenerator
    {
        internal static void ConvertToEcxel()
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
        internal static void ExportExcel(Window window)
        {
            try
            {
                string path = "";
                var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                if (dialog.ShowDialog(window).GetValueOrDefault())
                {
                    path = dialog.SelectedPath;
                }
                using (IXLWorkbook xL = new XLWorkbook())
                {
                    var data = Global.context.Les_Permis.Select(p => new {
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
                    MessageBox.Show("Les Informations ont ete sauvegarder sous format excel");
                }
            }
            catch { };
        }
    }
}
