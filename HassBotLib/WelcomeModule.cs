///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : WelcomeModule.cs
//  DESCRIPTION     : A class tha implements ~welcome command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Text;
using System.Threading.Tasks;

namespace HassBotLib {
    public class WelcomeModule : BaseModule {
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
            return "welcome";
        }

        public override int GetCount() {
            return _counter;
        }

        [Command("welcome")]
        public async Task WelcomeAsync() {
            await WelcomeCommand();
        }

        [Command("welcome")]
        public async Task WelcomeAsync([Remainder]string cmd) {
            await WelcomeCommand();
        }

        private async Task WelcomeCommand() {
            Counter++;

            StringBuilder sb = new StringBuilder();
            sb.Append("Welcome to Home Asssistant Discord Channel. Please read <#331130181102206976> \n");
            sb.Append("For sharing code, please use https://www.hastebin.com\n");
            sb.Append("If it is less than 10 lines of code, **make sure** it is formatted using below format:\n\\`\\`\\`yaml\ncode\n\\`\\`\\`\n");

            // mention users if any
            await base.MentionUsers();

            var embed = new EmbedBuilder();
            embed.WithTitle("Welcome! :pray: ");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Welcome:", sb.ToString());
            await ReplyAsync("", false, embed);
        }
    }
}