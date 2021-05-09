using System.Collections.Generic;

namespace Projet_Mines_Official
{
    public class Etat_Permis
    {
        public Etat_Permis()
        {
            this.Les_Permis = new HashSet<Permis>();
            //Associate Default Values
            this.Etat = "";
        }
        public int Etat_PermisId { get; set; }
        public string Etat { get; set; }

        public virtual ICollection<Permis> Les_Permis { get; set; }
    }
}
