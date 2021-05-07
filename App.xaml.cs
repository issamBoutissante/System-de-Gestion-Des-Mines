using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;

namespace Projet_Mines_Official
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ProjetMinesDBContext>());
            base.OnStartup(e);
        }
    }
}
