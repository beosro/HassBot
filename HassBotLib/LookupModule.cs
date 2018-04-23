///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : LookupModule.cs
//  DESCRIPTION     : A class that implements ~lookup command
//                    It uses sitemap data to lookup
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using HassBotData;
using System;
using System.Linq;

namespace HassBotLib {
    public class LookupModule : BaseModule {
        private static readonly string ERROR_USAGE =
            "Usage: ~lookup <keyword> <@ optional user>";

        private static readonly string DEEPSEARCH_USAGE =
            "Usage: ~deepsearch <keyword>";

        [Command("lookup")]
        public async Task LookupAsync() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :thinking:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", ERROR_USAGE);
            await ReplyAsync("", false, embed);
        }

        [Command("lookup")]
        public async Task LookupAsync([Remainder]string input) {
            await LookupCommand(input);
        }

        private async Task LookupCommand(string input) {
            string result = LookupMagic(input);
            result = result.Trim();

            // mention users if any
            string mentionedUsers = base.MentionUsers();

            var embed = new EmbedBuilder();
            if (result == string.Empty) {
                embed.WithTitle(string.Format("Searched for '{0}': ", input));
                embed.WithColor(Helper.GetRandomColor());
                string msg = string.Format("You may try `~deepsearch {0}`.", input);
                embed.AddInlineField("Couldn't find it! :frowning:", msg);
            }
            else {
                embed.WithColor(Helper.GetRandomColor());
                embed.AddInlineField("Here is what I found: :smile:", mentionedUsers + result);
            }
            await ReplyAsync("", false, embed);
        }

        /// <summary>
        /// Looks up a given string
        /// </summary>
        /// <param name="searchString">either one or multiple words</param>
        /// <returns>URL</returns>
        /// <logic>
        /// It splits the input string into multiple words, parses sitemap URLs, retrieves location
        /// of each url, and compares the words - if there is a match, it returns
        /// </logic>
        private static string LookupMagic(string searchString) {
            string[] searchWords = null;
            StringBuilder sb = new StringBuilder();
            XmlDocument doc = Sitemap.SiteMapXmlDocument;

            searchString = searchString.Replace('.', ' ').Replace('_', ' ').Replace('-', ' ').ToLower();
            if (searchString.Contains(" "))
                searchWords = searchString.Split(' ');
            else
                searchWords = new string[] { searchString };

            if (null == searchWords)
                return string.Empty;

            Array.Sort(searchWords);

            foreach (XmlNode item in doc.DocumentElement.ChildNodes) {
                string location = string.Empty;
                string[] sitemapWords = null;

                string loc = item.FirstChild.InnerText;
                if (loc.EndsWith("/")) {
                    int index = loc.LastIndexOf("/", (loc.Length - 2));
                    location = loc.Substring((index) + 1, ((loc.Length - index) - 2));
                }
                else {
                    int index = loc.LastIndexOf("/", loc.Length);
                    location = loc.Substring((index) + 1, ((loc.Length - index) - 1));
                }

                location = location.Replace('.', ' ').Replace('_', ' ').Replace('-', ' ').ToLower();
                if (location.Contains(" "))
                    sitemapWords = location.Split(' ');
                else
                    sitemapWords = new string[] { location };

                if (null == sitemapWords)
                    continue;

                Array.Sort(sitemapWords);
                if (string.Join("", searchWords) == string.Join("", sitemapWords)) {
                    sb.Append(item.FirstChild.InnerText);
                    sb.Append("\n");
                }
            }

            return sb.ToString();
        }

        [Command("deepsearch")]
        public async Task DeepSearchAsync() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :thinking:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", DEEPSEARCH_USAGE);
            await ReplyAsync("", false, embed);
        }

        [Command("deepsearch")]
        public async Task DeepSearchAsync([Remainder]string input) {
            XmlDocument doc = Sitemap.SiteMapXmlDocument;

            StringBuilder sb = new StringBuilder();
            foreach (XmlNode item in doc.DocumentElement.ChildNodes) {
                if (item.InnerText.Contains(input.Split(' ')[0])) {
                    sb.Append("<" + item.FirstChild.InnerText + ">\n");
                }
            }

            string result = sb.ToString();

            if (result.Length > 1900) {
                result = result.Substring(0, 1850);
                result += "...\n\nThe message is truncated because it is too long. You may want to change the search criteria.";
            }

            // Send a Direct Message to the User with search information
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(result);
        }
    }
}