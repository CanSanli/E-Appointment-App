using eAppointmentAppServer.Application.Services;
using eAppointmentAppServer.Domain.Entities;
using eAppointmentAppServer.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace eAppointmentAppServer.Infrastructure.Services
{
    internal sealed class JwtProvider(
        IConfiguration configuration,
        IUserRoleRepository userRoleRepository,
        RoleManager<AppRole> roleManager
        ) : IJwtProvider
    {
        public async Task<string> CreateTokenAsync(AppUser user)
        {
            //Kullanıcının rollerini çekip tokena ekliyoruz
            List<AppUserRole> appUserRoles = await userRoleRepository.Where(p => p.UserId == user.Id).ToListAsync();
            List<AppRole> roles = new();

            foreach (var userRole in appUserRoles)
            {
                AppRole? role = await roleManager.Roles.Where(p => p.Id == userRole.RoleId).FirstOrDefaultAsync();
                if (role is not null)
                {
                    roles.Add(role);  //rolleri çektim
                }
            }
            List<string?> stringRoles = roles.Select(s => s.Name).ToList(); //serialize edebilmek için rolleri stringe çevirdim

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FullName),
                new Claim(ClaimTypes.Email,user.Email ?? string.Empty),    // ??=> önceki değer null gelirse sonraki değeri (string.empty) döndür
                new Claim("UserName",user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Role, JsonSerializer.Serialize(stringRoles)) //serialize
            };
            DateTime expires = DateTime.Now.AddDays(1);

            SymmetricSecurityKey securityKey = 
                new(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:SecretKey").Value ?? "")); //3 random id birleştirerek 1 key oluşturduk daha uzun ve komplex olabilir
            SigningCredentials signingCredentials = new(securityKey,SecurityAlgorithms.HmacSha512); //key'i alıp şifreleyip signingcredentials'e dönüştürmek için HmacSha512 algoritmasını kullandık

            

            JwtSecurityToken jwtSecurityToken = new(
                issuer: configuration.GetSection("Jwt:Issuer").Value,  //uygulama kimin
                audience: configuration.GetSection("Jwt:Audience").Value,  //kim kullanacak
                claims: claims,
                notBefore: DateTime.Now,    //token ne zamandan sonra kullanılsın (10dk sonra vs)  //oluşturulduğundan itibaren kullanılsın
                expires: expires,          //token ne zaman sonlansın
                signingCredentials:signingCredentials         //uygulamanın şifreleme türü, anahtarı vs.
                );
            JwtSecurityTokenHandler handler = new();
            string token = handler.WriteToken(jwtSecurityToken);
            return token;
            
        }
    }
}
