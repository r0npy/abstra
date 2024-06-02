using Abstra.Mappers.Responses;

namespace Abstra.Intregration.Test
{
    internal static class SampleModels
    {
        public static ClientGetResponseDto GenerateClientGetResponseDto()
        {
            return new ClientGetResponseDto()
            {
                ClientId = 1,
                Name = "Ronald Riveros",
                Gender = "Male",
                Birthdate = new DateTime(1986, 12, 29),
                Phone = "+95185713",
                Address = "Chaco 3245",
                Status = "Active"
            };
        }        
    }
}
