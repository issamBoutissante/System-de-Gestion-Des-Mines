using System;
using System.Collections.Generic;
using System.Linq;

namespace Projet_Mines_Official
{
    class Remplir_la_base_de_donne
    {
        static ProjetMinesDBContext projetMinesDBContext=new ProjetMinesDBContext();
        public static void RemplirTypePermis()
        {
            List<Type_Permis> type_Permis = new List<Type_Permis>() {
                new Type_Permis(){Id=1,Type="PR"},
                new Type_Permis(){Id=2,Type="PRR"},
                new Type_Permis(){Id=3,Type="LE"},
                new Type_Permis(){Id=4,Type="LEE"}
            };
            if(projetMinesDBContext.Types_Permis.ToList().Count==0)
                projetMinesDBContext.Types_Permis.AddRange(type_Permis);

        }
        public static void RemplirEtat_Permis()
        {
            List<Etat_Permis> etat_Permis = new List<Etat_Permis>()
            {
                new Etat_Permis() { Id=1,Etat="Demmande"},
                new Etat_Permis() { Id=2,Etat="Decition"},
                new Etat_Permis() { Id=3,Etat="Demmande"},
                new Etat_Permis() { Id=4,Etat="Demmande"},
            };
            if(projetMinesDBContext.Etats_Permis.ToList().Count==0)
                projetMinesDBContext.Etats_Permis.AddRange(etat_Permis);
        }
        public static void RemplirRegion()
        {
            List<Region> regions = new List<Region>()
            {
                new Region(){Id=1,Nom_Region="Region1"},
                new Region(){Id=2,Nom_Region="Region2"},
                new Region(){Id=3,Nom_Region="Region3"},
                new Region(){Id=4,Nom_Region="Region4"},
            };
            if(projetMinesDBContext.Regions.ToList().Count==0)
                projetMinesDBContext.Regions.AddRange(regions);
        }
        public static void RemplirProvince()
        {
            List<Province> provinces = new List<Province>()
            {
                new Province(){Id=1,code_Province="1111",Nom_Province="Province1",Region=projetMinesDBContext.Regions.Find(1)},
                new Province(){Id=1,code_Province="1112",Nom_Province="Province2",Region=projetMinesDBContext.Regions.Find(1)},
                new Province(){Id=1,code_Province="1113",Nom_Province="Province3",Region=projetMinesDBContext.Regions.Find(1)},
                new Province(){Id=1,code_Province="1114",Nom_Province="Province4",Region=projetMinesDBContext.Regions.Find(1)},
            };
            if (projetMinesDBContext.Provinces.ToList().Count == 0)
                projetMinesDBContext.Provinces.AddRange(provinces);
        }
        public static void RemplirCaidat()
        {
            List<Caidat> caidats = new List<Caidat>()
            {
                new Caidat(){Id=1,Nom_Caidat="Caidat1",Province=projetMinesDBContext.Provinces.Find(1)},
                new Caidat(){Id=2,Nom_Caidat="Caidat2",Province=projetMinesDBContext.Provinces.Find(1)},
                new Caidat(){Id=3,Nom_Caidat="Caidat3",Province=projetMinesDBContext.Provinces.Find(1)},
                new Caidat(){Id=4,Nom_Caidat="Caidat4",Province=projetMinesDBContext.Provinces.Find(1)},
            };
            if (projetMinesDBContext.Caidats.ToList().Count == 0)
                projetMinesDBContext.Caidats.AddRange(caidats);
        }
        public static void RemplirCommune()
        {
            List<Commune> communes = new List<Commune>()
            {
                new Commune(){Id=1,Nom_Commune="commune1",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Id=2,Nom_Commune="commune2",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Id=3,Nom_Commune="commune3",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Id=4,Nom_Commune="commune4",Caidat=projetMinesDBContext.Caidats.Find(1)},
            };
            if (projetMinesDBContext.Communes.ToList().Count == 0)
                projetMinesDBContext.Communes.AddRange(communes);
        }
        public static void RemplirCarte()
        {
            List<Carte> cartes = new List<Carte>()
            {
                new Carte(){Id=1,Nom_carte="carte1"},
                new Carte(){Id=2,Nom_carte="carte2"},
                new Carte(){Id=3,Nom_carte="carte3"},
                new Carte(){Id=4,Nom_carte="carte4"},
            };
            if (projetMinesDBContext.Cartes.ToList().Count==0)
                projetMinesDBContext.Cartes.AddRange(cartes);
        }
        public static void RemplirPointPivot()
        {
            List<Point_Pivot> point_Pivots = new List<Point_Pivot>()
            {
                new Point_Pivot(){Id=1,Nom_Point_Pevot="point_pevot1"},
                new Point_Pivot(){Id=2,Nom_Point_Pevot="point_pevot2"},
                new Point_Pivot(){Id=3,Nom_Point_Pevot="point_pevot3"},
                new Point_Pivot(){Id=4,Nom_Point_Pevot="point_pevot4"},
            };
            if (projetMinesDBContext.Point_Pivots.ToList().Count == 0)
                projetMinesDBContext.Point_Pivots.AddRange(point_Pivots);
        }
        public static void RemplirPermis()
        {
            List<Permis> permis = new List<Permis>()
            {
                new Permis()
                {
                    Id=1,
                    Date_Depot=DateTime.Now.Date,
                    Etat_Permis=projetMinesDBContext.Etats_Permis.Find(1),
                    Type_Permis=projetMinesDBContext.Types_Permis.Find(1),
                    Titulaire=new Titulaire()
                    {
                        id=1,
                        Nom_Societe="Mon_societe"
                    }
                }
            };
            if (projetMinesDBContext.Les_Permis.ToList().Count == 0)
                projetMinesDBContext.Les_Permis.AddRange(permis);
        }
        public static void Remplir()
        {
            RemplirTypePermis();
            RemplirRegion();
            RemplirProvince();
            RemplirCaidat();
            RemplirCommune();
            RemplirCarte();
            RemplirPointPivot();
            RemplirEtat_Permis();
            RemplirPermis();
        }
    }
}
