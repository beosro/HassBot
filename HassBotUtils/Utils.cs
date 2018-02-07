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
            int maxLinesLimit = AppSettingsUtil.AppSettingsInt("maxLinesLimit", false, 15);
            if (message.Split('\n').Length >= maxLinesLimit) {
                return false;
            }
            return true;
        }
    }
}