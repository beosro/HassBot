﻿///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : Share.cs
//  DESCRIPTION     : A class that implements ~share command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace HassBotLib {
    public class ShareModule : BaseModule {

        [Command("share")]
        public async Task ShareAsync() {
            await ShareCommand();
        }

        [Command("share")]
        public async Task ShareAsync([Remainder]string cmd) {
            await ShareCommand();
        }

        private async Task ShareCommand() {
            string s = "Please use https://www.hastebin.com/ or https://paste.ubuntu.com/ to share code.";

            // mentioned users
            string mentionedUsers = base.MentionUsers();
            var embed = new EmbedBuilder();
            embed.WithTitle(":point_down: ");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Format Code:", mentionedUsers + s);

            await ReplyAsync("", false, embed);
        }
    }
}