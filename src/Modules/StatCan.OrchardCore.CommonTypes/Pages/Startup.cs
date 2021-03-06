using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.ResourceManagement;
using OrchardCore.Data.Migration;

namespace StatCan.OrchardCore.CommonTypes.Page
{
    [Feature(FeatureIds.Pages)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection serviceCollection) => serviceCollection.AddScoped<IDataMigration, Migrations>();
    }
}
