using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Mines_Official
{
    public class Area
    {
        public int Id { get; set; }
        public double Superficie { get; set; }
        public string Abscisse { get; set; }
        public string Ordonnee { get; set; }
        public char Dir_Est_ouest { get; set; }
        public char Dir_nord_sud { get; set; }
        public string Dis_e_o { get; set; }
        public string Dis_n_s { get; set; }
        public int Zone { get; set; }
        //les relations
        public List<Permis> Les_Permis { get; set; }
        public List<Borne> Bornes { get; set; }
        public Carte Carte { get; set; }
        public Point_Pivot Point_Pivot { get; set; }
        public Commune Commune { get; set; }
    }
}
