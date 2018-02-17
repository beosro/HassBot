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

        private static int _counter = 0;
        public static int Counter {
            get {
                return _counter;
            }
            set {
                _counter++;
            }
        }

        public override int GetCount() {
            return Counter;
        }

        public override string GetName() {
            return "lmgtfy";
        }

        [Command("lmgtfy")]
        public async Task LetMeGoogleThatForYou([Remainder]string cmd) {
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