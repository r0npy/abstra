using Abstra.Core.Repositories;
using Abstra.Core.Services;

namespace Abstra.Injections
{
    public static class AutoInjection
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IClientRepository, ClientRepository>();
            builder.Services.AddTransient<IClientService, ClientService>();
        }
    }
}
