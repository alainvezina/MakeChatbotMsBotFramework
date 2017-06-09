using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Bot.Builder.Internals.Fibers;

namespace Bot.Core.Services.Impl
{
    /// <summary>
    /// Implementation of <see cref="IDialogFactory"/>
    /// using Autofac <see cref="IComponentContext"/>.
    /// </summary>
    public class DialogFactory : IDialogFactory
    {
        private readonly IComponentContext _scope;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogFactory"/> class.
        /// </summary>
        public DialogFactory(IComponentContext scope)
        {
            SetField.NotNull(out _scope, nameof(scope), scope);
        }

        /// <inheritdoc />
        public T Create<T>()
        {
            return _scope.Resolve<T>();
        }

        /// <inheritdoc />
        public T Create<T, U>(U parameter)
        {
            return _scope.Resolve<T>(TypedParameter.From(parameter));
        }

        /// <inheritdoc />
        public T Create<T, U, V>(U parameter, V parameter2)
        {
            return _scope.Resolve<T>(TypedParameter.From(parameter), TypedParameter.From(parameter2));
        }

        /// <inheritdoc />
        public T Create<T, U, V, X>(U parameter, V parameter2, X parameter3)
        {
            return _scope.Resolve<T>(TypedParameter.From(parameter), TypedParameter.From(parameter2), TypedParameter.From(parameter3));
        }
    }
}
