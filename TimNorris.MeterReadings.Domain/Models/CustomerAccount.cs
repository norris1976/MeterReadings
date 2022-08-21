using System.ComponentModel.DataAnnotations;

namespace TimNorris.MeterReadings.Domain.Models
{
    public class CustomerAccount
    {
        public CustomerAccount()
        {

        }

        public CustomerAccount(int id, string? firstName, string? lastName)
        {
            AccountId = id;
            FirstName = firstName;
            LastName = lastName;
        }

        [Key]
        public int AccountId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}