using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.WebApi;
using Bot.Core.Entities;
using Bot.Core.Utilities.Autofac;
using Bot.Core.Utilities.Extensions;
using Bot.Web.Filter;
using Microsoft.ApplicationInsights;
using Microsoft.Azure;
using Microsoft.Bot.Builder.Azure;
using Microsoft.WindowsAzure.Storage;

namespace Bot.Web
{
    public class WebModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterSingleInstanceService<TelemetryClient, TelemetryClient>()
                .RegisterOptions<ApplicationContext>(
                    new NamedParameter("name", "My Bot"),
                    new ConfigurationParameter("environment", "Environment", defaultValue: "Production"),
                    new NamedParameter("version", typeof(WebModule).Assembly.GetName().Version.ToString()),
                    new NamedParameter("versionInformation",
                        FileVersionInfo.GetVersionInfo(typeof(WebModule).Assembly.Location).ProductVersion));

               
            builder.RegisterApiControllers(typeof(WebModule).Assembly);

            builder.RegisterType<GlobalExceptionHandler>()
                .WithParameter(new NamedParameter("includeStackTrace", true))
                .AsSelf()
                .SingleInstance();
        }
    }
}