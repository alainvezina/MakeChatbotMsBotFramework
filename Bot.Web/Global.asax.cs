﻿using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Autofac;
using Autofac.Integration.WebApi;
using Cineplex.Bot.Core;
using Cineplex.Bot.Web.Filters;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using Bot.Core;

namespace Bot.Web
{
    /// <summary>
    /// Entry point for the web application.
    /// </summary>
    public class BotApplication : HttpApplication
    {
        /// <summary>
        /// Application start.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(ConfigureWebApi);
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            // Json settings
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
            JsonConvert.DefaultSettings = () => config.Formatters.JsonFormatter.SerializerSettings;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            // Autofac
            var builder = new ContainerBuilder();
            builder.RegisterModule<ReflectionSurrogateModule>();
            builder.RegisterModule<CoreModule>();
            builder.RegisterModule<WebModule>();
#pragma warning disable CS0618 // Type or member is obsolete - because of Bot Framework we have to use it for now.
            builder.Update(Conversation.Container);
#pragma warning restore CS0618 // Type or member is obsolete
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Conversation.Container);

            // Exception handling
            config.Services.Replace(typeof(IExceptionHandler), Conversation.Container.Resolve<GlobalExceptionHandler>());
        }
    }
}
