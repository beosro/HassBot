///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : PersistStats.cs
//  DESCRIPTION     : Stats Persistence File
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HassBotUtils;
namespace HassBotData {
    public class PersistStats {

        private static Dictionary<string, int> _stats;
        private static string _statsFile = string.Empty;

        private PersistStats() {
            // private .ctor
        }

        static PersistStats() {
            _statsFile = AppSettingsUtil.AppSettingsString("statsFile", true, string.Empty);

            if (Persistence.FileExists(_statsFile)) {
                _stats = Persistence.LoadStats(_statsFile);
            }
            else {
                _stats = new Dictionary<string, int>();
                SaveStats();
            }
        }

        public static Dictionary<string, int> Commands {
            get {
                return _stats;
            }
        }

        public static void SaveStats() {
            Persistence.SaveStats(_stats, _statsFile);
        }
    }
}