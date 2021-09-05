using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projet_Mines_Official
{
    class PermisState
    {
        public static void updateEtat(Permis permis, int etatPermis)
        {
            switch (permis.Etat_PermisId)
            {
                case EtatPermis.Demmande:
                case EtatPermis.Decision:
                case EtatPermis.Permis:
                    permis.Etat_PermisId = etatPermis;
                    break;
                case EtatPermis.Renouvelle:
                    if (etatPermis == EtatPermis.EnExploitation)
                    {
                        permis.Etat_PermisId = etatPermis;
                    }
                    break;
            }
            MessageBox.Show(permis.Etat_PermisId.ToString());
        }
    }
}
