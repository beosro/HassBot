///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : HelpModule.cs
//  DESCRIPTION     : A class that implements ~help command
///////////////////////////////////////////////////////////////////////////////
using Discord.Commands;
using System.Text;
using System.Threading.Tasks;

namespace HassBotLib {
    public class HelpModule : BaseModule {
        private static int _counter = 0;
        public static int Counter {
            get {
                return _counter;
            }
            set {
                _counter++;
            }
        }

        private readonly CommandService _service;

        public HelpModule(CommandService service) {
            _service = service;
        }
        public override string GetName() {
            return "about";
        }

        public override int GetCount() {
            return _counter;
        }

        [Command("help")]
        public async Task HelpAsync() {
            Counter++;

            StringBuilder sb = new StringBuilder();
            sb.Append("`~help       - Displays this message.`\n");
            //sb.Append("`~8ball      - Predicts an answer to a given question.`\n");
            sb.Append("`~about      - Shows information about this bot.`\n");
            sb.Append("`~command    - Create custom commands. For ex: ~command <command name> <command description>`\n");
            sb.Append("`~deepsearch - Searches hard, sends you a direct message. Use with caution!`\n");
            sb.Append("`~format     - Shows how to format code.`\n");
            sb.Append("`~lookup     - Provides links to the documentation from sitemap.`\n");
            sb.Append("`~ping       - Reply with pong. Use this to check if the bot is alive or not`\n");
            sb.Append("`~share      - Shows how to share code that is more than 10 -15 lines.`\n");
            sb.Append("`~stats      - Shows some pretty interesting stats about the HassBot.`\n");
            sb.Append("`~update     - Refreshes and updates the lookup/sitemap data.`\n");
            sb.Append("`~welcome    - Shows welcome information Useful to point` <#331130181102206976> `to newcomers.`\n");

            await ReplyAsync(sb.ToString());
        }
    }
}