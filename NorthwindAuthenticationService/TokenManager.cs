using Microsoft.IdentityModel.Tokens;
using NorthwindModelClassLibrary;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NorthwindAuthenticationService
{
    public static class TokenManager
    {
        const string UserId = "UserId";
        const string RoleName = "RoleName";
        public static string GenerateWebToken(UserModel model, AppSettings settings)
        {
            //create a list of claims
            var claimsSet = new List<Claim>
            {
                new Claim(UserId, model.UserId.ToString()),
                new Claim(RoleName, model.RoleName.ToString())
            };
            //create an identity based on claim set
            var userIdentity = new ClaimsIdentity(claimsSet);
            var keyBytes = Encoding.UTF8.GetBytes(settings.AppSecret);
            var signInCredentials = new SigningCredentials(
                key: new SymmetricSecurityKey(keyBytes),
                algorithm: settings.Algorithm
                );

            
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = userIdentity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signInCredentials
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var preToken = handler.CreateToken(descriptor); 
            var writeableToken = handler.WriteToken(preToken);
            return writeableToken;
        }
        public static UserModel GetUserFromToken(string token, AppSettings settings, IUserServiceAsync service)
        {
            var keyBytes = Encoding.UTF8.GetBytes(settings.AppSecret);
            var signInCredentials = new SigningCredentials(
                key: new SymmetricSecurityKey(keyBytes),
                algorithm: settings.Algorithm
                );

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew =TimeSpan.Zero,
                
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(
                token: token,
                validationParameters:  validationParameters,
                validatedToken: out SecurityToken validatedToken
                );

            var outputToken = validatedToken as JwtSecurityToken;
            var userId = outputToken.Claims.FirstOrDefault(c=>c.Type==UserId)?.Value;
            //var roleName = outputToken.Claims.FirstOrDefault(c => c.Type == RoleName)?.Value;

            //discard variable is denoted with underscore
            _ = outputToken.Claims.FirstOrDefault(c => c.Type == RoleName)?.Value;

            var user = service.GetUserDetails(Convert.ToInt32(userId)).Result;
            return user;
        }
    }
}
