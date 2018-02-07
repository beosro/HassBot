///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : Ping.cs
//  DESCRIPTION     : A class that implements ~ping command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace HassBotLib {
    public class PingModule : BaseModule {
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
            return "ping";
        }

        public override int GetCount() {
            return _counter;
        }

        [Command("ping")]
        public async Task PingAsync() {
            Counter++;

            var embed = new EmbedBuilder();
            embed.WithTitle(":ping_pong:");
            embed.WithColor(Color.DarkRed);
            embed.AddField("ping?", "PONG!!!");
            await ReplyAsync("", false, embed);
        }
    }
}