using Microsoft.Extensions.Configuration;

namespace Abstra.NUnit.Tests.Helpers
{
    internal static class Configurations
    {
        internal static IConfiguration InitConfiguration()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(projectDir)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return configurationBuilder.Build();
        }
    }
}
