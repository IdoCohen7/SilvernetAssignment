using System.ComponentModel.DataAnnotations;

namespace Silvernet.DTOs
{
    public class UserCreateDTO
    {
        [Required]
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public long TenantId { get; set; }
    }
}
