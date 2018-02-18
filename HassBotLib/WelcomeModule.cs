///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : WelcomeModule.cs
//  DESCRIPTION     : A class tha implements ~welcome command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HassBotUtils;
using System.Text;
using System.Threading.Tasks;

namespace HassBotLib {
    public class WelcomeModule : BaseModule {

        [Command("welcome")]
        public async Task WelcomeAsync() {
            await WelcomeCommand();
        }

        [Command("welcome")]
        public async Task WelcomeAsync([Remainder]string cmd) {
            await WelcomeCommand();
        }

        private async Task WelcomeCommand() {
            StringBuilder sb = new StringBuilder();

            string serverName = AppSettingsUtil.AppSettingsString("discordServerName", true, string.Empty); 
            string welcomerulesChannel = AppSettingsUtil.AppSettingsString("welcomerulesChannel", false, string.Empty);

            sb.Append(string.Format("Welcome to {0} Discord Channel! ", serverName));

            if (string.Empty != welcomerulesChannel) {
                sb.Append(string.Format("Please read {0} \n", welcomerulesChannel));
            }
            sb.Append("For sharing code, please use <https://www.hastebin.com>\n");
            sb.Append("If it is less than 10 lines of code, **make sure** it is formatted using below format:\n\\`\\`\\`yaml\ncode\n\\`\\`\\`\n");

            // mentioned users
            string mentionedUsers = base.MentionUsers();
            var embed = new EmbedBuilder();
            embed.WithTitle("Welcome! :pray: ");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Welcome:", mentionedUsers + sb.ToString());
            await ReplyAsync("", false, embed);
        }
    }
}