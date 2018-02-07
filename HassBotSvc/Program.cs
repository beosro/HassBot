///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/06/2018
//  FILE            : Program.cs
//  DESCRIPTION     : Service Loader. Loads all the services in the assembly
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HassBotSvc {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main() {

            // Setup Log4net
            log4net.Config.XmlConfigurator.Configure();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new HassBotSvc()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
