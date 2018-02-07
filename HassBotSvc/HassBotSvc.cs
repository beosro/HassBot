///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/06/2018
//  FILE            : HassBotSvc.cs
//  DESCRIPTION     : HassBot Service File
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using HassBotLib;
using System.Reflection;

namespace HassBotSvc {
    public partial class HassBotSvc : ServiceBase {

        private HASSBot _bot = new HASSBot();
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public HassBotSvc() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            try {
                // start the bot
                _bot.Start();
            }
            catch (Exception e) {
                logger.Error("Error starting Home Assistant Bot.", e);
            }

            // Can't run the following way - The Service Control Manager (SCM) thinks the 
            // bot is not responding. Also, the SCM can't start, stop, and restart.
            // new HASSBot().StartBotAsync().GetAwaiter().GetResult();
        }

        protected override void OnStop() {
            _bot.Stop();
        }
    }
}