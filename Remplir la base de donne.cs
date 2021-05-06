using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Projet_Mines_Official
{
    class Remplir_la_base_de_donne
    {
        static ProjetMinesDBContext projetMinesDBContext=new ProjetMinesDBContext();
        public static void RemplirTypePermis()
        {
            List<Type_Permis> type_Permis = new List<Type_Permis>() {
                new Type_Permis(){Type="PR"},
                new Type_Permis(){Type="PRR"},
                new Type_Permis(){Type="LE"},
                new Type_Permis(){Type="LEE"}
            };
            if(projetMinesDBContext.Types_Permis.ToList().Count==0)
                projetMinesDBContext.Types_Permis.AddRange(type_Permis);
        }
        public static void RemplirEtat_Permis()
        {
            List<Etat_Permis> etat_Permis = new List<Etat_Permis>()
            {
                new Etat_Permis() { Etat="Demmande"},
                new Etat_Permis() { Etat="Decision"},
                new Etat_Permis() { Etat="Permis"},
            };
            if(projetMinesDBContext.Etats_Permis.ToList().Count==0)
                projetMinesDBContext.Etats_Permis.AddRange(etat_Permis);
        }
        public static void RemplirRegion()
        {
            List<Region> regions = new List<Region>()
            {
                new Region(){Nom_Region="Region1"},
                new Region(){Nom_Region="Region2"},
                new Region(){Nom_Region="Region3"},
                new Region(){Nom_Region="Region4"},
            };
            if(projetMinesDBContext.Regions.ToList().Count==0)
                projetMinesDBContext.Regions.AddRange(regions);
        }
        public static void RemplirProvince()
        {
            List<Province> provinces = new List<Province>()
            {
                new Province(){code_Province="1111",Nom_Province="Province1",Region=projetMinesDBContext.Regions.Find(1)},
                new Province(){code_Province="1112",Nom_Province="Province2",Region=projetMinesDBContext.Regions.Find(1)},
                new Province(){code_Province="1113",Nom_Province="Province3",Region=projetMinesDBContext.Regions.Find(1)},
                new Province(){code_Province="1114",Nom_Province="Province4",Region=projetMinesDBContext.Regions.Find(1)},
            };
            if (projetMinesDBContext.Provinces.ToList().Count == 0)
                projetMinesDBContext.Provinces.AddRange(provinces);
        }
        public static void RemplirCaidat()
        {
            List<Caidat> caidats = new List<Caidat>()
            {
                new Caidat(){Nom_Caidat="Caidat1",Province=projetMinesDBContext.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="Caidat2",Province=projetMinesDBContext.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="Caidat3",Province=projetMinesDBContext.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="Caidat4",Province=projetMinesDBContext.Provinces.Find(1)},
            };
            if (projetMinesDBContext.Caidats.ToList().Count == 0)
                projetMinesDBContext.Caidats.AddRange(caidats);
        }
        public static void RemplirCommune()
        {
            List<Commune> communes = new List<Commune>()
            {
                new Commune(){Nom_Commune="commune1",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Nom_Commune="commune2",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Nom_Commune="commune3",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Nom_Commune="commune4",Caidat=projetMinesDBContext.Caidats.Find(1)},
            };
            if (projetMinesDBContext.Communes.ToList().Count == 0)
                projetMinesDBContext.Communes.AddRange(communes);
        }
        public static void RemplirCarte()
        {
            List<Carte> cartes = new List<Carte>()
            {
                new Carte(){Nom_carte="carte1"},
                new Carte(){Nom_carte="carte2"},
                new Carte(){Nom_carte="carte3"},
                new Carte(){Nom_carte="carte4"},
            };
            if (projetMinesDBContext.Cartes.ToList().Count==0)
                projetMinesDBContext.Cartes.AddRange(cartes);
        }
        public static void RemplirPointPivot()
        {
            List<Point_Pivot> point_Pivots = new List<Point_Pivot>()
            {
                new Point_Pivot(){Nom_Point_Pevot="point_pevot1"},
                new Point_Pivot(){Nom_Point_Pevot="point_pevot2"},
                new Point_Pivot(){Nom_Point_Pevot="point_pevot3"},
                new Point_Pivot(){Nom_Point_Pevot="point_pevot4"},
            };
            if (projetMinesDBContext.Point_Pivots.ToList().Count == 0)
                projetMinesDBContext.Point_Pivots.AddRange(point_Pivots);
        }
            //Scafolding
        public static void RemplirElementDossierPR()
        {
            List<Element_Dossier> element_Dossiers = new List<Element_Dossier>()
            {
                new Element_Dossier(){isExist=false,nom_dossier=" - personnel de l'entreprise chargés de la conduite et du suivi des travaux ou éventuellement. Les contrats le liant aux personnes " +
                "physiques ou morales agréées visées à l'article 58 de la loi n°33-13."},
                new Element_Dossier(){isExist=false,nom_dossier=" -  Les moyens humains et techniques envisagés pour l'exécution des travaux ."},
                new Element_Dossier(){isExist=false,nom_dossier=" - Une fiche indiquant le statut de la personne morale et le capital social."},
                new Element_Dossier(){isExist=false,nom_dossier=" -  La liste et la valeur du matériel détenu par le demandeur, ou que celui-ci envisage d'acquérir et le financement correspondant."},
                new Element_Dossier(){isExist=false,nom_dossier=" - les garanties et cautions dont bénéficie l’entreprise, le cas éventuel ."},
                new Element_Dossier(){isExist=false,nom_dossier=" - Les pièces administratives délivrées par les autorités compétentes et justifiant que le demandeur est en règle au regard de ses obligations fiscales et cotisations sociales ."},
                new Element_Dossier(){isExist=false,nom_dossier=" - Programme de travaux provisoire indiquant la nature de l’importance des travaux programmés, les méthodes de recherche projetées ainsi le montant des dépenses prévues."},
                new Element_Dossier(){isExist=false,nom_dossier=" - L’original de la fiche du point pivot ."},
                new Element_Dossier(){isExist=false,nom_dossier=" - La définition de la position du centre du périmètre sollicité en coordonnées Lambert par rapport au point pivot ."},
                new Element_Dossier(){isExist=false,nom_dossier=" - 3 cartes régulières au 1/100 000 ou 1/50 000 sur lesquelles figure la position le périmètre demandé."},
                new Element_Dossier(){isExist=false,nom_dossier=" - L’original du récépissé du versement de la rémunération des services rendus au titre de l’institution du permis de recherche."},
                new Element_Dossier(){isExist=false,nom_dossier=" - Une pièce attestant de la qualité de mandataire de la personne morale au cas où la demande est formulée par un mandataire."},
            };
            if (projetMinesDBContext.Elements_Dossiers.ToList().Count == 0)
                projetMinesDBContext.Elements_Dossiers.AddRange(element_Dossiers);
        }
        public static void RemplirPermis()
        {
            List<Permis> permis = new List<Permis>()
            {
                new Permis()
                {
                    Date_Depot=DateTime.Now.Date,
                    Etat_Permis=projetMinesDBContext.Etats_Permis.Find(1),
                    Type_Permis=projetMinesDBContext.Types_Permis.Find(1),
                    Num_Permis=1234,
                    Titulaire=new Titulaire()
                    {
                        Nom_Societe="Mon_societe"
                    }
                },
                  new Permis()
                {
                    Date_Depot=DateTime.Now.Date,
                    Etat_Permis=projetMinesDBContext.Etats_Permis.Find(1),
                    Type_Permis=projetMinesDBContext.Types_Permis.Find(1),
                    Num_Permis=1235,
                    Titulaire=new Titulaire()
                    {
                        Nom_Societe="Une Autre Societe_societe"
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
            projetMinesDBContext.SaveChanges();
        }
    }
}
