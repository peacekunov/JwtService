using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtService {
    public class JwtService : IJwtService {

        private const string SIGNING_ALGORITHM = SecurityAlgorithms.HmacSha256;

        private Dictionary<string, JwtConfiguration> Configs { get; }

        public JwtService() {
            Configs = new Dictionary<string, JwtConfiguration>();
        }

        public void AddConfiguration(JwtConfiguration jwtConfiguration) {
            Configs.Add(jwtConfiguration.Name, jwtConfiguration);
        }

        public SymmetricSecurityKey GetSecretKey(string configName) {
            return Configs[configName].SecretKey;
        }

        public string GenerateToken(string configName, Claim[] claims) {
            JwtConfiguration config = Configs[configName];
            SigningCredentials signingCredentials = new SigningCredentials(config.SecretKey, SIGNING_ALGORITHM);
            JwtSecurityToken token = new JwtSecurityToken(
                config.Issuer,
                config.Audience,
                claims,
                DateTime.Now,
                DateTime.Now.AddHours(config.TokenLifeTimeHours),
                signingCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
