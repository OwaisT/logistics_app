using LogisticsApp.Application.Authentication.Commands.RegisterFacilityManager;
using LogisticsApp.Application.Authentication.Commands.RegisterFacilityWorker;
using LogisticsApp.Application.Authentication.Common;
using LogisticsApp.Application.Authentication.Queries.Login;
using LogisticsApp.Contracts.Authentication;
using Mapster;

namespace LogisticsApp.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterFacilityWorkerRequest, RegisterFacilityWorkerCommand>();
        config.NewConfig<RegisterFacilityManagerRequest, RegisterFacilityManagerCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Id, src => src.User.Id.Value)
            .Map(dest => dest.FirstName, src => src.User.FirstName)
            .Map(dest => dest.LastName, src => src.User.LastName)
            .Map(dest => dest.Email, src => src.User.Email)
            .Map(dest => dest.Roles, src => src.User.Roles)
            .Map(dest => dest.Token, src => src.Token);
    }
}
