﻿///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : NewUser.cs
//  DESCRIPTION     : A class that contains "new user" stuff
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.WebSocket;
using HassBotUtils;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HassBotLib {
    public class NewUser {

        private static readonly string FUNFACT_URL = 
            "http://api.icndb.com/jokes/random?firstName={0}&lastName=&limitTo=[nerdy]";

        private static readonly string POST_DATA = 
            @"{""object"":{""name"":""Name""}}";

        public static async Task NewUserJoined(SocketGuildUser user) {
            StringBuilder sb = new StringBuilder();

            GetWelcomeMessage(sb, user);

            // Send a Direct Message to the new Users with instructions
            var dmChannel = await user.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(sb.ToString());
        }

        private static void GetWelcomeMessage(StringBuilder sb, SocketGuildUser user) {

            string botspamChannel = AppSettingsUtil.AppSettingsString("botspamChannel", false, string.Empty);

            sb.Append($"Hello, {user.Mention}! Welcome to Home Assistant Discord Channel.\n\n");
            sb.Append("These are the rules you **MUST** follow in order to get support from the members.\n\n");

            sb.Append("Feel free to introduce yourself, as we are all friends here.\n");
            sb.Append("Do not insult, belittle, or abuse your fellow community members. Any reports of abuse will not be taken lightly and will lead to a ban.\n");
            sb.Append(string.Format("Head over to {0} channel and run the command `~help` to get started.\n\n", botspamChannel));
            sb.Append("We hope you enjoy the company of many talented individuals here!\n\n");

            sb.Append("A few more **important things** to remember:\n\n");
            sb.Append("1. A maximum of 10-15 lines of code can be posted. For code that is more than 15 lines, please use https://www.hastebin.com\n");
            sb.Append("2. Please make sure you format the code when pasting. Use markdown language when pasting code.\n");
            sb.Append(string.Format("3. For help, please go to {0} channel and type `~format` or `~share` commands.\n\n", botspamChannel));
            sb.Append("4. To get access to the #github, #status #reddit and #botspam channels, run `?rank other` command from the #lounge channel.\n\n");

            sb.Append(string.Format("Once again, Welcome to the {0} Channel!\n\n", user.Guild.Name));
        }

        private static string GetRandomFunFact(string userHandle) {
            string url = string.Format(FUNFACT_URL, userHandle);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = POST_DATA.Length;
            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            requestWriter.Write(POST_DATA);
            requestWriter.Close();

            try {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                responseReader.Close();
                dynamic stuff = JObject.Parse(response);
                return stuff.value.joke;
            }
            catch{
                return string.Empty;
            }
        }
    }
}