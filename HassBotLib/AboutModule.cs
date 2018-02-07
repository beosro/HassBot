///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : AboutModule.cs
//  DESCRIPTION     : A class that implements ~about command
///////////////////////////////////////////////////////////////////////////////
using System.Threading.Tasks;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Linq;
using Discord;

namespace HassBotLib {
    public class AboutModule : BaseModule {

        private static int _counter = 0;
        public static int Counter {
            get {
                return _counter;
            }
            set {
                _counter++;
            }
        }

        private Process _process;

        public AboutModule() {
            _process = Process.GetCurrentProcess();
        }
        public override string GetName() {
            return "about";
        }

        public override int GetCount() {
            return _counter;
        }

        [Command("about")]
        public async Task About() {
            Counter++;

            var embed = new EmbedBuilder();
            embed.WithTitle($"Hello, This is `{Context.Client.CurrentUser.Username}`, written by @skalavala \n");
            embed.WithColor(Helper.GetRandomColor());
            embed.AddInlineField("Up Since", $"{ GetUptime() }");
            embed.AddInlineField("Total Users", $"{Context.Client.Guilds.Sum(g => g.Users.Count)}");
            embed.AddInlineField("Heap Size", $"{GetHeapSize()} MiB");
            embed.AddInlineField("Memory", $"{ GetMemoryUsage() }");
            embed.AddInlineField("Discord Lib Version", $"{ GetLibrary() }");
            embed.AddInlineField("Latency", $" { GetLatency() }");

            await ReplyAsync(string.Empty, false, embed);
        }

        public string GetUptime() {
            var uptime = (DateTime.Now - _process.StartTime);
            return $"{uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s";
        }

        private static string GetHeapSize()
            => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();

        public string GetLibrary()
            => $"Discord.Net ({DiscordConfig.Version})";

        public string GetMemoryUsage()
            => $"{Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2)}mb";

        public string GetLatency()
            => $"{Context.Client.Latency}ms";
    }
}