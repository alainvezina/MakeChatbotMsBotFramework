using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chronic;
using Bot.Core.Utilities.Extensions;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Scorables.Internals;
using Microsoft.Bot.Connector;

namespace Bot.Core.Scrorables
{ /// <summary>
    /// Base class for <see cref="Microsoft.Bot.Builder.Scorables.IScorable{Item, Score}"/>
    /// that evaluates basic score as a <see cref="bool"/> using <see cref="StringExtensions.HumanEquals(string, string)"/>.
    /// A scorable represents a global action which can be triggered from anywhere
    /// in the dialog stack and will superseed any dialog action.
    /// </summary>
    public abstract class SimpleScorable : ScorableBase<IActivity, bool, double>
    {
        private readonly IEnumerable<string> _matches;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleScorable"/> class.
        /// </summary>
        protected SimpleScorable(params string[] matches)
        {
            SetField.NotNull(out _matches, nameof(matches), matches);
        }

        /// <inheritdoc/>
        protected override Task<bool> PrepareAsync(IActivity item, CancellationToken token)
        {
            if (item is IMessageActivity message && !string.IsNullOrEmpty(message.Text))
            {
                return Task.FromResult(_matches.Any(x => x.HumanEquals(message.Text)));
            }

            return Task.FromResult(false);
        }

        /// <inheritdoc/>
        protected override bool HasScore(IActivity item, bool state) => state;

        /// <inheritdoc/>
        protected override double GetScore(IActivity item, bool state) => 1d;

        /// <inheritdoc/>
        protected override Task DoneAsync(IActivity item, bool state, CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}
