using System.Collections.Generic;

namespace Projet_Mines_Official
{
    public class Carte
    {
        public Carte()
        {
            this.Areas = new HashSet<Area>();
            //Associate Default Values
            this.Nom_carte = "";
        }
        public int CarteId { get; set; }
        public string Nom_carte { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
    }
}
