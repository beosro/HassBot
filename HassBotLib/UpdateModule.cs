///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : UpdateModule.cs
//  DESCRIPTION     : A class that implements ~update command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;
using HassBotData;

namespace HassBotLib {
    public class UpdateModule : BaseModule {
        private static int _counter = 0;
        public static int RefreshCounter {
            get {
                return _counter;
            }
            set {
                _counter++;
            }
        }

        private static readonly string UPDATE_SUCCESSFUL = 
            "Refreshed lookup data successfully!";

        private static readonly string UPDATE_FAILED =
            "Failed to refresh lookup data! contact @skalavala";

        public override string GetName() {
            return "update";
        }

        public override int GetCount() {
            return _counter;
        }

        [Command("update")]
        public async Task UpdateAsync() {
            RefreshCounter++;

            var embed = new EmbedBuilder();
            try {
                embed.WithColor(Helper.GetRandomColor());
                embed.AddInlineField(":thumbsup:", UPDATE_SUCCESSFUL);
                Sitemap.ReloadData();
            }
            catch {
                embed.WithColor(Color.Red);
                embed.AddInlineField(":cold_sweat:", UPDATE_FAILED);
            }

            await ReplyAsync(string.Empty, false, embed);
        }
    }
}