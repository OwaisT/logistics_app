using LogisticsApp.Application.Authentication.Commands.Register;
using LogisticsApp.Application.Authentication.Common;
using LogisticsApp.Application.Authentication.Queries.Login;
using LogisticsApp.Contracts.Authentication;
using Mapster;

namespace LogisticsApp.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}
