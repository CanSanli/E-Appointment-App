using eAppointmentAppServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace eAppointmentAppServer.WebAPI
{
    public  static class CreateUserAsync
    {
        public static async Task CreateUser(WebApplication app)
        {

            using (var scoped = app.Services.CreateScope())  //hiç kullanıcı yoksa oluşturması için
            {
                var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                if (!userManager.Users.Any())
                {
                    await userManager.CreateAsync(new()
                    {
                        FirstName = "Can",
                        LastName = "Sanli",
                        Email = "admin@admin.com",
                        UserName = "admin",

                    }, "1");   //"1" : password
                }
            }
        }
    }
}
