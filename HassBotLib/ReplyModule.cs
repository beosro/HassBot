/////////////////////////////////////////////////////////////////////////////////
////  AUTHOR          : Suresh Kalavala
////  DATE            : 03/15/2018
////  FILE            : ReplyModule.cs
////  DESCRIPTION     : A class that implements ~reply command
/////////////////////////////////////////////////////////////////////////////////
//using Discord;
//using Discord.Commands;
//using System;
//using System.Linq;
//using System.Threading.Tasks;

//namespace HassBotLib {
//    public class ReplyModule : BaseModule {

//        private static readonly string ERROR_USAGE = "Try ~reply message_id <your reply>";

//        [Command("reply")]
//        public async Task ReplyAsync() {
//            var embed = new EmbedBuilder();
//            embed.WithTitle("Oooops! :pencil2:");
//            embed.WithColor(Color.DarkRed);
//            embed.AddInlineField("Usage", ERROR_USAGE);
//            await ReplyAsync(string.Empty, false, embed);
//        }

//        [Command("reply")]
//        public async Task ReplyAsync([Remainder]string cmd) {
//            string[] parts = cmd.Split(' ');
//            string quoted_message_id = parts[0];
//            string reply_message = cmd.Replace(quoted_message_id, string.Empty);

//            var embed = new EmbedBuilder();
//            EmbedAuthorBuilder outerAuthor = new EmbedAuthorBuilder();
//            embed.Author = outerAuthor;
//            embed.Author.Name = Context.User.Username;
//            embed.Author.IconUrl = Context.User.GetAvatarUrl();

//            // delete the original message
//            await Context.Message.DeleteAsync();

//            var messages = await Context.Channel.GetMessagesAsync(100).Flatten();
//            foreach (IUserMessage quotedMessage in messages) {

//                if (quotedMessage.Id == ulong.Parse(quoted_message_id)) {
//                    if (quotedMessage.Content == string.Empty)
//                        return;

//                    embed.WithColor(Helper.GetRandomColor());

//                    // post the original message as-is
//                    await Context.Channel.SendMessageAsync(string.Empty, false, new EmbedBuilder() {
//                        Author = new EmbedAuthorBuilder() {
//                            Name = quotedMessage.Author.Username,
//                            IconUrl = quotedMessage.Author.GetAvatarUrl()
//                        },
//                        Title = "Said: ",
//                        Description = quotedMessage.Content
//                    });
//                }
//            }

//            //await ReplyAsync(reply_message, false, null);

//            // add your reply

//            await Context.Channel.SendMessageAsync(string.Empty, false, new EmbedBuilder() {
//                Author = new EmbedAuthorBuilder() {
//                    Name = Context.User.Username,
//                    IconUrl = Context.User.GetAvatarUrl()
//                },
//                Title = "Says :",
//                Description = reply_message
//            });
//        }
//    }
//}