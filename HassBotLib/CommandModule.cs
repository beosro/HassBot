///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : CommandModule.cs
//  DESCRIPTION     : A class that implements ~command command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HassBotData;
using HassBotDTOs;
using System;

namespace HassBotLib {
    public class CommandModule : BaseModule {
        private static int _counter = 0;
        public static int Counter {
            get {
                return _counter;
            }
            set {
                _counter++;
            }
        }

        private static readonly string USAGE_COMMAND =
            "Usage: ~command <command name> <command description>";

        private static readonly string COMMAND_TOTAL =
            "There are `{0}` custom command(s) available. ";

        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override string GetName() {
            return "command";
        }

        public override int GetCount() {
            return _counter;
        }

        [Command("command")]
        public async Task CommandAsync() {
            Counter++;
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :thinking:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", USAGE_COMMAND);
            await ReplyAsync("", false, embed);
        }

        [Command("command")]
        public async Task CustomCommandAsync([Remainder]string cmd) {
            if (!CheckModPermissions()) {
                var embed = new EmbedBuilder();
                embed.WithTitle("Success! :thumbsdown:");
                embed.WithColor(Color.DarkRed);
                embed.AddInlineField("Sorry!", "You do not have permissions to create command. Only Moderators can create commands.");
                await ReplyAsync("", false, embed);
                return;
            }

            Counter++;
            string command = cmd;
            string value = string.Empty;
            if (cmd.Length > 0) {
                int i = cmd.IndexOf(' ');
                if (i != -1) {
                    command = cmd.Substring(0, i);
                    value = cmd.Substring(i, cmd.Length - i);
                }

                value = (value != null) ? value.Trim() : string.Empty;
                if (value == string.Empty) {
                    // passed empty value - remove that command from list
                    CommandManager.TheCommandManager.RemoveCommandByName(command);
                    return;
                }

                CommandDTO cmdDTO = CommandManager.TheCommandManager.GetCommandByName(command);
                if (cmdDTO == null) {
                    cmdDTO = new CommandDTO();
                    cmdDTO.CommandCount = 0;
                    cmdDTO.CommandData = value;
                    cmdDTO.CommandCreatedDate = DateTime.Now;
                    cmdDTO.CommandName = command;
                }
                cmdDTO.CommandCount += 1;
                cmdDTO.CommandAuthor = Context.User.Mention;
                CommandManager.TheCommandManager.UpdateCommand(cmdDTO);

                var embed = new EmbedBuilder();
                embed.WithTitle("Success! :thumbsup:");
                embed.WithColor(Helper.GetRandomColor());
                embed.AddInlineField("You are all set!", 
                    string.Format("Go ahead and run the command using `~{0}`", command));
                await ReplyAsync("", false, embed);
            }
        }

        // Quick and Dirty way of checking for Mod permissions. TBD 
        private bool CheckModPermissions() {
            if (Context.User.Username == "skalavala" ||
                Context.User.Username == "arsaboo" ||
                Context.User.Username == "Tinkerer" ||
                Context.User.Username == "Vasiley" ||
                Context.User.Username == "dale3h" ||
                Context.User.Username == "baloob" ||
                Context.User.Username == "infamy" ||
                Context.User.Username == "quadflight" ||
                Context.User.Username == "ccostan" ||
                Context.User.Username == "cogneato" ||
                Context.User.Username == "Ludeeus" ||
                Context.User.Username == "bassclarinetl2") {
                return true;
            }
            return false;
        }

        [Command("list")]
        public async Task CommandListAsync() {
            Counter++;
            StringBuilder sb = new StringBuilder(128);

            string commandTotal = string.Format(COMMAND_TOTAL,
                                                CommandManager.TheCommandManager.CommandCount.ToString());

            sb.Append(commandTotal);
            GetCommaSeparatedCommandList(sb);

            await ReplyAsync(sb.ToString());
        }

        private static void GetCommaSeparatedCommandList(StringBuilder buffer) {
            List<CommandDTO> cmds = CommandManager.TheCommandManager.Commands();

            if (buffer == null || cmds == null || cmds.Count == 0)
                return;

            for ( int i = 0; i < cmds.Count; i++ ) {
                if (i == 0) buffer.Append("[ ");
                buffer.Append(cmds[i].CommandName);
                if (i + 1 == cmds.Count)
                    buffer.Append(" ]");
                else
                    buffer.Append(", ");
            }
        }
    }
}