using Abstra.Core.Repositories;
using Abstra.NUnit.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Abstra.NUnit.Tests
{
    public class ClientRespositoryTest
    {
        IClientRepository _clientRepository;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            _configuration = Configurations.InitConfiguration();
            _clientRepository = new ClientRepository(_configuration);        
        }

        [Test]
        public async Task GetOneClients()
        {
            var actual = await _clientRepository.Get(1);
            var expected = SampleModels.GenerateClient();

            Assert.That(JsonSerializer.Serialize(actual), Is.EqualTo(JsonSerializer.Serialize(expected)));
        }

        [Test]
        public async Task GetAllClients()
        {
            var actual = await _clientRepository.Get();

            Assert.That(actual.Count() > 0);
        }
    }
}
