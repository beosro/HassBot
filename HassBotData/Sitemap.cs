///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : Sitemap.cs
//  DESCRIPTION     : A static Sitemap Singleton class that loads static data
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Configuration;
using System.Xml;
using HassBotUtils;
using System.Text;
using System.Reflection;

namespace HassBotData {

    public sealed class Sitemap {
        private static readonly Lazy<Sitemap> lazy = new Lazy<Sitemap>(() => new Sitemap());
        private static readonly XmlDocument doc = new XmlDocument();
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private Sitemap() {
            // private .ctor
        }

        static Sitemap() {
            string siteMap = AppSettingsUtil.AppSettingsString("sitemapPath", true, string.Empty);
            if (System.IO.File.Exists(siteMap)) {
                doc.Load(siteMap);
            }
            else {
                ReloadData();
            }
        }

        public static void ReloadData() {
            string siteMap = AppSettingsUtil.AppSettingsString("sitemapPath", true, string.Empty);
            Helper.DownloadSiteMap();
            doc.Load(siteMap);
            logger.Info("Sitemap data refreshed successfully!");
        }

        public static Sitemap Instance {
            get {
                return lazy.Value;
            }
        }

        public static XmlDocument SiteMapXmlDocument {
            get {
                return doc;
            }
        }

        public static string Lookup (string input) {
            ArgumentValidation.CheckForEmptyString("Missing lookup 'search' string.", "input");
            XmlDocument doc = Sitemap.SiteMapXmlDocument;

            StringBuilder sb = new StringBuilder();
            string fomatted_input = "/" + input + "/";
            foreach (XmlNode item in doc.DocumentElement.ChildNodes) {
                if (item.InnerText.Contains(fomatted_input)) {
                    if (item.FirstChild.InnerText.EndsWith(fomatted_input)) {
                        sb.Append("<" + item.FirstChild.InnerText + ">");
                        sb.Append("\n");
                    }
                }
            }
            string result = sb.ToString();
            result = result.Trim();
            return result;
        }
    }
}