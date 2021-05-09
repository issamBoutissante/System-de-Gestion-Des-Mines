using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Mines_Official
{
    public class Caidat
    {
        public Caidat()
        {
            this.Communes = new HashSet<Commune>();
            //Associate Default Values
            this.Nom_Caidat = "";
            this.ProvinceId = 1;
        }
        public int CaidatId { get; set; }
        public string Nom_Caidat { get; set; }

        public int? ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }

        public virtual ICollection<Commune> Communes { get; set; }
    }
}
