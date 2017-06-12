using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Core.Scrorables;
using Bot.Core.Utilities.Extensions;
using Bot.Core.Dialogs;
using Bot.Core.Services;
using Bot.Core.Services.Impl;
using Bot.Core.Dialogs.Flows;

namespace Bot.Core
{
    public class CoreModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterScopedService<IDialogFactory, DialogFactory>();

            builder
                .RegisterDialog<MenuFlow>()
                .RegisterDialog<RootDialog>()
                .RegisterSimpleScorable<GetConversationInfoScorable>()
                .RegisterSimpleScorable<ResetScorable>();
        }
    }
}
