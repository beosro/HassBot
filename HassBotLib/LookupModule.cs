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
namespace HassBotLib {
    public class LookupModule : BaseModule {
        private static int _counter = 0;
        public static int Counter {
            get {
                return _counter;
            }
            set {
                _counter++;
            }
        }

        private static readonly string ERROR_USAGE =
            "Usage: ~lookup <keyword>";

        private static readonly string DEEPSEARCH_USAGE =
            "Usage: ~deepsearch <search string>";

        public override string GetName() {
            return "lookup";
        }

        public override int GetCount() {
            return _counter;
        }

        [Command("lookup")]
        public async Task LookupAsync() {
            Counter++;

            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :thinking:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", ERROR_USAGE);
            await ReplyAsync("", false, embed);
        }

        [Command("deepsearch")]
        public async Task DeepSearchAsync() {
            Counter++;

            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :thinking:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", DEEPSEARCH_USAGE);
            await ReplyAsync("", false, embed);
        }

        [Command("lookup")]
        public async Task LookupAsync(string input) {
            Counter++;

            XmlDocument doc = Sitemap.SiteMapXmlDocument;

            StringBuilder sb = new StringBuilder();
            string fomatted_input = "/" + input + "/";
            foreach (XmlNode item in doc.DocumentElement.ChildNodes) {
                if (item.InnerText.Contains(fomatted_input)) {
                    if (item.FirstChild.InnerText.EndsWith(fomatted_input)) {
                        sb.Append(item.FirstChild.InnerText);
                        sb.Append("\n");
                    }
                }
            }

            string result = sb.ToString();
            result = result.Trim();

            var embed = new EmbedBuilder();
            if (result == string.Empty) {
                embed.WithTitle(string.Format("Searched for '{0}': ", input));
                embed.WithColor(Helper.GetRandomColor());
                embed.AddInlineField("Couldn't find anything! :frowning:", "Try again with a different search string.");
            }
            else {
                embed.WithColor(Helper.GetRandomColor());
                embed.AddInlineField("Here is what I found: :smile:", sb.ToString());
            }
            await ReplyAsync("", false, embed);
        }

        [Command("deepsearch")]
        public async Task DeepSearchAsync(string input) {
            Counter++;

            XmlDocument doc = Sitemap.SiteMapXmlDocument;

            StringBuilder sb = new StringBuilder();
            foreach (XmlNode item in doc.DocumentElement.ChildNodes) {
                if (item.InnerText.Contains(input)) {
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