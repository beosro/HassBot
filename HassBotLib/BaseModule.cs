///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : BaseModule.cs
//  DESCRIPTION     : A base class for all Module Components
///////////////////////////////////////////////////////////////////////////////
using Discord.Commands;
using System.Threading.Tasks;

namespace HassBotLib {

    public abstract class BaseModule : ModuleBase<SocketCommandContext> {
        abstract public string GetName();
        abstract public int GetCount();

        protected string MentionUsers() {
            string mentionedUsers = string.Empty;
            foreach (var user in Context.Message.MentionedUsers) {
                mentionedUsers += $"{user.Mention} ";
            }

            return mentionedUsers;
        }
    }
}