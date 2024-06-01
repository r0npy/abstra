using Abstra.Mappers.Responses;
using Mapster;
using Abstra.Core.Domains;

namespace Abstra.Mappers
{
    public static class MappingConfigurator
    {
        public static void Configure()
        {
            #region Mapster Mapping
            TypeAdapterConfig<Client, ClientGetResponseDto>.NewConfig()
                .Map(dest => dest.IdClient, src => src.ClientId)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Gender, src => src.Gender)
                .Map(dest => dest.Birthdate, src => src.Birthdate)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.Phone, src => src.Phone);
            #endregion
        }
    }
}
