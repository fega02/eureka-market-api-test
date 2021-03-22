using System.ComponentModel.DataAnnotations;

namespace AWSMarketAPI.Models
{
    public class SignupRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
