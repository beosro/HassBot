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
        [Command("help")]
        public async Task HelpAsync() {
            await HelpCommand();
        }

        [Command("help")]
        public async Task HelpAsync([Remainder]string cmd) {
            await HelpCommand();
        }

        private async Task HelpCommand() {
            StringBuilder sb = new StringBuilder();
            sb.Append("`~about      - Shows information about this bot.`\n");
            sb.Append("`~help       - Displays this message. Usage: ~help`\n");
            sb.Append("`~8ball      - Predicts an answer to a given question. Usage: ~8ball <question> <@optional user1> <@optional user2>...etc`\n");
            sb.Append("`~list       - Shows existing custom command list.`\n");
            sb.Append("`~command    - Create custom commands using: ~command <command name> <command description>`\n");
            sb.Append("`~command    - Run Custom Command. Usage: ~skalavala <@optional user1> <@optional user2>...etc`\n");
            sb.Append("`~lookup     - Provides links to the documentation from sitemap. Usage: ~lookup <search> <@optional user1> <@optional user2>...etc`\n");
            sb.Append("`~deepsearch - Searches hard, sends you a direct message. Use with caution!`\n");
            sb.Append("`~format     - Shows how to format code. Usage: ~format <@optional user1> <@optional user2>...etc`\n");
            sb.Append("`~share      - Shows how to share code that is more than 10 -15 lines. Usage: ~share <@optional user1> <@optional user2>...etc`\n");
            sb.Append("`~lmgtfy     - Googles content for you. Usage: ~lmgtfy <@optional user1> <@optional user2> <search String>`\n");
            sb.Append("`~ping       - Reply with pong. Use this to check if the bot is alive or not. Usage: ~ping`\n");
            sb.Append("`~update     - Refreshes and updates the lookup/sitemap data. Usage: ~update`\n");
            sb.Append("`~yaml?      - Validates the given YAML code. Usage: ~yaml <yaml code> <@optional user1> <@optional user2>...etc`\n");
            sb.Append("`~welcome    - Shows welcome information. Usage: ~welcome <@optional user1> <@optional user2>...etc`\n");
            sb.Append("`~json2yaml  - Converts JSON code to YAML. Usage json2yaml <json code>`");
            sb.Append("`~yaml2json  - Converts YAML code to JSON. Usage: ~yaml2json <yaml code>`");
            sb.Append("\n\n");
            sb.Append("Tip: If you put the yaml/json code in the correct format [\\`\\`\\`yaml <code> \\`\\`\\`], or [\\`\\`\\`json <code> \\`\\`\\`], Hassbot will automatically validate the code, and responds using emojis :thumbsup:\n");

            // mention users if any
            string mentionedUsers = base.MentionUsers();
            await ReplyAsync(mentionedUsers + sb.ToString());
        }
    }
}