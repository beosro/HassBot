## Discord Bot Running as Windows Service (C#)

This is the HassBot (A Discord Bot) I wrote for Home Assistant's Discord Channel. The Bot basically has a bunch of commands - like help, about...etc. It also has custom commands, where the moderators can create commands on the fly and run them as needed. 

The command prefixes are `~` and `.`. That means, the commands can be executed either by `~` or `.`. The following are the list of commands that it supports + custom/default command - which is anything!

```
~help       - Displays this message. Usage: ~help
~8ball      - Predicts an answer to a given question. Usage: ~8ball <question> <@optional user1> <@optional user2>...etc
~about      - Shows information about this bot.
~command    - Create custom commands. For ex: ~command <command name> <command description>
~command    - Run Custom Command. Usage: ~skalavala <@optional user1> <@optional user2>...etc
~deepsearch - Searches hard, sends you a direct message. Use with caution!
~format     - Shows how to format code. Usage: ~format <@optional user1> <@optional user2>...etc
~lookup     - Provides links to the documentation from sitemap. Usage: ~lookup <search> <@optional user1> <@optional user2>...etc
~ping       - Reply with pong. Use this to check if the bot is alive or not. Usage: ~ping
~share      - Shows how to share code that is more than 10 -15 lines. Usage: ~share <@optional user1> <@optional user2>...etc
~stats      - Shows some pretty interesting stats about the HassBot. Usage: ~stats
~update     - Refreshes and updates the lookup/sitemap data. Usage: ~update
~yaml?      - Validates the given YAML code. Usage: ~yaml <yaml code> <@optional user1> <@optional user2>...etc
~welcome    - Shows welcome information Useful & point #welcome-rules to newcomers. Usage: ~welcome <@optional user1> <@optional user2>...etc
```

# Default Command is `Lookup` Command

Apart from the commands listed above, one can also simply search by providing search string as the command. For ex: Even if there is no "pre-defined" command, called "`xyz`", you can call using `~xyz`. That will check in the sitemap and if there are any matching entries, it will give you the links to those. If not, an emoji reaction will be added to the original request.
Lst's say there is no pre-defined command, called `duckdns`. But there is a component in Home Assistant listed in `sitemap.xml`, you can simply get the link to that by running 

```
~duckdns
```

## Running the Bot as a Windows Service
The program is written in C# and uses Discord.Net warpper for Discord API. THe Bot runs as a Windows Service instead of Command Line Applications you see everywhere. The package consists of one solution, and a Console Application to test the code/bot, and a Windows Service that is deployed to the server to run. There are a bunch of common libraries that are shared between Console App and Windows Service, and you will find most of the code there. The Console App and the Windows Service are basically dummy clients that utilize the common components.

## The features include:

### Welcoming new users
Every time a new user joins the channel, it sends out a public announcement, welcoming the user, and also sends a personal/direct message explaining the rules of the channel.

### Code Limits
There is a limit of 10-15 lines of code when posting to prevent code walls. The Bot checks for the number of lines, and issues a citation when violated.

### YAML Verification
People who come to the Home Assistant Discord channel tend to post their configuration and automation seeking for help. There is an automatic YAML verification in place, where everytime someone posts code, it automatically verifies the code, and responses in the form of emojis whether the code passed the test or it failed the test. Sort of like `yamllint`, except it is realtime.

### lookup
When the lookup command is issued with a parameter, it searches in the Home Assistant's sitemap url (https://home-assistant.io/sitemap.xml) and points to the right articles and links.

### 8Ball
A fun command that randomly gives predictions to the questions. The answers are rarely and barely accurate.

### Ping
Used to check the pulse of the Bot. When the `~ping` command is issued, the bot responses `Pong!`.

### Welcome
When the command `~welcome` is issued, it reminds the user to follow welcome rules.

...more.

A bit shout out to @Tinkerer and @Ludeeus for the requirement and testing :smile:
