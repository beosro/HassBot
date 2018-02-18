///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/04/2018
//  FILE            : Magic8BallModule.cs
//  DESCRIPTION     : A class that implements ~8ball command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HassBotLib {
    public class Magic8BallModule : BaseModule {

        private string previousPrediction = string.Empty;
        private static readonly string ERROR_USAGE =
                "That's not how it works. Try ~8ball <your question>";

        [Command("8ball")]
        public async Task Magic8BallAsync() {
            var embed = new EmbedBuilder();
            embed.WithTitle("Oooops! :8ball:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Usage", ERROR_USAGE);
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("8ball")]
        public async Task Magic8BallAsync([Remainder]string cmd) {
            Random rnd = new Random();
            string[] predictions = {
                    "Yes!", "No!", "Maybe", "Ask again later!", "Likely", "No Way!", "Not now!", "Stars say No!",
                    "Most likely", "Not likely", "Definitely", "Most Definitely", "Concentrate and ask again",
                    "It is certain", "without a doubt!",  "As I see it, Yes!", "Are you crazy?",
                    "Signs point to 'Yes'", "Better not tell you now", "That's a secret!", "Outlook not so good!",
                    "Odds aren't good!", "Outlook is good!", "Don't count on it!"};

            var embed = new EmbedBuilder();
            embed.WithTitle(":8ball:");
            embed.WithColor(Helper.GetRandomColor());
            string prediction = string.Empty;

            // additional logic, so that the 8Ball prediction doesn't repeat
            while (true) {
                int r = rnd.Next(predictions.Count());
                prediction = predictions[r];
                if (previousPrediction != prediction)
                    break;
            }

            // mention users if any
            string mentionedUsers = base.MentionUsers();

            // update previous prediction
            previousPrediction = prediction;
            if ( string.Empty == mentionedUsers)
                embed.AddField("8Ball Prediction:", prediction);
            else
                embed.AddField("8Ball Prediction:", mentionedUsers + prediction);
            await ReplyAsync(string.Empty, false, embed);
        }
    }
}