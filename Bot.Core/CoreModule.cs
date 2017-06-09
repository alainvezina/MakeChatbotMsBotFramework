using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Core.Scrorables;
using Bot.Core.Utilities.Extensions;

namespace Bot.Core
{
    public class CoreModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterSimpleScorable<GetConversationInfoScorable>()
                .RegisterSimpleScorable<ResetScorable>();
        }
    }
}
