﻿///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : Helper.cs
//  DESCRIPTION     : A helper class
///////////////////////////////////////////////////////////////////////////////

using Discord;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using HassBotUtils;
using Discord.WebSocket;
using Discord.Commands;

namespace HassBotLib {
    public class Helper {

        private static readonly string YAML_START = @"```yaml";
        private static readonly string YAML_END   = @"```";
        private static readonly string JSON_START = @"```json";
        private static readonly string JSON_END = @"```";
        private static readonly string GOOD_EMOJI  = "✅";
        private static readonly string BAD_EMOJI   = "❌";

        private static readonly log4net.ILog logger =
                    log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static Color GetRandomColor() {
            Random rnd = new Random();
            Color[] colors = { Color.Blue, Color.DarkBlue, Color.DarkerGrey,
                               Color.DarkGreen, Color.DarkGrey, Color.DarkMagenta,
                               Color.DarkOrange, Color.DarkPurple, Color.DarkRed,
                               Color.DarkTeal, Color.Gold, Color.Green,
                               Color.LighterGrey, Color.LightGrey,
                               Color.LightOrange, Color.Magenta, Color.Orange,
                               Color.Purple, Color.Red, Color.Teal };

            int r = rnd.Next(colors.Count());
            return colors[r];
        }

        public static Task LogMessage(LogMessage message) {
            switch (message.Severity) {
                case LogSeverity.Critical:
                    logger.Fatal(message.Message);
                    break;
                case LogSeverity.Error:
                    logger.Error(message.Message);
                    break;
                case LogSeverity.Warning:
                    logger.Warn(message.Message);
                    break;
                case LogSeverity.Info:
                    logger.Info(message.Message);
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    logger.Debug(message.Message);
                    break;
            }
            return Task.FromResult(0);
        }

        public static async Task HandleHoundCIMessages(SocketUserMessage message, 
                                                       SocketCommandContext context, 
                                                       SocketGuildChannel channel) {

            if (null == channel || channel.Name != "github" || message.Embeds.Count <= 0)
                return;

            bool purgeHoundBotMsgs = AppSettingsUtil.AppSettingsBool("deleteHoundBotMsgs", 
                                                                     false, false);
            if (!purgeHoundBotMsgs)
                return;

            // #github channel contains messages from many different sources. 
            // check if the sender is 'houndci-bot' before deleting.
            foreach (Embed e in message.Embeds) {
                EmbedAuthor author = (EmbedAuthor) e.Author;
                if (author.ToString() == "houndci-bot") {
                    //logger.InfoFormat("Deleting the houndci-bot message: {0} => {1}: {2}",
                    //                   e.Url, e.Title, e.Description);
                    await context.Message.DeleteAsync();
                }
            }
        }

        public static async Task ReactToYaml(string content, SocketCommandContext context) {
            if (!(content.Contains(YAML_START) || content.Contains(YAML_END)))
                return;

            int start = content.IndexOf(YAML_START);
            int end = content.IndexOf(YAML_END, start + 3);

            if (start == -1 || end == -1 || end == start)
                return;

            string errMsg = string.Empty;
            string substring = content.Substring(start, (end - start));
            bool yamlCheck = ValidateHelper.ValidateYaml(substring, out errMsg);
            if (yamlCheck) {
                var okEmoji = new Emoji(GOOD_EMOJI);
                await context.Message.AddReactionAsync(okEmoji);
            }
            else {
                var errorEmoji = new Emoji(BAD_EMOJI);
                await context.Message.AddReactionAsync(errorEmoji);
            }
        }

        internal static async Task ReactToJson(string content, SocketCommandContext context) {
            if (!(content.Contains(JSON_START) || content.Contains(JSON_END)))
                return;

            int start = content.IndexOf(JSON_START);
            int end = content.IndexOf(JSON_END, start + 3);

            if (start == -1 || end == -1 || end == start)
                return;

            string errMsg = string.Empty;
            string substring = content.Substring(start, (end - start));
            bool jsonCheck = ValidateHelper.ValidateJson(substring, out errMsg);
            if (jsonCheck) {
                var okEmoji = new Emoji(GOOD_EMOJI);
                await context.Message.AddReactionAsync(okEmoji);
            }
            else {
                var errorEmoji = new Emoji(BAD_EMOJI);
                await context.Message.AddReactionAsync(errorEmoji);
            }
        }

        public static async Task ChangeNickName(DiscordSocketClient client, 
                                                SocketCommandContext context) {
            // Change Nick Name 💎
            // Get the Home Assistant Server Guild
            ulong serverGuild = (ulong)AppSettingsUtil.AppSettingsLong("serverGuild", true, 330944238910963714);
            var guild = client.GetGuild(serverGuild);
            if (null == guild)
                return;

            var user = guild.GetUser(context.User.Id);
            if (user.Nickname.Contains("🔹")) {
                await user.ModifyAsync(
                    x => {
                        string newNick = user.Nickname.Replace("🔹", string.Empty);
                        x.Nickname = newNick;
                    }
                );
            }
        }
    }
}