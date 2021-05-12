using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Mines_Official
{
    public class Element_Dossier
    {
        public Element_Dossier()
        {
            this.Les_Permis = new HashSet<Permis>();
            //Associate Default Values
            this.nom_dossier = "";
            this.isExist = true;
            this.Type_PermisId = 1;
        }
        public int Element_DossierId { get; set; }
        public string nom_dossier { get; set; }
        public bool isExist { get; set; }

        public int Type_PermisId { get; set; }
        [ForeignKey("Type_PermisId")]
        public Type_Permis Type_Permis { get; set; }
        public virtual ICollection<Permis> Les_Permis { get; set; }
    }
}
