using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Mines_Official
{
    public class Observation
    {
        public int Id { get; set; }
        public string Acte { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date_Acte { get; set; }
    }
}
