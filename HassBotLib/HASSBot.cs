///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : HassBot.cs
//  DESCRIPTION     : The Main Hassbot component
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HassBotData;
using HassBotDTOs;
using HassBotUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace HassBotLib {
    public class HASSBot {
        private static readonly char PREFIX_1 = '~';
        private static readonly char PREFIX_2 = '.';
        private static readonly string POOP = "💩";

        private static readonly string TOKEN = "token";
        private static readonly string MAX_LINE_LIMIT =
            "Attention!: Please use https://www.hastebin.com to share code that is more than 10-15 lines. You have been warned, {0}!\nPlease read rule #6 here <#331130181102206976>";
        private static readonly log4net.ILog logger =
             log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task StartBotAsync() {
            await StartInternal();
        }

        public async void Start() {
            await StartInternal();
        }

        public async void Stop() {
            await _client.LogoutAsync();
        }

        private async Task StartInternal() {
            // create client and command objects
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            // register commands
            await RegisterCommandsAsync();

            string token = AppSettingsUtil.AppSettingsString(TOKEN, true, string.Empty);
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // wait forever and process commands! 
            await Task.Delay(Timeout.Infinite);
        }

        private async Task RegisterCommandsAsync() {
            _client.Log += Helper.LogMessage;
            _commands.Log += Helper.LogMessage;
            _client.UserJoined += NewUser.NewUserJoined;
            _client.MessageReceived += HandleCommandAsync;

            Assembly libAssembly = Assembly.Load("HassBotLib");
            await _commands.AddModulesAsync(libAssembly);
        }

        private async Task HandleCommandAsync(SocketMessage arg) {
            var message = arg as SocketUserMessage;
            if (message == null)
                return;

            // Create a Command Context.
            var context = new SocketCommandContext(_client, message);
            var channel = message.Channel as SocketGuildChannel;
            await Helper.DeleteHoundCIMessages(message, context, channel);

            // We don't want the bot to respond to itself or other bots.
            if (message.Author.Id == _client.CurrentUser.Id || message.Author.IsBot)
                return;

            await Helper.ReactToYaml(message.Content, context);

            if (!Utils.LineCountCheck(message.Content)) {
                var poopEmoji = new Emoji(POOP);
                string msxLimitMsg = AppSettingsUtil.AppSettingsString("maxLineLimitMessage", false, MAX_LINE_LIMIT);
                await message.Channel.SendMessageAsync(string.Format(msxLimitMsg, context.User.Mention));
                await context.Message.AddReactionAsync(poopEmoji);
            }

            // Create a number to track where the prefix ends and the command begins
            int pos = 0;

            if (!(message.HasCharPrefix(PREFIX_1, ref pos) ||
                  message.HasCharPrefix(PREFIX_2, ref pos) ||
                  message.HasMentionPrefix(_client.CurrentUser, ref pos)))
                return;

            var result = await _commands.ExecuteAsync(context, pos, _services);

            if (!result.IsSuccess && result.Error != CommandError.UnknownCommand) {
                logger.Error(result.ErrorReason);
                return;
            }

            string key = message.Content.Substring(1);
            string command = key.Split(' ')[0];

            string mentionedUsers = PrepareMentionedUsers( message );

            CommandDTO cmd = CommandManager.TheCommandManager.GetCommandByName(command);
            if (cmd != null && cmd.CommandData != string.Empty) {
                cmd.CommandCount += 1;
                CommandManager.TheCommandManager.UpdateCommand(cmd);
                await message.Channel.SendMessageAsync(mentionedUsers + cmd.CommandData);
            }
            else {
                if (result.IsSuccess)
                    return;

                // command not found, look it up and see if there are any results.
                string lookupResult = Sitemap.Lookup(command);
                if (string.Empty != lookupResult) {
                    await message.Channel.SendMessageAsync(mentionedUsers + lookupResult);
                }
            }
        }

        private static string PrepareMentionedUsers(SocketUserMessage message) {
            if ( null == message || message.MentionedUsers.Count == 0 )
                return string.Empty;

            string mentionedUsers = string.Empty;
            foreach (var user in message.MentionedUsers) {
                mentionedUsers += $"{user.Mention} ";
            }

            return mentionedUsers;
        }
    }
}