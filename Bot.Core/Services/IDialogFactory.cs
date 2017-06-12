using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Services
{
    /// <summary>
    /// Factory for <see cref="IDialog"/>
    /// </summary>
    public interface IDialogFactory
    {
        /// <summary>
        /// Creates a dialog of type <typeparamref name="T"/>.
        /// </summary>
        T Create<T>();

        /// <summary>
        /// Creates a dialog of type <typeparamref name="T"/>
        /// using the single <paramref name="parameter"/> as argument.
        /// </summary>
        T Create<T, U>(U parameter);

        /// <summary>
        /// Creates a dialog of type <typeparamref name="T"/>
        /// using the two parameters as arguments.
        /// </summary>
        T Create<T, U, V>(U parameter, V parameter2);

        /// <summary>
        /// Creates a dialog of type <typeparamref name="T"/>
        /// using the two parameters as arguments.
        /// </summary>
        T Create<T, U, V, X>(U parameter, V parameter2, X parameter3);
    }
}
