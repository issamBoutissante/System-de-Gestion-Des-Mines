using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projet_Mines_Official
{
    class RapportPath
    {
        public string Value { get; set; }
        public RapportPath(string value)
        {
            this.Value = value;
        }
        //PR
        public static RapportPath Decision_PR { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\Decision_PR.docx")); } }
        public static RapportPath Bon_achat { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\Bon_achat.docx")); } }
        public static RapportPath Bulletin_Versement_PR { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\Bulletin de versement PR.docx")); } }
        public static RapportPath premier_mise_demeure { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\premier_mise_demeure.docx")); } }
        public static RapportPath deuxieme_mise_demeure { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\deuxieme_mise_demeure.docx")); } }
        public static RapportPath lettre_transmission_PR { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\lettre_transmission_PR.docx")); } }
        public static RapportPath Revocation_PR { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\Revocation_PR.docx")); } }
        public static RapportPath Bordereau_envoi_PR_DMH { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\Bordereau_envoi_PR_DMH.docx")); } }
        public static RapportPath Bordereau_envoi_PR_DP { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\Bordereau_envoi_PR_DP.docx")); } }
        public static RapportPath Bordereau_envoi_PR_Conservation { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\Bordereau_envoi_PR_Conservation.docx")); } }
        public static RapportPath Programme_Traveux_Canvas { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\Programme_Traveux_Canvas.docx")); } }
        public static RapportPath Lettre_Revocation_Renonciation { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\Lettre_Revocation_Renonciation.docx")); } }
        public static RapportPath Lettre_Decision_Revocation { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\Lettre_Decision_Revocation.docx")); } }
        public static RapportPath Declaration_Travaux { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PR\\Declaration_Travaux.docx")); } }
        
        //PRR
        public static RapportPath Invitatation_Enquete_Rennouvelement { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PRR\\Invitatation_Enquete_Rennouvelement.docx")); } }
        public static RapportPath Lettre_Transmission_Decision_PRR { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PRR\\Lettre_Transmission_Decision.docx")); } }

        //LE
        public static RapportPath Premier_Mise_Demeur_LE { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\LE\\Premier_Mise_Demeur_LE.docx")); } }
        public static RapportPath Deuxieme_Mise_Demeur_LE { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\LE\\Deuxieme_Mise_Demeur_LE.docx")); } }
        public static RapportPath Note_Service { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\LE\\Note_Service.docx")); } }
        public static RapportPath Invitation_Enquete_Transformation_LE { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\LE\\Invitation_Enquete_Transformation_LE.docx")); } }
        public static RapportPath Lettre_Transmission_Decision_LE { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\LE\\Lettre_Transmission_Decision_LE.docx")); } }
        public static RapportPath Bordureu_Envoi_DMH_LE { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\LE\\Bordureu_Envoi_DMH_LE.docx")); } }
        public static RapportPath Bordureu_Envoi_DP_LE { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\LE\\Lettre_Transmission_Decision_LE.docx")); } }
        public static RapportPath Bordureu_Envoi_Conservation_LE { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\LE\\Bordureu_Envoi_Conservation_LE.docx")); } }
        public static RapportPath Lettre_Revocation_Renonciation_LE { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\LE\\Lettre_Revocation_Renonciation_LE.docx")); } }
        public static RapportPath Lettre_Transmission_Decision_Revocation_LE { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\LE\\Lettre_Transmission_Decision_Revocation_LE.docx")); } }

        //LEE
        public static RapportPath Invitation_Enquete_Renouvellement_LE { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\LEE\\Invitation_Enquete_Renouvellement_LE.docx")); } }
        public static RapportPath Lettre_Transmission_Decision_Renouvellement_LE { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\LEE\\Lettre_Transmission_Decision_Renouvellement_LE.docx")); } }

        private static string GetPathFromCurrentProject(string FolderOrFileName)
        {
            return $@"{Directory.GetCurrentDirectory().Replace(@"\bin\Debug", "\\")}{FolderOrFileName}";
        }

    }
}
