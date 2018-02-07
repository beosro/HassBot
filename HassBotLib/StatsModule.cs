///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : StatsModule.cs
//  DESCRIPTION     : A class that implements ~stats command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HassBotLib {
    public class StatsModule : BaseModule {
        private static int _counter = 0;
        public static int Counter {
            get {
                return _counter;
            }
            set {
                _counter++;
            }
        }
        public override string GetName() {
            return "stats";
        }

        public override int GetCount() {
            return _counter;
        }

        [Command("stats")]
        public async Task StatsAsync() {
            await StatsCommand();
        }

        [Command("stats")]
        public async Task StatsAsync([Remainder]string cmd) {
            await StatsCommand();
        }

        private async Task StatsCommand() {
            Counter++;

            await Helper.PersistCounters();

            var embed = new EmbedBuilder();
            embed.WithTitle("Stats of the @HassBot:");
            embed.WithColor(Helper.GetRandomColor());
            embed.WithDescription("Number of times each command is run:");
            embed.AddInlineField("Custom Commands", CommandModule.Counter.ToString());
            embed.AddInlineField("Format", FormatModule.Counter.ToString());
            embed.AddInlineField("Help", HelpModule.Counter.ToString());
            embed.AddInlineField("About", AboutModule.Counter.ToString());
            embed.AddInlineField("Lookup", LookupModule.Counter.ToString());
            embed.AddInlineField("Ping", PingModule.Counter.ToString());
            embed.AddInlineField("Share", ShareModule.Counter.ToString());
            embed.AddInlineField("Stats", StatsModule.Counter.ToString());
            embed.AddInlineField("Welcome", WelcomeModule.Counter.ToString());
            embed.AddInlineField("Update", UpdateModule.RefreshCounter.ToString());
            embed.AddInlineField("8Ball", Magic8BallModule.Counter.ToString());
            embed.AddInlineField("New users", NewUser.NewUsers.ToString());
            embed.AddInlineField("Total Messages", HASSBot.MessagesProcessed.ToString());

            // mention users if any
            await base.MentionUsers();

            await ReplyAsync(string.Empty, false, embed);
        }
    }
}