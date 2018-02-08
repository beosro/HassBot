///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/04/2018
//  FILE            : Utils.cs
//  DESCRIPTION     : A Util class with bunch of utility methods
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Reflection;

namespace HassBotUtils
{
    public sealed class Utils
    {
        private static readonly log4net.ILog logger =
                    log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static bool LineCountCheck(string message) {
            if (string.Empty == message)
                return true;

            int maxLinesLimit = AppSettingsUtil.AppSettingsInt("maxLinesLimit", false, 15);
            bool yamlHeader = false;
            string YAML_START = @"```yaml";
            string YAML_START_1 = @"```";
            string YAML_END = @"```";

            int start = message.IndexOf(YAML_START);
            if ( -1 == start)
                start = message.IndexOf(YAML_START_1);

            int end = message.IndexOf(YAML_END, start + 3);

            if (start == -1 || end == -1 || end == start)
                yamlHeader = false;
            else
                yamlHeader = true;

            if (yamlHeader)
                maxLinesLimit = maxLinesLimit +2;

            if (message.Split('\n').Length > maxLinesLimit) {
                return false;
            }
            return true;
        }
    }
}