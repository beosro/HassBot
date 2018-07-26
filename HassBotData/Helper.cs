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
            try {
                string sitemapUrl = AppSettingsUtil.AppSettingsString("sitemapUrl", true, string.Empty);
                string sitemapPath = AppSettingsUtil.AppSettingsString("sitemapPath", true, string.Empty);

                // The Home Assistant web site has stopped support for TLS 1.0 - which is used bydefault.
                // Let's force it to use TLS 1.2 - otherwise it will throw the following error:
                // The underlying connection was closed: An unexpected error occurred on a send.
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                WebClient wc = new WebClient();

                wc.DownloadFile(new Uri(sitemapUrl), sitemapPath);
                logger.Info(SITEMAP_UPDATED);
            }
            catch (Exception e) {
                logger.Error(ERR_DOWNLOADING, e);
            }
        }
    }
}