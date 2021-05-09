using System.Collections.Generic;

namespace Projet_Mines_Official
{
    public class Titulaire
    {
        public Titulaire()
        {
            this.Lest_Permis = new HashSet<Permis>();
            //Associate Default Values
            this.Nom_Societe = "";
            this.Nom_Demandeur = "";
            this.status_Demandeur = "";
            this.Raison_Social = "";
            this.Numero_Cnss = "";
            this.Registre_Commerce = "";
            this.Taxe_Prof = "";
            this.Nom_Site = "";
            this.Election_Domicile = "";
            this.Effictif = "";
        }
        public int TitulaireId { get; set; }
        public string Nom_Societe { get; set; }
        public string Nom_Demandeur { get; set; }
        public string status_Demandeur { get; set; }
        public string Raison_Social { get; set; }
        public string Numero_Cnss { get; set; }
        public string Registre_Commerce { get; set; }
        public string Taxe_Prof{ get; set; }
        public string Nom_Site { get; set; }
        public string Election_Domicile { get; set; }
        public string Effictif { get; set; }
        public virtual ICollection<Permis> Lest_Permis { get; set; }
    }
}
