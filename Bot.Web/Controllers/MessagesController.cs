using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Bot.Web.Models.Errors;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using Autofac;
using Bot.Core;
using Bot.Core.Dialogs;

namespace Bot.Web.Controllers
{
    /// <summary>
    /// Controller for the Bot Connector.
    /// </summary>
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// This is the Bot Connector Api entry point for all messages.
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity == null)
            {
                return Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseModel(new ErrorModel(ErrorCode.ValidationError, "Missing activity (body)")));
            }

            if (activity.Type == ActivityTypes.Message)
            {
                using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, activity))
                {
                    var dialog = scope.Resolve<RootDialog>();
                    await Conversation.SendAsync(activity, () => dialog);
                }
            }
            else
            {
                await HandleSystemMessage(activity);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private async Task HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                IConversationUpdateActivity update = message;
                using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
                {
                    if (update.MembersAdded.Any(o => o.Id == message.Recipient.Id))
                    {
                        var reply = message.CreateReply("Cineplex Chatbot can help you find movies you likes and buy tickets from Canada's most popular destination for movies, showtimes, tickets, and trailers.");
                        ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));
                        await connector.Conversations.ReplyToActivityAsync(reply);

                        switch (message.ChannelId)
                        {
                            case Core.Constants.Channel.FacebookKey:
                                break;
                            default:
                                reply = message.CreateReply("Please type Get started to get going!");
                                connector = new ConnectorClient(new Uri(message.ServiceUrl));
                                await connector.Conversations.ReplyToActivityAsync(reply);
                                break;
                        }
                    }
                }
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }
        }
    }
}