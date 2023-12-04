using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Auth1.Core.Entity;
using System.Reflection;

namespace Auth1.Data
{
    public static class IdentityContextSeed
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.SeedAuths();
        }
        private static void SeedAuths(this ModelBuilder builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>(users =>
            {
                users.HasData(new ApplicationUser
                {
                    Id = 1,
                    FirstName = "Alex",
                    SecondName  = "Sanches",
                    MidleName = "Alex",
                    PassportSerie = "AA",
                    PassportNumber = "2653478",
                    Region = "Tashkent",
                    JShShIR = "123456789102",
                    DateOfBirth = DateTime.Now,
                    Gender = "Male",
                    MobilePhone  = "+998901234567",
                    HomePhone = null,
                    Email = "asadbek",
                    UserName = "asadbek",
                    NormalizedUserName="asadbek",
                    PasswordHash = hasher.HashPassword(null, "qwerty3012")
                });;
            });
        }
    }
}
