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

namespace HassBotData {

    public sealed class Sitemap {
        private static readonly Lazy<Sitemap> lazy = new Lazy<Sitemap>(() => new Sitemap());
        private static readonly XmlDocument doc = new XmlDocument();

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
    }
}