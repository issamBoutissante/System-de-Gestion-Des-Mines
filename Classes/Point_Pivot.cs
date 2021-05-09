using System.Collections.Generic;

namespace Projet_Mines_Official
{
    public class Point_Pivot
    {
        public Point_Pivot()
        {
            this.Areas = new HashSet<Area>();
            //Associate Default Values
            this.Nom_Point_Pevot = "";
        }
        public int Point_PivotId { get; set; }
        public string Nom_Point_Pevot { get; set; }
        public  virtual ICollection<Area> Areas { get; set; }
    }
}
