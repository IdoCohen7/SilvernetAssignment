using System.ComponentModel.DataAnnotations;

namespace Silvernet.DTOs
{
    public class TenantCreateDTO
    {
        [Required]
        public long Id { get; set; }
        
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
