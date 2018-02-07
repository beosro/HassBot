## Discord Bot Running as Windows Service (C#)

This is the HassBot (A Discord Bot) I wrote for Home Assistant's Discord Channel. The Bot basically has a bunch of commands - like help, about...etc. It also has custom commands, where the moderators can create commands on the fly and run them as needed. 

```
~help    - Displays this message.
~command - Create custom commands. For ex: ~command <command name> <command description>
~format  - Shows how to format code.
~info    - Shows information about this bot.
~lookup  - Provides links to the documentation.
~ping    - Reply with pong. Use this to check if the bot is alive or not
~share   - Shows how to share code that is more than 10 -15 lines.
~welcome - Shows welcome information Useful to point #welcome-rules to newcomers.
~stats   - Shows some pretty interesting stats about the HassBot.
```

The command prefixes are `~` and `.`. That means, the commands can be executed either by `~` or `.`.

## Running the Bot as a Windows Service
The program is written in C#, and runs as a Windows Service. The package consists of one solution, and a Console Application to test the code/bot, and a Windows Service that is deployed to the server to run. There are a bunch of common libraries that are shared between Console App and Windows Service, and you will find most of the code there. The Console App and the Windows Service are basically dummy clients that utilize the common components.

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
