using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Mines_Official
{
    class RapportPath
    {
        public string Value { get; set; }
        public RapportPath(string value)
        {
            this.Value = value;
        }
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
        public static RapportPath Invitatation_Enquete_Rennouvelement { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PRR\\Invitatation_Enquete_Rennouvelement.docx")); } }
        public static RapportPath Lettre_Transmission_Decision_PRR { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\PRR\\Lettre_Transmission_Decision.docx")); } }

        private static string GetPathFromCurrentProject(string FolderOrFileName)
        {
            return $@"{Directory.GetCurrentDirectory().Replace(@"\bin\Debug", "\\")}{FolderOrFileName}";
        }

    }
}
