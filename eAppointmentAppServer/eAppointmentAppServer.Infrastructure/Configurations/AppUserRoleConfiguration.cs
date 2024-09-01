using eAppointmentAppServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointmentAppServer.Infrastructure.Configurations
{
    internal class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
          builder.HasKey(k=> new {k.UserId,k.RoleId});

        }
    }
}
