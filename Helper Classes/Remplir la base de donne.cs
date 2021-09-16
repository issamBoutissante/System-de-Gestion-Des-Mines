using System.Collections.Generic;
using System.Linq;

namespace Projet_Mines_Official
{
    class Remplir_la_base_de_donne
    {
        public static void RemplirTypePermis()
        {
            List<Type_Permis> type_Permis = new List<Type_Permis>() {
                new Type_Permis(){Type="PR"},
                new Type_Permis(){Type="PRR"},
                new Type_Permis(){Type="LE"},
                new Type_Permis(){Type="LEE"}
            };
            if(Global.context.Types_Permis.Count()==0)
                Global.context.Types_Permis.AddRange(type_Permis);
        }
        public static void RemplirEtat_Permis()
        {
            List<Etat_Permis> etat_Permis = new List<Etat_Permis>()
            {
                new Etat_Permis() { Etat="Demmande"},
                new Etat_Permis() { Etat="Decision"},
                new Etat_Permis() { Etat="Permis"},
                new Etat_Permis() { Etat="Renouvelle"},
                new Etat_Permis() { Etat="EnExploitation"},
            };
            if(Global.context.Etats_Permis.Count()==0)
                Global.context.Etats_Permis.AddRange(etat_Permis);
        }
        public static void RemplirRegion()
        {
            List<Region> regions = new List<Region>()
            {
                new Region(){Nom_Region="Marrakech-Safi"},
                new Region(){Nom_Region="MTH"},
            };
            if(Global.context.Regions.Count()==0)
                Global.context.Regions.AddRange(regions);
        }
        public static void RemplirProvince()
        {
            List<Province> provinces = new List<Province>()
            {
                new Province(){Nom_Province="Marrakech",Region=Global.context.Regions.Single(r=>r.Nom_Region=="Marrakech-Safi")},
                new Province(){Nom_Province="Chichaoua",RegionId=1},
                new Province(){Nom_Province="Al Haouz"},
                new Province(){Nom_Province="El Kelaâ des Sraghna"},
                new Province(){Nom_Province="Essaouira"},
                new Province(){Nom_Province="Rehamna"},
                new Province(){Nom_Province="Safi"},
                new Province(){Nom_Province="Youssoufia"},
                
            };
            if (Global.context.Provinces.Count() == 0)
                Global.context.Provinces.AddRange(provinces);
        }
        public static void RemplirCaidat()
        {
            List<Caidat> caidats = new List<Caidat>()
            {
                new Caidat(){Nom_Caidat="Ighod",Province=Global.context.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="jnane Bouih",Province=Global.context.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="jdour",Province=Global.context.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="ahdil",Province=Global.context.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="nfifa-oued Lbour",Province=Global.context.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="Sidi Bou Othmane",Province=Global.context.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="Asif Lmal",Province=Global.context.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="Tazart",Province=Global.context.Provinces.Find(1)},
            };
            if (Global.context.Caidats.Count() == 0)
                Global.context.Caidats.AddRange(caidats);
        }
        public static void RemplirCommune()
        {
            List<Commune> communes = new List<Commune>()
            {
                new Commune(){Nom_Commune="Ighoud",Caidat=Global.context.Caidats.Find(1)},
                new Commune(){Nom_Commune="Jnane-Bouih",Caidat=Global.context.Caidats.Find(1)},
                new Commune(){Nom_Commune="Sidi-Ghanem",Caidat=Global.context.Caidats.Find(1)},
                new Commune(){Nom_Commune="Ahdil",Caidat=Global.context.Caidats.Find(1)},
                new Commune(){Nom_Commune="Ait Faska",Caidat=Global.context.Caidats.Find(1)},
                new Commune(){Nom_Commune="Lalla Aaziza",Caidat=Global.context.Caidats.Find(1)},
                new Commune(){Nom_Commune="Douirane",Caidat=Global.context.Caidats.Find(1)},
                new Commune(){Nom_Commune="Afalla Issen",Caidat=Global.context.Caidats.Find(1)},
                new Commune(){Nom_Commune="Siti Fadma",Caidat=Global.context.Caidats.Find(1)},
            };
            if (Global.context.Communes.Count() == 0)
                Global.context.Communes.AddRange(communes);
        }
        public static void RemplirCarte()
        {
            List<Carte> cartes = new List<Carte>()
            {
                new Carte(){Nom_carte="Chichaoua"},
                new Carte(){Nom_carte="Yousoufia"},
                new Carte(){Nom_carte="Demnat"},
                new Carte(){Nom_carte="Inim-N-Tanout"},
            };
            if (Global.context.Cartes.Count()==0)
                Global.context.Cartes.AddRange(cartes);
        }
        public static void RemplirPointPivot()
        {
            List<Point_Pivot> point_Pivots = new List<Point_Pivot>()
            {
                new Point_Pivot(){Nom_Point_Pevot="Zorg"},
                new Point_Pivot(){Nom_Point_Pevot="Cheikh Ben Allou"},
                new Point_Pivot(){Nom_Point_Pevot="J.Ighoud"},
                new Point_Pivot(){Nom_Point_Pevot="Sous"},
                new Point_Pivot(){Nom_Point_Pevot="Kt Si Ahmed"},
            };
            if (Global.context.Point_Pivots.Count() == 0)
                Global.context.Point_Pivots.AddRange(point_Pivots);
        }
            //Scafolding
        public static void RemplirElementDossierPR()
        {
            List<Element_Dossier> element_Dossiers = new List<Element_Dossier>()
            {
                new Element_Dossier(){nom_dossier=" - personnel de l'entreprise chargés de la conduite et du suivi des travaux ou éventuellement. Les contrats le liant aux personnes " +
                "physiques ou morales agréées visées à l'article 58 de la loi n°33-13."},
                new Element_Dossier(){nom_dossier=" -  Les moyens humains et techniques envisagés pour l'exécution des travaux ."},
                new Element_Dossier(){nom_dossier=" - Une fiche indiquant le statut de la personne morale et le capital social."},
                new Element_Dossier(){nom_dossier=" -  La liste et la valeur du matériel détenu par le demandeur, ou que celui-ci envisage d'acquérir et le financement correspondant."},
                new Element_Dossier(){nom_dossier=" - les garanties et cautions dont bénéficie l’entreprise, le cas éventuel ."},
                new Element_Dossier(){nom_dossier=" - Les pièces administratives délivrées par les autorités compétentes et justifiant que le demandeur est en règle au regard de ses obligations fiscales et cotisations sociales ."},
                new Element_Dossier(){nom_dossier=" - Programme de travaux provisoire indiquant la nature de l’importance des travaux programmés, les méthodes de recherche projetées ainsi le montant des dépenses prévues."},
                new Element_Dossier(){nom_dossier=" - L’original de la fiche du point pivot ."},
                new Element_Dossier(){nom_dossier=" - La définition de la position du centre du périmètre sollicité en coordonnées Lambert par rapport au point pivot ."},
                new Element_Dossier(){nom_dossier=" - 3 cartes régulières au 1/100 000 ou 1/50 000 sur lesquelles figure la position le périmètre demandé."},
                new Element_Dossier(){nom_dossier=" - L’original du récépissé du versement de la rémunération des services rendus au titre de l’institution du permis de recherche."},
                new Element_Dossier(){nom_dossier=" - Une pièce attestant de la qualité de mandataire de la personne morale au cas où la demande est formulée par un mandataire."},
            };
            if (Global.context.Elements_Dossiers.Count() == 0)
                Global.context.Elements_Dossiers.AddRange(element_Dossiers);
        }
        public static void RemplirPermis()
        {
            //permis.Permis_ElementDossiers = Global.context.Elements_Dossiers.Where(ed => ed.Type_PermisId == 1).ToList();
            if (Global.context.Les_Permis.Count() == 0)
            {
                for(int i = 0; i <= 10; i++)
                {
                    Permis newPermis = new Permis(new Area(), new Titulaire());
                    Global.context.Les_Permis.Add(newPermis);
                    Global.context.SaveChanges();
                }
                Global.context.Les_Permis.ToList().ForEach(p =>
                {
                    InitilializerLesDossierPermis.InitilizerDossiers(p, TypePermis.PR);
                });
            }
        }
        public static void RemplirLogin()
        {
            List<Utilisateur> utilisateurs = new List<Utilisateur>()
            {
                new Utilisateur(){NomUtilisateur="chaimae",MotPass="chaimae"},
                new Utilisateur(){NomUtilisateur="issam",MotPass="issam"},
                new Utilisateur(){NomUtilisateur="aoujil",MotPass="aoujil"},
                new Utilisateur(){NomUtilisateur="aginane",MotPass="aginane"},
            };
            if (Global.context.Utilisateurs.Count() == 0)
                Global.context.Utilisateurs.AddRange(utilisateurs);
        }
        public static void Remplir()
        {
            RemplirTypePermis();
            Global.context.SaveChanges();

            RemplirRegion();
            Global.context.SaveChanges();

            RemplirProvince();
            Global.context.SaveChanges();

            RemplirCaidat();
            Global.context.SaveChanges();

            RemplirCommune();
            Global.context.SaveChanges();

            RemplirCarte();
            Global.context.SaveChanges();

            RemplirPointPivot();
            Global.context.SaveChanges();

            RemplirEtat_Permis();
            Global.context.SaveChanges();

            RemplirElementDossierPR();
            Global.context.SaveChanges();
            
            RemplirPermis();
            Global.context.SaveChanges();

            RemplirLogin();
            Global.context.SaveChanges();
        }
    }
}
