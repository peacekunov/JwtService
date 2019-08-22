using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CrashItServer.Extensions.Jwt {
    public interface IJwtService {

        void AddConfiguration(JwtConfiguration jwtConfiguration);

        SymmetricSecurityKey GetSecretKey(string configName);

        string GenerateToken(string configName, Claim[] claims);

    }
}
