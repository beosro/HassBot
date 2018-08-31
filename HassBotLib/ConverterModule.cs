///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : AboutModule.cs
//  DESCRIPTION     : A class that implements ~yaml2json & ~json2yaml commands
///////////////////////////////////////////////////////////////////////////////
using System.Threading.Tasks;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Linq;
using Discord;

namespace HassBotLib {
    public class ConverterModule : BaseModule {

        private static readonly string ERROR_USAGE_YAML2JSON = 
            "Usage: ~yaml2json <yaml code>";
        private static readonly string ERROR_USAGE_JSON2YAML =
            "Usage: ~json2yaml <json code>";

        public ConverterModule() {

        }

        [Command("yaml2json")]
        public async Task Yaml2Json() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :frowning: ");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", ERROR_USAGE_YAML2JSON);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("yaml2json")]
        public async Task Yaml2Json([Remainder]string cmd) {
            string json = HassBotUtils.Utils.Yaml2Json(cmd);
            await ReplyAsync("```json\n" + json + "\n```\n");
        }

        [Command("json2yaml")]
        public async Task Json2Yaml() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :frowning: ");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", ERROR_USAGE_JSON2YAML);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("json2yaml")]
        public async Task Json2Yaml([Remainder]string cmd) {
            string yaml = HassBotUtils.Utils.Json2Yaml(cmd);
            await ReplyAsync("```yaml\n" + yaml + "\n```\n");
        }
    }
}