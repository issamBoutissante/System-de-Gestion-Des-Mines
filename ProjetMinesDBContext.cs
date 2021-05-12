using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Projet_Mines_Official
{
    public class ProjetMinesDBContext:DbContext
    {
        public DbSet<Permis> Les_Permis { get; set; }
        public DbSet<Titulaire> Titulaires { get; set; }
        public DbSet<Etat_Permis> Etats_Permis { get; set; }
        public DbSet<Type_Permis> Types_Permis { get; set; }
        public DbSet<Element_Dossier> Elements_Dossiers { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Borne> bornes { get; set; }
        public DbSet<Point_Pivot> Point_Pivots { get; set; }
        public DbSet<Carte> Cartes { get; set; }
        public DbSet<Commune> Communes{ get; set; }
        public DbSet<Caidat> Caidats { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Region> Regions { get; set; }
        //public DbSet<Observation> Observations { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
