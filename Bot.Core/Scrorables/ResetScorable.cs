using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bot.Core.Services;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

namespace Bot.Core.Scrorables
{
    /// <summary>
    /// Scorable that implements the global reset command.
    /// </summary>
    public class ResetScorable : SimpleScorable
    {
        private readonly IDialogFactory _dialogFactory;
        private readonly IDialogStack _stack;
        private readonly IBotData _botData;
        private readonly IBotToUser _botToUser;
        private readonly IDialogTask _task;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetScorable"/> class.
        /// </summary>
        public ResetScorable(IDialogFactory dialogFactory, IDialogStack stack, IBotToUser botToUser, IBotData botData, IDialogTask task)
            : base(Resources.ResetScorable_Match)
        {
            SetField.NotNull(out _task, nameof(task), task);
            SetField.NotNull(out _botData, nameof(botData), botData);
            SetField.NotNull(out _stack, nameof(stack), stack);
            SetField.NotNull(out _dialogFactory, nameof(dialogFactory), dialogFactory);
            SetField.NotNull(out _botToUser, nameof(botToUser), botToUser);
        }

        /// <inheritdoc/>
        protected override async Task PostAsync(IActivity item, bool state, CancellationToken token)
        {
            _stack.Reset();
            _botData.UserData.Clear();
            _botData.PrivateConversationData.Clear();
            _botData.ConversationData.Clear();
            await _botData.FlushAsync(token);
            await _botToUser.PostAsync("Profile Reset", cancellationToken: token);
        }
    }
}
