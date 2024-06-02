using Abstra.Mappers.Requests;
using Abstra.Mappers.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Abstra.Intregration.Test
{
    [TestClass]
    public class ClientTest
    {
        private static readonly char[] pswd = { '1', '2', '3', '4', '5' };

        [TestMethod]
        public async Task GetOneCliente()
        {
            WebApplicationFactory<Abstra.Program> webApplicationFactory = new();

            var httpClient = webApplicationFactory.CreateDefaultClient();

            var loginRequest = new ClientPostRequestLoginDto
            {
                ClientId = 1,
                Password = new string(pswd)
            };

            var loginContent = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");
            var loginResponse = await httpClient.PostAsync("/api/client/login", loginContent);
            loginResponse.EnsureSuccessStatusCode();

            var loginStringResult = await loginResponse.Content.ReadAsStringAsync();
            var loginResponseDto = JsonSerializer.Deserialize<ClientPostResponseLoginDto>(loginStringResult);
            var accessToken = loginResponseDto?.AccessToken;

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.GetAsync("/api/client/1");

            var stringResult = await response.Content.ReadAsStringAsync();

            ClientGetResponseDto? actual = JsonSerializer.Deserialize<ClientGetResponseDto>(stringResult);

            ClientGetResponseDto expected = SampleModels.GenerateClientGetResponseDto();

            Console.WriteLine(stringResult);

            Assert.AreEqual(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actual));
        }
    }
}
