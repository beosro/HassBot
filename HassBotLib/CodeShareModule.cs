///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 04/21/2018
//  FILE            : CodeShareModule.cs
//  DESCRIPTION     : A class that implements ~codeshare command
///////////////////////////////////////////////////////////////////////////////
using System.Threading.Tasks;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Linq;
using Discord;

namespace HassBotLib {

    public class CodeShareModule : BaseModule {
        private static readonly string CODESHARE_URL = "https://codeshare.io/new";
        private static readonly string CODESHARE_MESSAGE =
            "Click on the link {0} and paste your code there. It makes it easy to collaborate and make changes in real-time.";

        [Command("codeshare")]
        public async Task CodeShareAsync() {
            await CodeShareCommand();
        }

        [Command("codeshare")]
        public async Task CodeShareAsync([Remainder]string cmd) {
            await CodeShareCommand();
        }

        private async Task CodeShareCommand() {
            var embed = new EmbedBuilder();
            string codeshareUrl = HassBotUtils.Utils.GetCodeShareURL(CODESHARE_URL);
            string message = string.Format(CODESHARE_MESSAGE, codeshareUrl);

            // mention users if any
            string mentionedUsers = base.MentionUsers();
            if (string.Empty != mentionedUsers)
                message = mentionedUsers + " " + message;

            await ReplyAsync(message, false, null);
        }
    }
}