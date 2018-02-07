///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : Helper.cs
//  DESCRIPTION     : A helper class
///////////////////////////////////////////////////////////////////////////////

using Discord;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HassBotLib {
    public class Helper {

        private static readonly log4net.ILog logger =
                    log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static Color GetRandomColor() {
            Random rnd = new Random();
            Color[] colors = { Color.Blue, Color.DarkBlue, Color.DarkerGrey,
                               Color.DarkGreen, Color.DarkGrey, Color.DarkMagenta,
                               Color.DarkOrange, Color.DarkPurple, Color.DarkRed, Color.DarkTeal,
                               Color.Gold, Color.Green, Color.LighterGrey, Color.LightGrey,
                               Color.LightOrange, Color.Magenta, Color.Orange,
                               Color.Purple, Color.Red, Color.Teal };

            int r = rnd.Next(colors.Count());
            return colors[r];
        }

        public static Task LogMessage(LogMessage message) {
            switch (message.Severity) {
                case LogSeverity.Critical:
                    logger.Fatal(message.Message);
                    break;
                case LogSeverity.Error:
                    logger.Error(message.Message);
                    break;
                case LogSeverity.Warning:
                    logger.Warn(message.Message);
                    break;
                case LogSeverity.Info:
                    logger.Info(message.Message);
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    logger.Debug(message.Message);
                    break;
            }
            return Task.FromResult(0);
        }

        public static Task PersistCounters() {
            Assembly thisAssembly = typeof(BaseModule).Assembly;
            foreach (var t in thisAssembly.GetTypes()) {
                if (t.IsAbstract || t != typeof (BaseModule))
                    continue;
                var instance = Activator.CreateInstance(t);
                BaseModule x = (BaseModule)instance;
                logger.Debug(string.Format("{0} is of count: '{1}'", t.AssemblyQualifiedName, ((BaseModule)instance).GetCount()));
            }

            return Task.FromResult(0);
        }
    }
}