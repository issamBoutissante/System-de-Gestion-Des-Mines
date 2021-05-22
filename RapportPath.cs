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
        public static RapportPath Decision_PR { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\Decision_PR.docx")); } }
        public static RapportPath Bon_achat { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\Bon_achat.docx")); } }
        public static RapportPath Bulletin_Versement_PR { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\Bulletin de versement PR.docx")); } }
        public static RapportPath premier_mise_demeure { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\premier_mise_demeure.docx")); } }
        public static RapportPath deuxieme_mise_demeure { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\deuxieme_mise_demeure.docx")); } }
        public static RapportPath lettre_transmission_PR { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\lettre_transmission_PR.docx")); } }
        public static RapportPath Bordereau_envoi_PR { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\Bordereau_envoi_PR.docx")); } }
        public static RapportPath Revocation_PR { get { return new RapportPath(GetPathFromCurrentProject("Rapports\\Revocation_PR.docx")); } }

        private static string GetPathFromCurrentProject(string FolderOrFileName)
        {
            return $@"{Directory.GetCurrentDirectory().Replace(@"\bin\Debug", "\\")}{FolderOrFileName}";
        }

    }
}
