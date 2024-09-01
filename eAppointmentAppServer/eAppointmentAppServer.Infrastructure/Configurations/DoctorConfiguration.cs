using eAppointmentAppServer.Domain.Entities;
using eAppointmentAppServer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointmentAppServer.Infrastructure.Configurations
{
    internal class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(p => p.FirstName).HasColumnType("varchar(50)");
            builder.Property(p => p.LastName).HasColumnType("varchar(50)");
            //builder.HasIndex(x => x.FirstName).IsUnique();  //burada bu şekilde düzenlemeler de yapabiliriz

            //migration oluştututken smartenum classı nasıl kaydedeceğini bilmediği için hata veriyordu.
            //hatayı düzeltmek için smartEnum classı ile oluşturduğum veriyi db'ye kaydederken value değeri ile kaydedip.
            //db'den çekerken value değerine göre tekrar SmartEnum objesine çevirmesini sağladık.
            builder.Property(p => p.Department)  
                .HasConversion(v => v.Value, v => DepartmentEnum.FromValue(v))
                .HasColumnName("Deparment");
        }
    }
}
