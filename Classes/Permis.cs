using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Mines_Official
{
    public class Permis
    {
        public Permis()
        {
            this.Chevauchements = new HashSet<Permis>();
            this.Licence_Permis = new HashSet<Permis>();
            this.Les_Element_Dossier = new HashSet<Element_Dossier>();
            //this.Observations = new HashSet<Observation>();
            //Associate Default Values
            this.Num_Demmande = 0;
            this.Num_Permis = 0;
            this.Date_Depot = DateTime.Now.Date;
            this.Investisement_Projet = 0;
            this.Occupation_Temporaire = "";
            this.Date_Institition = DateTime.Now.Date;
            this.Echeance = DateTime.Now.Date;
            this.Investisement_Realise = 0;
            this.Inscription_Conservation = false;
            this.Date_Depart_CRI = DateTime.Now.Date;
            this.Date_Retour_CRI = DateTime.Now.Date;
            this.Date_Decision = DateTime.Now.Date;
            this.Date_Enquete = DateTime.Now.Date;
            this.Date_Rapot = DateTime.Now.Date;
            this.Etat_PermisId = 1;
            this.Type_PermisId = 1;
            Area = new Area();
            Titulaire = new Titulaire();
        }
        public int PermisId { get; set; }
        public int? Num_Demmande { get; set; }
        public int? Num_Permis{ get; set; }
        [Column(TypeName="Date")]
        public DateTime Date_Depot { get; set; }
        public double Investisement_Projet { get; set; }
        public string Occupation_Temporaire { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date_Institition { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Echeance { get; set; }
        public double? Investisement_Realise { get; set; }
        public bool Inscription_Conservation { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date_Depart_CRI { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date_Retour_CRI { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date_Decision { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date_Enquete { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date_Rapot { get; set; }
        //les relation avec lui meme

        public virtual ICollection<Permis> Chevauchements { get; set; }

        public int? Ex_PermisId { get; set; }
        [ForeignKey("Ex_PermisId")]
        public virtual Permis Ex_Permis { get; set; }

        public virtual ICollection<Permis> Licence_Permis { get; set; }
        //les relation avec autres classes

        public int? Etat_PermisId { get; set; }
        [ForeignKey("Etat_PermisId")]
        public virtual Etat_Permis Etat_Permis { get; set; }

        public int? Type_PermisId { get; set; }
        [ForeignKey("Type_PermisId")]
        public virtual Type_Permis Type_Permis { get; set; }
        
        public int? AreaId { get; set; }
        [ForeignKey("AreaId")]
        public virtual Area Area { get; set; }

        public int? TitulaireId { get; set; }
        [ForeignKey("TitulaireId")]
        public virtual Titulaire Titulaire { get; set; }

        public virtual ICollection<Element_Dossier> Les_Element_Dossier { get; set; }
        //nous avons dit qu'on va le remplace par Historique
        //public virtual ICollection<Observation> Observations { get; set; }
    }
}