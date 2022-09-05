using AppGrpcService;
using Grpc.Core;
using App.Microservices.AuthServer.Services;

namespace App.Microservices.AuthServer.GrpcServices;

public class UserGrpcService : Users.UsersBase
{
    private readonly IUserService _userService;
    public UserGrpcService(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task<GrpcLoginResponse> Login(GrpcLoginRequest request, ServerCallContext context)
    {
        var applicationUser = await _userService.Authenticate(request.Username, request.Password);
        if (applicationUser == null)
        {
            return new GrpcLoginResponse
            {
                Token = "",
                RefreshToken = ""
            };
        }

        return await Task.FromResult(new GrpcLoginResponse{
            Token= "token_string",
            RefreshToken = "refresh_token_string"
        });
    }
}
