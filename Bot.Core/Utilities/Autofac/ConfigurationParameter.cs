using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Microsoft.Azure;

namespace Bot.Core.Utilities.Autofac
{
    /// <summary>
    /// Autofac <see cref="Parameter"/> that retrieves values
    /// from either the config file or environments parameters
    /// using <see cref="CloudConfigurationManager.GetSetting(string)"/>.
    /// </summary>
    public class ConfigurationParameter : NamedParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationParameter"/> class.
        /// </summary>
        /// <param name="className">The name of the parameter on the service class.</param>
        /// <param name="configurationName">The name of the configuration parameter.</param>
        /// <param name="defaultValue">The default value if any.</param>
        public ConfigurationParameter(string className, string configurationName, object defaultValue = null)
            : base(className, CloudConfigurationManager.GetSetting(configurationName) ?? (defaultValue ?? throw new ArgumentException($"Missing configuration value {configurationName}")))
        {
        }
    }
}
