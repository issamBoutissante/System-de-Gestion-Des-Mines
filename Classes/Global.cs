using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Mines_Official
{
    static class Global
    {
        public static ProjetMinesDBContext context = new ProjetMinesDBContext();
        public static Home Home;
        public static void InitializeHome(Home home)
        {
            Home = home;
        }
    }
}
