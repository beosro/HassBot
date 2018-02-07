///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : HassBot.cs
//  DESCRIPTION     : The Main Hassbot component
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using HassBotUtils;
using HassBotData;
using HassBotDTOs;

namespace HassBotLib {
    public class HASSBot {

        private static int _messagesProcessed = 0;
        public static int MessagesProcessed {
            get {
                return _messagesProcessed;
            }
            set {
                _messagesProcessed++;
                //PersistStats.SaveStats();
            }
        }

        private static readonly char PREFIX_1 = '~';
        private static readonly char PREFIX_2 = '.';
        private static readonly string TOKEN = "token";
        private static readonly string MAX_LINE_LIMIT =
            "Attention!: Please use https://www.hastebin.com to share code that is more than 10-15 lines. You have been warned, {0}!\nPlease read rule #6 here <#331130181102206976>";
        private static readonly log4net.ILog logger =
             log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        /// <summary>
        /// Use the following method when run as Console App
        /// </summary>
        /// <returns></returns>
        public async Task StartBotAsync() {
            // create client and command objects
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            // Use inversion of control pattern to instantiate singleton objects
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            // add the event handlers for logging
            _client.Log += Helper.LogMessage;
            _commands.Log += Helper.LogMessage;

            _client.UserJoined += NewUser.NewUserJoined;

            // register commands
            await RegisterCommandsAsync();

            string token = AppSettingsUtil.AppSettingsString(TOKEN, true, string.Empty);
            // log into the discord server with the credentials
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // wait forever and process commands! 
            // You are a bot, you have no life!
            await Task.Delay(Timeout.Infinite);
        }

        /// <summary>
        /// Use the following method to run as Service
        /// </summary>
        public async void Start() {
            // create client and command objects
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            // Use inversion of control pattern to instantiate singleton objects
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            // add the event handlers for logging
            _client.Log += Helper.LogMessage;
            _commands.Log += Helper.LogMessage;

            _client.UserJoined += NewUser.NewUserJoined;

            // register commands
            await RegisterCommandsAsync();

            string token = AppSettingsUtil.AppSettingsString(TOKEN, true, string.Empty);
            // log into the discord server with the credentials
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // wait forever and process commands! 
            // You are a bot, you have no life!
            await Task.Delay(Timeout.Infinite);
        }

        /// <summary>
        /// Stops the bot - basically logs out of the server
        /// </summary>
        public async void Stop() {
            await _client.LogoutAsync();
        }

        public async Task RegisterCommandsAsync() {
            _client.MessageReceived += HandleCommandAsync;
            Assembly libAssembly = Assembly.Load("HassBotLib");
            await _commands.AddModulesAsync(libAssembly);
        }

        private async Task HandleCommandAsync(SocketMessage arg) {
            var message = arg as SocketUserMessage;
            if (message == null)
                return;

            // We don't want the bot to respond to itself or other bots.
            // NOTE: Self-bots should invert this first check and remove the second
            // as they should ONLY be allowed to respond to messages from the same account.
            if (message.Author.Id == _client.CurrentUser.Id || message.Author.IsBot)
                return;
            
            // Create a Command Context.
            var context = new SocketCommandContext(_client, message);
            await ReactYamlValidity(message.Content, context);

            if (!Utils.validateNumberOfLines(message.Content))
                await message.Channel.SendMessageAsync(string.Format(MAX_LINE_LIMIT, context.User.Mention));

            // Create a number to track where the prefix ends and the command begins
            int pos = 0;

            if (!(message.HasCharPrefix(PREFIX_1, ref pos) ||
                message.HasCharPrefix(PREFIX_2, ref pos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref pos)))
                return;

            MessagesProcessed++;

            // Execute the command. (result does not indicate a return value, 
            // rather an object stating if the command executed successfully).
            var result = await _commands.ExecuteAsync(context, pos, _services);

            // Uncomment the following lines if you want the bot
            // to send a message if it failed (not advised for most situations).
            if (!result.IsSuccess && result.Error != CommandError.UnknownCommand) {
                // await message.Channel.SendMessageAsync("Oops, something went wrong!");
                logger.Error(result.ErrorReason);
            }
            else {
                // Check the custom commands section before returning
                string key = message.Content.Substring(1);
                CommandDTO cmd = CommandManager.TheCommandManager.GetCommandByName(key);

                if (cmd != null && cmd.CommandData != string.Empty) {
                    cmd.CommandCount += 1;
                    CommandManager.TheCommandManager.UpdateCommand(cmd);
                    await message.Channel.SendMessageAsync(cmd.CommandData);
                }
            }
        }

        private async Task ReactYamlValidity(string content, SocketCommandContext context) {
            if (!(content.Contains("```yaml") || content.Contains("```")))
                return;

            string START_YAML = "```yaml";
            string START_YAML_1 = "```";
            string END_YAML = "```";

            int start = content.IndexOf(START_YAML);
            if ( start == -1 )
                start = content.IndexOf(START_YAML_1);

            int end = content.IndexOf(END_YAML, start+3);
            if (start == -1 && end == -1 && end == start)
                return;

            string substring = content.Substring(start, (end - start));
            string errMsg = string.Empty;
            bool yamlCheck = ValidateYaml.Validate(substring, out errMsg);
            if (yamlCheck) {
                var okEmoji = new Emoji("✅");
                await context.Message.AddReactionAsync(okEmoji);
            }
            else {
                var errorEmoji = new Emoji("❌");
                await context.Message.AddReactionAsync(errorEmoji);
            }
        }
    }
}