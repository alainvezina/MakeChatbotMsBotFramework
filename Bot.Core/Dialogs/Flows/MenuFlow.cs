using Bot.Core.Services;
using Bot.Core.Utilities.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Dialogs.Flows
{
    /// <summary>
    /// This flow is responsible for:
    ///  - Displaying greating
    ///  - Displaying the root menu
    /// </summary>
    [Serializable]
    public class MenuFlow : IDialog<object>
    {
        private readonly IDialogFactory _dialogFactory;
 
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuFlow"/> class.
        /// </summary>
        public MenuFlow(IDialogFactory dialogFactory)
        {
            SetField.NotNull(out _dialogFactory, nameof(dialogFactory), dialogFactory);
        }

        /// <inheritdoc/>
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostTyping();
            await PostMenu(context);
        }

        /// <summary>
        /// Post menu switch channel
        /// </summary>
        private async Task PostMenu(IDialogContext context, bool isReplay = false)
        {
            if (context.ConversationData.ContainsKey(Constants.ConversationData.FirstEncounter))
            {
                await context.PostAsync(Resources.Dialog_HiAgain);
            }
            else
            {
                await context.PostAsync(Resources.Dialog_HiThere);
                await context.PostAsync(Resources.Dialog_Welcome);
                context.ConversationData.SetValue(Constants.ConversationData.FirstEncounter, true);
            }
        }
    }
}