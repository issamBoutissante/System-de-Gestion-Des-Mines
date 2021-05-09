using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Mines_Official
{
    public class Area
    {
        public Area()
        {
            this.Les_Permis = new HashSet<Permis>();
            this.Bornes = new HashSet<Borne>();
            //associate default values 
            this.Superficie = 0;
            this.Abscisse = "";
            this.Ordonnee = "";
            this.Dir_Est_ouest = 'e';
            this.Dir_nord_sud = 'n';
            this.Dis_e_o = "";
            this.Dis_n_s = "";
            this.Zone = "";
            this.CarteId = 1;
            this.Point_PivotId = 1;
            this.CommuneId = 1;

        }
        public int AreaId { get; set; }
        public double? Superficie { get; set; }
        public string Abscisse { get; set; }
        public string Ordonnee { get; set; }
        public char? Dir_Est_ouest { get; set; }
        public char? Dir_nord_sud { get; set; }
        public string Dis_e_o { get; set; }
        public string Dis_n_s { get; set; }
        public string Zone { get; set; }
        //les relations
        public virtual ICollection<Permis> Les_Permis { get; set; }
        public virtual ICollection<Borne> Bornes { get; set; }

        public int? CarteId { get; set; }
        [ForeignKey("CarteId")]
        public virtual Carte Carte { get; set; }

        public int? Point_PivotId { get; set; } 
        [ForeignKey("Point_PivotId")]
        public virtual Point_Pivot Point_Pivot { get; set; }

        public int? CommuneId { get; set; }
        [ForeignKey("CommuneId")]
        public virtual Commune Commune { get; set; }
    }
}
