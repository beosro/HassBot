///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/06/2018
//  FILE            : Program.cs
//  DESCRIPTION     : A command line class that tests the bot
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HassBotLib;
namespace TestClient {
    class Program {
        static void Main(string[] args) {
            // initialize the log4net
            log4net.Config.XmlConfigurator.Configure();

            // start the bot
            new HASSBot().StartBotAsync().GetAwaiter().GetResult();
        }
    }
}