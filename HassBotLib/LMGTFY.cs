///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : HassBot.cs
//  DESCRIPTION     : A class that implements ~lmgtfy command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System.Web;

namespace HassBotLib {

    public class LMGTFY : BaseModule {

        private static readonly string ERROR_USAGE =
            "Usage: ~lmgtfy <google search string>";

        [Command("lmgtfy")]
        public async Task LetMeGoogleThatForYouAsync() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :sob:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", ERROR_USAGE);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("lmgtfy")]
        public async Task LetMeGoogleThatForYouAsync([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":point_up:");
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = base.MentionUsers();
            if (string.Empty != mentionedUsers) {
                foreach (string user in mentionedUsers.Split(' '))
                    if (string.Empty != user) {
                        string userHandle = user.Replace("!", string.Empty);
                        cmd = cmd.Replace(userHandle.Trim(), string.Empty);
                    }
            }

            string encoded = HttpUtility.UrlEncode(cmd.Trim());
            embed.AddInlineField("Let me Google that for you...",
                string.Format("Here, try this {0} => <http://lmgtfy.com/?q={1}>", mentionedUsers, encoded));
            await ReplyAsync("", false, embed);
        }
    }
}