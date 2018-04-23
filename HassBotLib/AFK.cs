///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : AwayFromKeyboard.cs
//  DESCRIPTION     : A class that implements ~away command
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
    public class AFK : BaseModule {

        private static readonly string USAGE_COMMAND = 
            "Usage: `~afk <message>` or `~away <message>` or `~seen <username>`";

        [Command("afk"), Alias("away")]
        public async Task AFKAsync() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :thinking:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", USAGE_COMMAND);
            await ReplyAsync("", false, embed);
        }

        [Command("afk"), Alias("away")]
        public async Task AFKAsync([Remainder]string afkMessage) {
            string userName = Context.User.Username;
            AFKDTO afkDTO = AFKManager.TheAFKManager.GetAFKByName(userName);
            if (afkDTO == null) {
                afkDTO = new AFKDTO();
                afkDTO.Id = Context.User.Id;
                afkDTO.AwayMessage = afkMessage;
                afkDTO.AwayTime = DateTime.Now;
                afkDTO.AwayUser = userName;
            }
            AFKManager.TheAFKManager.UpdateAFK(afkDTO);
            await ReplyAsync(string.Format("{0} is away! {1} :wave:", userName, afkMessage));
        }

        [Command("seen")]
        public async Task SeenAsync() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :thinking:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", USAGE_COMMAND);
            await ReplyAsync("", false, embed);
        }

        [Command("seen")]
        public async Task SeenAsync(string afkMessage) {
            AFKDTO afkDTO = AFKManager.TheAFKManager.GetAFKByName(afkMessage);
            if (afkDTO == null)
                return;
            
            string msg = "**{0} is away** for {1}with a message :point_right:   \"{2}\"";
            string awayFor = string.Empty;
            if ((DateTime.Now - afkDTO.AwayTime).Days > 0) {
                awayFor += (DateTime.Now - afkDTO.AwayTime).Days.ToString() + "d ";
            }
            if ((DateTime.Now - afkDTO.AwayTime).Hours > 0) {
                awayFor += (DateTime.Now - afkDTO.AwayTime).Hours.ToString() + "h ";
            }
            if ((DateTime.Now - afkDTO.AwayTime).Minutes > 0) {
                awayFor += (DateTime.Now - afkDTO.AwayTime).Minutes.ToString() + "m ";
            }
            if ((DateTime.Now - afkDTO.AwayTime).Seconds > 0) {
                awayFor += (DateTime.Now - afkDTO.AwayTime).Seconds.ToString() + "s ";
            }

            string message = string.Format(msg, afkDTO.AwayUser, awayFor, afkDTO.AwayMessage);

            await ReplyAsync(message);
        }
    }
}