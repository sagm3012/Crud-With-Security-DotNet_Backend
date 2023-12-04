using Auth1.Core;

namespace Auth1.Models
{
    public class ApplicationUserDTO
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
        public string? UserName { get; set; }
        //  [JsonIgnore]
        public string? PasswordHash { get; set; }
    }
}
