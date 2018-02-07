///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : BaseModule.cs
//  DESCRIPTION     : A base class for all Module Components
///////////////////////////////////////////////////////////////////////////////
using Discord.Commands;

namespace HassBotLib {
    public abstract class BaseModule : ModuleBase<SocketCommandContext> {
        abstract public string GetName();
        abstract public int GetCount();
    }
}