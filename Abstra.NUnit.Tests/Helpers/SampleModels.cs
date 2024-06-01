using Abstra.Core.Domains;

namespace Abstra.NUnit.Tests.Helpers
{
    internal static class SampleModels
    {
        public static Client GenerateClient()
        {
            return new Client()
            {
                ClientId = 1,
                Name = "Ronald Riveros",
                Gender = 'M',
                Birthdate = new DateTime(1986, 12, 29),
                Phone = "+95185713",
                Address = "Chaco 3245",
                Status = true
            };
        }
    }
}
