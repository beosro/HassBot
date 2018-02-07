///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : FormatModule.cs
//  DESCRIPTION     : A class that implements ~format command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Text;
using System.Threading.Tasks;

namespace HassBotLib {
    public class FormatModule : BaseModule {
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
            return "format";
        }

        public override int GetCount() {
            return _counter;
        }

        [Command("format")]
        public async Task FormatAsync() {
            Counter++;

            StringBuilder sb = new StringBuilder();
            sb.Append("To format your text as code, enter three backticks on the first line, press Shift+Enter for a new line, paste your code, press Shift + Enter again for another new line, and lastly three more backticks. Here's an example:\n");
            sb.Append("\\`\\`\\`yaml\n");
            sb.Append("code here\n");
            sb.Append("\\`\\`\\`\n");

            var embed = new EmbedBuilder();
            embed.WithTitle(":information_source:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Format Code:", sb.ToString());
            await ReplyAsync("", false, embed);
        }
    }
}