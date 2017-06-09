using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Core
{
    /// <summary>
    /// List of string constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// List of string constants for ConversationData
        /// </summary>
        public static class ConversationData
        {
            /// <summary>
            /// Key name where GeoCordinates are stored
            /// </summary>
            public const string LocationKey = "Location";

            /// <summary>
            /// Key name where flag first encounter is stored
            /// </summary>
            public const string FirstEncounter = "FirstEncounter";
        }

        /// <summary>
        /// List of string constants for Channel
        /// </summary>
        public static class Channel
        {
            /// <summary>
            /// Name of the facebook channel
            /// </summary>
            public const string FacebookKey = "Facebook";
        }
    }
}
