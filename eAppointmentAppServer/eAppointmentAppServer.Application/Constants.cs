using eAppointmentAppServer.Domain.Entities;

namespace eAppointmentAppServer.Application
{
    public static class Constants
    {
        public static List<AppRole> GetRoles()
        {
            List<string> roles = new()
            {
                "Admin",
                "Doctor",
                "Personel",
                "Patient"
            };
            return roles.Select(s => new AppRole() { Name=s}).ToList();
        }
        //public static List<AppRole> Roles = new()
        //{
        //    new()
        //    {

        //    Name="Admin"
        //    },
        //    new()
        //    {

        //    Name="Doctor"
        //    },
        //    new()
        //    {

        //    Name="Personel"
        //    },

        //};
    }
}






