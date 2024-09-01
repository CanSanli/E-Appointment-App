using eAppointmentAppServer.Domain.Entities;
using eAppointmentAppServer.Infrastructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace eAppointmentAppServer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });

            services.AddIdentity<AppUser,AppRole>(action =>  //password kuralları
            {
                action.Password.RequiredLength = 1;
                action.Password.RequireUppercase = false;
                action.Password.RequireLowercase = false;
                action.Password.RequireNonAlphanumeric = false;
                action.Password.RequireDigit = false; 
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

            //Normalde kullanacağımız tüm servisleri bu şekilde Interface ve classları belirtmemiz gerekiyor. 100 tane servis olsa 10 satır kod yazıcaz
            //Bunu daha basite indirgemek adına Structor kütüphanesini kullandık. Services.scan fonksiyonu içerisinde verdiğimiz özellikler doğrultusunda
            //Kullanılan servisleri tespit ederek otomatik olarak kendisi ayarlıyor.
           
            //------------------ Bu kullanıma gerek kalmadı----------
            //services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            //services.AddScoped<DoctorRepository, DoctorRepository>();
            //services.AddScoped<PatientRepository, PatientRepository>();

            //services.AddScoped<IJwtProvider, JwtProvider>();
            //-------------------------------------------------------
           
            services.Scan(action =>   //Interface ve class'ın isimleri aynı olduğu sürece ("I" önemsiz) bu fonksiyon çalışır.
            {
                action
                .FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(registrationStrategy: RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });

           
            return services;
        }
    }
}
