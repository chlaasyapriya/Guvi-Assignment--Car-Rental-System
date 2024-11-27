using Assignment__Car_Rental_System.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace Assignment__Car_Rental_System.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string key;
        public JwtMiddleware(RequestDelegate next, IConfiguration iconfiguration)
        {
            _next = next;
            key = iconfiguration["JWT:Key"];
        }
        public async Task Invoke(HttpContext context,IUserRepo userRepo)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if(token!=null)
                AttachUserToContext(context, userRepo, token);
            await _next(context);
        }
        private void AttachUserToContext(HttpContext context, IUserRepo userRepo, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var _key = Encoding.UTF8.GetBytes(key);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidateIssuer=false,
                    ValidateAudience=false,
                    ClockSkew=TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var username = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
                context.Items["User"] = userRepo.GetUserByEmail(username);
            }
            catch
            {

            }
        }
    }
}
