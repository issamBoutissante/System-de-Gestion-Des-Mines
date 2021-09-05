﻿using System.Collections.Generic;
using System.Linq;

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
            if(projetMinesDBContext.Types_Permis.Count()==0)
                projetMinesDBContext.Types_Permis.AddRange(type_Permis);
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
            if(projetMinesDBContext.Etats_Permis.Count()==0)
                projetMinesDBContext.Etats_Permis.AddRange(etat_Permis);
        }
        public static void RemplirRegion()
        {
            List<Region> regions = new List<Region>()
            {
                new Region(){Nom_Region="Marrakech-Safi"},
                new Region(){Nom_Region="MTH"},
            };
            if(projetMinesDBContext.Regions.Count()==0)
                projetMinesDBContext.Regions.AddRange(regions);
        }
        public static void RemplirProvince()
        {
            List<Province> provinces = new List<Province>()
            {
                new Province(){Nom_Province="Marrakech",Region=projetMinesDBContext.Regions.Single(r=>r.Nom_Region=="Marrakech-Safi")},
                new Province(){Nom_Province="Chichaoua",RegionId=1},
                new Province(){Nom_Province="Al Haouz"},
                new Province(){Nom_Province="El Kelaâ des Sraghna"},
                new Province(){Nom_Province="Essaouira"},
                new Province(){Nom_Province="Rehamna"},
                new Province(){Nom_Province="Safi"},
                new Province(){Nom_Province="Youssoufia"},
                
            };
            if (projetMinesDBContext.Provinces.Count() == 0)
                projetMinesDBContext.Provinces.AddRange(provinces);
        }
        public static void RemplirCaidat()
        {
            List<Caidat> caidats = new List<Caidat>()
            {
                new Caidat(){Nom_Caidat="Ighod",Province=projetMinesDBContext.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="jnane Bouih",Province=projetMinesDBContext.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="jdour",Province=projetMinesDBContext.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="ahdil",Province=projetMinesDBContext.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="nfifa-oued Lbour",Province=projetMinesDBContext.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="Sidi Bou Othmane",Province=projetMinesDBContext.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="Asif Lmal",Province=projetMinesDBContext.Provinces.Find(1)},
                new Caidat(){Nom_Caidat="Tazart",Province=projetMinesDBContext.Provinces.Find(1)},
            };
            if (projetMinesDBContext.Caidats.Count() == 0)
                projetMinesDBContext.Caidats.AddRange(caidats);
        }
        public static void RemplirCommune()
        {
            List<Commune> communes = new List<Commune>()
            {
                new Commune(){Nom_Commune="Ighoud",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Nom_Commune="Jnane-Bouih",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Nom_Commune="Sidi-Ghanem",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Nom_Commune="Ahdil",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Nom_Commune="Ait Faska",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Nom_Commune="Lalla Aaziza",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Nom_Commune="Douirane",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Nom_Commune="Afalla Issen",Caidat=projetMinesDBContext.Caidats.Find(1)},
                new Commune(){Nom_Commune="Siti Fadma",Caidat=projetMinesDBContext.Caidats.Find(1)},
            };
            if (projetMinesDBContext.Communes.Count() == 0)
                projetMinesDBContext.Communes.AddRange(communes);
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
            if (projetMinesDBContext.Cartes.Count()==0)
                projetMinesDBContext.Cartes.AddRange(cartes);
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
            if (projetMinesDBContext.Point_Pivots.Count() == 0)
                projetMinesDBContext.Point_Pivots.AddRange(point_Pivots);
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
            if (projetMinesDBContext.Elements_Dossiers.Count() == 0)
                projetMinesDBContext.Elements_Dossiers.AddRange(element_Dossiers);
        }
        public static void RemplirPermis()
        {
            //permis.Permis_ElementDossiers = projetMinesDBContext.Elements_Dossiers.Where(ed => ed.Type_PermisId == 1).ToList();
            if (projetMinesDBContext.Les_Permis.Count() == 0)
            {
                for(int i = 0; i <= 10; i++)
                {
                    Permis newPermis = new Permis(new Area(), new Titulaire());
                    projetMinesDBContext.Les_Permis.Add(newPermis);
                    projetMinesDBContext.SaveChanges();
                }
                projetMinesDBContext.Les_Permis.ToList().ForEach(p =>
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
            if (projetMinesDBContext.Utilisateurs.Count() == 0)
                projetMinesDBContext.Utilisateurs.AddRange(utilisateurs);
        }
        public static void Remplir()
        {
            RemplirTypePermis();
            projetMinesDBContext.SaveChanges();

            RemplirRegion();
            projetMinesDBContext.SaveChanges();

            RemplirProvince();
            projetMinesDBContext.SaveChanges();

            RemplirCaidat();
            projetMinesDBContext.SaveChanges();

            RemplirCommune();
            projetMinesDBContext.SaveChanges();

            RemplirCarte();
            projetMinesDBContext.SaveChanges();

            RemplirPointPivot();
            projetMinesDBContext.SaveChanges();

            RemplirEtat_Permis();
            projetMinesDBContext.SaveChanges();

            RemplirElementDossierPR();
            projetMinesDBContext.SaveChanges();
            
            RemplirPermis();
            projetMinesDBContext.SaveChanges();

            RemplirLogin();
            projetMinesDBContext.SaveChanges();
        }
    }
}
