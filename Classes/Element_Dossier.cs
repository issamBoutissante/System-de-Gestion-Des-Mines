using System.Collections.Generic;
using System.ComponentModel;

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
        }
        public int Element_DossierId { get; set; }
        public string nom_dossier { get; set; }
        public bool isExist { get; set; }

        public virtual ICollection<Permis> Les_Permis { get; set; }
    }
}
