using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Bot.Core.Scrorables;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Scorables;
using Microsoft.Bot.Connector;

namespace Bot.Core.Utilities.Extensions
{
    /// <summary>
    /// <see cref="ContainerBuilder"/> extensions.
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Registers a single instance service.
        /// </summary>
        /// <typeparam name="TInterface">The main service interface type.</typeparam>
        /// <typeparam name="TImplementation">The implementation service type.</typeparam>
        public static ContainerBuilder RegisterSingleInstanceService<TInterface, TImplementation>(this ContainerBuilder builder)
            where TImplementation : TInterface
        {
            builder.RegisterType<TImplementation>()
                .Keyed<TInterface>(FiberModule.Key_DoNotSerialize)
                .As<TInterface>()
                .AsImplementedInterfaces()
                .SingleInstance();
            return builder;
        }

        /// <summary>
        /// Registers a service with InstancePerLifetimeScope.
        /// </summary>
        /// <typeparam name="TInterface">The main service interface type.</typeparam>
        /// <typeparam name="TImplementation">The implementation service type.</typeparam>
        public static ContainerBuilder RegisterScopedService<TInterface, TImplementation>(this ContainerBuilder builder)
            where TImplementation : TInterface
        {
            builder.RegisterType<TImplementation>()
                .Keyed<TInterface>(FiberModule.Key_DoNotSerialize)
                .As<TInterface>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            return builder;
        }

        /// <summary>
        /// Registers <typeparamref name="T"/> as a <see cref="SimpleScorable"/>
        /// </summary>
        public static ContainerBuilder RegisterSimpleScorable<T>(this ContainerBuilder builder)
            where T : SimpleScorable
        {
            builder
                .RegisterType<T>()
                .As<IScorable<IActivity, double>>()
                .InstancePerLifetimeScope();
            return builder;
        }

        /// <summary>
        /// Registers a dialog.
        /// </summary>
        public static ContainerBuilder RegisterDialog<TDialog, TResult>(this ContainerBuilder builder)
            where TDialog : IDialog<TResult>
        {
            builder.RegisterType<TDialog>()
                .As<IDialog<TResult>>()
                .InstancePerDependency();
            return builder;
        }

        /// <summary>
        /// Registers a dialog.
        /// </summary>
        public static ContainerBuilder RegisterDialog<TDialog>(this ContainerBuilder builder)
        {
            builder.RegisterType<TDialog>()
                .AsSelf()
                .InstancePerDependency();
            return builder;
        }

        /// <summary>
        /// Registers <typeparamref name="T"/> as an option class
        /// (single instance, as themselves, with configuration-injected parameters.
        /// </summary>
        public static ContainerBuilder RegisterOptions<T>(this ContainerBuilder builder, params Parameter[] parameters)
        {
            var registrationBuilder = builder.RegisterType<T>()
                .Keyed<T>(FiberModule.Key_DoNotSerialize);

            foreach (var parameter in parameters ?? Enumerable.Empty<Parameter>())
            {
                registrationBuilder = registrationBuilder.WithParameter(parameter);
            }

            registrationBuilder
                .As<T>()
                .SingleInstance();

            return builder;
        }
    }
}
