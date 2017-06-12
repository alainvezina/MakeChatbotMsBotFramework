using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core.Entities
{
    /// <summary>
    /// Represents the overall context for the Bot application.
    /// </summary>
    public class ApplicationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationContext"/> class.
        /// </summary>
        public ApplicationContext(string name, string environment, string version, string versionInformation)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
            Version = version ?? throw new ArgumentNullException(nameof(name));
            VersionInformation = versionInformation ?? throw new ArgumentNullException(nameof(versionInformation));
        }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the environment
        /// </summary>
        public string Environment { get; }

        /// <summary>
        /// Gets the version of the application.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Gets specific version information.
        /// </summary>
        public string VersionInformation { get; }
    }
}
