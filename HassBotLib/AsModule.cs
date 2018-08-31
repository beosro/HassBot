///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 08/21/2018
//  FILE            : AsModule.cs
//  DESCRIPTION     : A class that implements ~as command
//                    Use this command to yell at someone "as" @hassbot :)
///////////////////////////////////////////////////////////////////////////////
using System.Threading.Tasks;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Linq;
using Discord;
using Discord.WebSocket;
using HassBotUtils;
using System.Reflection;

namespace HassBotLib {

    public class AsModule : BaseModule {
        private static readonly string ERROR_USAGE =
            "~as @user #channel <your message>";
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [Command("as")]
        public async Task AsAsync() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops!");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", ERROR_USAGE);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("as")]
        private async Task AsCommand([Remainder]string cmd) {
            if (!CheckModPermissions(Context.User)) {
                var embed = new EmbedBuilder();
                embed.WithTitle("Oops! :thumbsdown:");
                embed.WithColor(Color.DarkRed);
                embed.AddInlineField("Aha!", "You are not given the power to run this command!");
                await ReplyAsync("", false, embed);
                return;
            }

            // delete the original message
            await Context.Message.DeleteAsync();
            string mentionedChannels = base.MentionChannels();
            if (mentionedChannels != string.Empty) {
                cmd = cmd.Replace("<#" + mentionedChannels + ">", string.Empty);
                ulong id = ulong.Parse(mentionedChannels); ;
                var chnl = Context.Client.GetChannel(id) as ITextChannel;
                await chnl.SendMessageAsync(cmd, false, null);
                logger.Info(string.Format("From {0} in #{1} ==>: {2}", Context.User, chnl.Name, cmd));
            }
            else {
                logger.Info(string.Format("From {0} ==>: {1}", Context.User, cmd));
                await ReplyAsync(cmd, false, null);
            }
        }

        private bool CheckModPermissions(SocketUser user) {
            // get the list of mods from config file
            string mods = AppSettingsUtil.AppSettingsString("mods", true, string.Empty);

            // add special guest to the list 
            mods += ",zombu2";

            string[] moderators = mods.Split(',');
            var results = Array.FindAll(moderators, s => s.Trim().Equals(user.Username, StringComparison.OrdinalIgnoreCase));
            if (results.Length == 1)
                return true;
            else
                return false;
        }
    }
}