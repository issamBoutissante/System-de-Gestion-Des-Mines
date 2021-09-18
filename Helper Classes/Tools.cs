using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Mines_Official.Helper_Classes
{
    class Tools
    {
        internal static void CopyArea(Area exArea,Area newArea)
        {
            newArea.Abscisse = exArea.Abscisse;
            newArea.Ordonnee = exArea.Ordonnee;
            newArea.Dir_Est_ouest = exArea.Dir_Est_ouest;
            newArea.Dir_nord_sud = exArea.Dir_nord_sud;
            newArea.Dis_e_o = exArea.Dis_e_o;
            newArea.Dis_n_s = exArea.Dis_n_s;
            newArea.Zone = exArea.Zone;
            //newArea.les = exArea.Dis_e_o;

        }
    }
}
