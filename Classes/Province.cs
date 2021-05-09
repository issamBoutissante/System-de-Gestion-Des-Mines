using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Mines_Official
{
    public class Province
    {
        public Province()
        {
            this.Caidats = new HashSet<Caidat>();
            //Associate Default Values
            this.Nom_Province = "";
            this.code_Province = "";
            this.RegionId = 1;
        }
        public int ProvinceId { get; set; }
        public string Nom_Province { get; set; }
        public string code_Province { get; set; }

        public int? RegionId { get; set; }
        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }

        public virtual ICollection<Caidat> Caidats { get; set; }
    }
}
