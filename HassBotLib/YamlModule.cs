///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : YamlModule.cs
//  DESCRIPTION     : A class that implements ~yaml? command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

using HassBotUtils;
namespace HassBotLib {
    public class YamlModule : BaseModule {

        private static readonly string ERROR_USAGE =
            "That's not how it works. Try the following:\n~yaml? \\`\\`\\`yaml\ncode\n\\`\\`\\`";

        private static readonly string YAML_START = @"```yaml";
        private static readonly string YAML_END = @"```";

        private static int _counter = 0;
        public static int Counter {
            get {
                return _counter;
            }
            set {
                _counter++;
            }
        }
        public override string GetName() {
            return "yaml";
        }

        public override int GetCount() {
            return _counter;
        }

        [Command("yaml?")]
        public async Task YamlAsync() {
            Counter++;

            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops!");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", ERROR_USAGE);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("yaml?")]
        public async Task YamlAsync([Remainder]string cmd) {
            await YamlCommand(cmd);
        }

        private async Task YamlCommand(string cmd) {
            Counter++;

            int start = cmd.IndexOf(YAML_START);
            int end = cmd.IndexOf(YAML_END, start + 3);

            if (start == -1 || end == -1 || end == start)
                return;

            string errMsg = string.Empty;
            string substring = cmd.Substring(start, (end - start));

            string errorMessage = string.Empty;
            bool result = ValidateYaml.Validate(substring, out errorMessage);

            // mentioned users
            string mentionedUsers = base.MentionUsers();
            var embed = new EmbedBuilder();
            if (result == true) {
                embed.WithTitle(":thumbsup:");
                embed.WithColor(Helper.GetRandomColor());
                embed.AddField("yaml?", mentionedUsers + "Now, That's the perfectly valid YAML I'm talking about!");
            }
            else {
                embed.WithTitle(":thumbsdown:");
                embed.WithColor(Color.DarkRed);
                embed.AddField("yaml?", mentionedUsers + string.Format("Oops... Invalid YAML! Please check the code again!", mentionedUsers + errorMessage));
            }
            await ReplyAsync("", false, embed);
        }
    }
}