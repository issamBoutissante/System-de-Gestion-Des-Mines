using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Mines_Official
{
    public class Commune
    {
        public Commune()
        {
            this.Areas = new HashSet<Area>();
            //Associate Default Values
            this.Nom_Commune = "";
            this.CaidatId = 1;
        }
        public int CommuneId { get; set; }
        public string Nom_Commune { get; set; }

        public int? CaidatId { get; set; }
        [ForeignKey("CaidatId")]
        public virtual Caidat Caidat { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
    }
}
