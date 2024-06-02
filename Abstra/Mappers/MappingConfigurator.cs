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

            TypeAdapterConfig<Account, AcccountGetResponseDto>.NewConfig()
                .Map(dest => dest.AccountId, src => src.AccountId)
                .Map(dest => dest.AccountNumber, src => src.AccountNumber)
                .Map(dest => dest.AccountType, src => src.AccountType == 'A' ? "Cuenta de Ahorro" : "Cuenta Corriente")
                .Map(dest => dest.ClientId, src => src.Client!.ClientId)
                .Map(dest => dest.ClientName, src => src.Client!.Name)
                .Map(dest => dest.InitialBalance, src => src.InitialBalance)
                .Map(dest => dest.Status, src => src.Status ? "Active" : "Inactive");
            #endregion
        }
    }
}
