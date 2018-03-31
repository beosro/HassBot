using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace HassBotLib {
    public class Base64Module : BaseModule {

        private static readonly string ERROR_USAGE =
            "Try ~base64_encode <string to encode> or ~base64_decode <string to decode>";

        [Command("base64_encode")]
        public async Task Base64EncodeAsync() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :rosette:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", ERROR_USAGE);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("base64_decode")]
        public async Task Base64DecodeAsync() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :rosette:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", ERROR_USAGE);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("base64_encode")]
        public async Task Base64EncodeAsync([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":rosette:");
            embed.WithColor(Helper.GetRandomColor());
            string data = HassBotUtils.Utils.Base64Encode(cmd);
            embed.AddField("Base64 Encoded Value:", data);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("base64_decode")]
        public async Task Base64DecodeAsync([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":rosette:");
            embed.WithColor(Helper.GetRandomColor());
            string data = HassBotUtils.Utils.Base64Decode(cmd);
            embed.AddField("Base64 Decoded Value:", data);

            await ReplyAsync(string.Empty, false, embed);
        }
    }
}
