using System.Collections.Generic;

namespace Projet_Mines_Official
{
    public class Region
    {
        public Region()
        {
            this.Provinces = new HashSet<Province>();
            //Associate Default Values
            this.Nom_Region = "";
        }
        public int RegionId { get; set; }
        public string Nom_Region { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
    }
}
