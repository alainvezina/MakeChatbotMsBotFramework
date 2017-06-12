using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Utilities.Extensions
{
    public static class BotUserExtensions
    {
        /// <summary>
        /// Posts a typing (e.g. "...") message to user.
        /// </summary>
        public static Task PostTyping(this IBotToUser context)
        {
            var reply = context.MakeMessage();
            reply.Type = ActivityTypes.Typing;
            return context.PostAsync(reply);
        }

    }
}
