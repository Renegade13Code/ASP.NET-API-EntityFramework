using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webAPI.Models.Domain;

namespace webAPI.Repository
{
    public class MyTokenHandler : IMyTokenHandler
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepo;

        public MyTokenHandler(IConfiguration configuration, IUserRepository userRepo)
        {
            this.configuration = configuration;
            this.userRepo = userRepo;
        }

        public async Task<string> CreateToken(User user)
        {
            // Create claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress));

            // This was for staticUserRepo
            //user.Roles.ForEach(role =>
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //});

            var roles = await userRepo.GetRoles(user);

            if(roles != null)
            {
                roles.ToList().ForEach(role =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.name));
                });
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration.GetValue<string>("Jwt:Issuer"),
                configuration.GetValue<string>("Jwt:Audience"),
                claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: credentials
                );

            // From static implementation
            //return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
