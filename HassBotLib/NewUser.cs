///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : NewUser.cs
//  DESCRIPTION     : A class that contains "new user" stuff
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HassBotLib {
    public class NewUser {

        private static int _counter = 0;
        public static int NewUsers {
            get {
                return _counter;
            }
            set {
                _counter++;
            }
        }

        //private static readonly string WELCOME_MESSAGE =
        //    "Welcome, {0}! Please make sure you thoroughly read <#331130181102206976> before posting. Thank you!";

        private static readonly string FUNFACT_URL = 
            "http://api.icndb.com/jokes/random?firstName={0}&lastName=&limitTo=[nerdy]";
        private static readonly string POST_DATA = 
            @"{""object"":{""name"":""Name""}}";

        public static async Task NewUserJoined(SocketGuildUser user) {
            NewUsers++;

            StringBuilder sb = new StringBuilder();

            GetWelcomeMessage(sb, user);

            var guild = user.Guild;
            var channel = guild.DefaultChannel;

            // Send a Direct Message to the new Users with instructions
            var dmChannel = await user.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(sb.ToString());

            //// publicly welcome the new user to the channel
            //string funFact = GetRandomFunFact(user.Mention);
            //if ( funFact == string.Empty)
            //    await channel.SendMessageAsync(string.Format(WELCOME_MESSAGE, user.Mention));
            //else {
            //    funFact = WebUtility.HtmlDecode(funFact);
            //    StringBuilder msgWithFunFact = new StringBuilder();
            //    msgWithFunFact.Append(string.Format(WELCOME_MESSAGE, user.Mention));
            //    msgWithFunFact.Append($"\nFun Fact about {user.Mention}: ");
            //    msgWithFunFact.Append(funFact);
            //    msgWithFunFact.Append("\n");
            //    await channel.SendMessageAsync(msgWithFunFact.ToString());
            //}
        }

        private static void GetWelcomeMessage(StringBuilder sb, SocketGuildUser user) {

            sb.Append($"Hello, {user.Mention}! Welcome to Home Assistant Discord Channel.\n\n");
            sb.Append("These are the rules you **MUST** follow in order to get support from the members.\n\n");

            sb.Append("Feel free to introduce yourself, as we are all friends here.\n");
            sb.Append("Do not insult, belittle, or abuse your fellow community members. Any reports of abuse will not be taken lightly and will lead to a ban.\n");
            sb.Append("Head over to <#331106174722113548> channel and run the command `~help` to get started.\n\n");
            sb.Append("We hope you enjoy the company of many talented individuals here!\n\n");

            sb.Append("**A couple of more things to remember:**\n\n");
            sb.Append("1. A maximum of 10-15 lines of code can be posted. For code that is more than 15 lines, please use https://www.hastebin.com\n");
            sb.Append("2. Please make sure you format the code when pasting. Use markdown language when pasting code.\n");
            sb.Append("3. For help, please go to <#331106174722113548> channel and type `~format` or `~share` commands.\n\n");

            sb.Append("Once again, Welcome to the Home Assistant Channel!\n\n");
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