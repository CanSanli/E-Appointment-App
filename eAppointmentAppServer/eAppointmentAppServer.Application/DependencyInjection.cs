using Microsoft.Extensions.DependencyInjection;

namespace eAppointmentAppServer.Application
{
    //REquest response ve handle ayrı ayrı classlarda oluşturuluyor. WebAPI request ve response classlarını bilir fakat handle olan (asıl işlemi gerçekleştiren) classı bilmez.
    //Bu kısmı DependencyInjection class'ında yönetiyoruz. Requesti alır, Requestin bağlı olduu handle class'ını bulur, işlemi yaptırır ve dönüş verir.
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            
            //mediatr bu katmandaki yapılara / verilere erişmebilir.
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);   //mediatr kütüphanesini başka bir katmanda da kullanırsak buradaki metodu çoğul yapıp o katmanın assembly'ini de buraya vermeliyiz.
            });
            return services;
        }

    }
}
  