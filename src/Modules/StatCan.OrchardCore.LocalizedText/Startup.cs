using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Modules;
using OrchardCore.Data.Migration;
using OrchardCore.Liquid;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Scripting;
using StatCan.OrchardCore.LocalizedText.Drivers;
using StatCan.OrchardCore.LocalizedText.Fields;
using StatCan.OrchardCore.LocalizedText.Models;
using StatCan.OrchardCore.LocalizedText.Liquid;
using StatCan.OrchardCore.LocalizedText.Scripting;
using OrchardCore.DisplayManagement.Implementation;

namespace StatCan.OrchardCore.LocalizedText
{
    [Feature(Constants.Features.LocalizedText)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataMigration, Migrations>();

            services.AddScoped<ILocalizedTextAccessor, LocalizedTextDisplayEvents>();
            services.AddScoped<IShapeDisplayEvents, LocalizedTextDisplayEvents>(sp => (LocalizedTextDisplayEvents)sp.GetRequiredService<ILocalizedTextAccessor>());

            services.AddContentPart<LocalizedTextPart>()
                .UseDisplayDriver<LocalizedTextPartDisplayDriver>();

            services.AddSingleton<IGlobalMethodProvider, GlobalMethodProvider>();
        }
    }

    [RequireFeatures("OrchardCore.Liquid")]
    public class LiquidStartup : StartupBase
    {
        public LiquidStartup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<LocalizedTextPart>();
            TemplateContext.GlobalMemberAccessStrategy.Register<LocalizedTextEntry>();
            TemplateContext.GlobalMemberAccessStrategy.Register<LocalizedTextItem>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddLiquidFilter<LocalizedTextFilter>("localize");
        }
    }
}
