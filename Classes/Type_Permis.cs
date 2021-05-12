using System.Collections.Generic;

namespace Projet_Mines_Official
{
    public class Type_Permis
    {
        public Type_Permis()
        {
            this.Les_Permis = new HashSet<Permis>();
            this.Element_Dossiers = new HashSet<Element_Dossier>();
            //Associate Default Values
            this.Type = "";
        }
        public int Type_PermisId { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Permis> Les_Permis { get; set; }
        public virtual ICollection<Element_Dossier> Element_Dossiers { get; set; }
    }
}
