using Auth1.Core;

namespace Auth1.Models
{
    public class TableMetaData
    {
        public int Rows { get; set; }
        public int First { get; set; }
        public int? SortOrder { get; set; }
        public Filters? Filters { get; set; }
       // public object GlobalFilter { get; set; }
    }   
    public class Filters
    {
        public Name? Name { get; set; }
        public Gender? Gender { get; set; }
        public DateOfBirth? DateOfBirth { get; set; }
        public Passport? Passport { get; set; }
        public Region? Region { get; set; }
        public MobilePhone? MobilePhone { get; set; }
    }

    public class DateOfBirth
    {
        public DateTime? Value { get; set; }
        public string? MatchMode { get; set; }
    }

    public class Gender
    {
        public string? Value { get; set; }
        public string? MatchMode { get; set; }
    }

    public class MobilePhone
    {
        public string? Value { get; set; }
        public string? MatchMode { get; set; }
    }

    public class Name
    {
        public string? Value { get; set; }
        public string? MatchMode { get; set; }
    }

    public class Passport
    {
        public string? Value { get; set; }
        public string? MatchMode { get; set; }
    }

    public class Region
    {
        public string? Value { get; set; }
        public string? MatchMode { get; set; }
    }
}
