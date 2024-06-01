using Abstra.Core.Domains;
using Abstra.Mappers.Responses;
using Mapster;

namespace Abstra.Mappers
{
    public static class MappingConfigurator
    {
        public static void Configure()
        {
            #region Mapster Mapping
            TypeAdapterConfig<Client, ClientGetResponseDto>.NewConfig()
                .Map(dest => dest.ClientId, src => src.ClientId)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Gender, src => src.Gender == 'M' ? "Male" : "Female")
                .Map(dest => dest.Birthdate, src => src.Birthdate)
                .Map(dest => dest.Address, src => src.Address)
                .Map(dest => dest.Phone, src => src.Phone)
                .Map(dest => dest.Status, src => src.Status ? "Active" : "Inactive");
            #endregion
        }
    }
}
