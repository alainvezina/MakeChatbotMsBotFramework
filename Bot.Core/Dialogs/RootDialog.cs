using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Core.Services;
using Microsoft.Bot.Builder.Dialogs;

namespace Bot.Core.Dialogs
{
    /// <summary>
    ///     Root <see cref="IDialog" /> for all conversations.
    /// </summary>
    [Serializable]
    public class RootDialog : IDialog
    {
        private readonly IDialogFactory _dialogFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RootDialog"/> class.
        /// </summary>
        public RootDialog(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        /// <inheritdoc/>
        public Task StartAsync(IDialogContext context)
        {
            return Task.CompletedTask;
        }
    }
}
