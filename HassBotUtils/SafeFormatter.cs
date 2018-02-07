/////////////////////////////////////////////////////////////////////////////////
//  DATE            : 02/06/2018
//  FILE            : SafeFormatter.cs
//  DESCRIPTION     : An utility class that safely formats strings
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HassBotUtils {
    public abstract class SafeFormatter {

        protected SafeFormatter() {
            // .ctor
        }

        public static string Format(string format, params Object[] args) {
            ArgumentValidation.CheckForEmptyString(format, "format");
            try {
                return string.Format(format, args);
            }
            catch {
                return format;
            }
        }

        public static string FormatNotify(string format, params Object[] args) {
            ArgumentValidation.CheckForEmptyString(format, "format");
            try {
                return string.Format(format, args);
            }
            catch (Exception e) {
                return format + " [" + e.Message + "]";
            }
        }
    }
}