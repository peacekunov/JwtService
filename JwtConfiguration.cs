using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtService {
    public class JwtConfiguration {
        public string Name;
        public string Issuer;
        public string Audience;
        public int TokenLifeTimeHours;
        public SymmetricSecurityKey SecretKey;

        public JwtConfiguration(string name, string signingSecurityKey, string issuer, string audience, int tokenLifeTimeHours) {
            Name = name;
            Issuer = issuer;
            Audience = audience;
            TokenLifeTimeHours = tokenLifeTimeHours;
            SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingSecurityKey));
        }
    }
}
