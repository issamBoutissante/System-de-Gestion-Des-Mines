using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Mines_Official
{
    // on va pas utilise ce class pour le moment
    public class Observation
    {
        public Observation()
        {
            //Associate Default Values
            this.Acte = "";
            this.Date_Acte = DateTime.Now.Date;
        }
        public int? ObservationId { get; set; }
        public string Acte { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date_Acte { get; set; }
    }
}
