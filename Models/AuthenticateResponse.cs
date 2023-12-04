using Auth1.Core;
using Auth1.Core.Entity;
using System.Net.Mail;

namespace Auth1.Models
{
    public class AuthenticateResponse
    {
        public int? Id { get; set; }

        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? MidleName { get; set; }
        public string? PassportSerie { get; set; }
        public string? PassportNumber { get; set; }
        public string? Region { get; set; }
        public string? JShShIR { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? MobilePhone { get; set; }
        public string? HomePhone { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }

        public AuthenticateResponse(ApplicationUser user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            SecondName= user.SecondName;
            MidleName = user.MidleName;
            PassportSerie = user.PassportSerie;
            PassportNumber = user.PassportNumber;
            Region = user.Region;
            JShShIR = user.JShShIR;
            DateOfBirth = user.DateOfBirth;
            Gender = user.Gender;
            MobilePhone = user.MobilePhone;
            HomePhone = user.HomePhone;
            Email = user.Email;                             
            Token = token;
        }
    }
}
