///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : Helper.cs
//  DESCRIPTION     : A helper class 
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Reflection;

using HassBotUtils;
namespace HassBotData {
    public class Helper {
        private static readonly string ERR_DOWNLOADING =
            "Error downloading Home Assistant sitemap file.";

        private static readonly string SITEMAP_UPDATED =
            "Updated sitemap file successfully.";

        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private Helper() {

        }

        public static void DownloadSiteMap() {
            using (var client = new WebClient()) {
                string sitemapUrl = AppSettingsUtil.AppSettingsString("sitemapUrl", true, string.Empty);
                string sitemapPath = AppSettingsUtil.AppSettingsString("sitemapPath", true, string.Empty);
                try {
                    client.DownloadFile(sitemapUrl, sitemapPath);
                    logger.Info(SITEMAP_UPDATED);
                }
                catch (Exception e) {
                    logger.Error(ERR_DOWNLOADING, e);
                }
            }
        }
    }
}
