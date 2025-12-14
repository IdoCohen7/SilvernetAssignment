using System.ComponentModel.DataAnnotations;

namespace Silvernet.Models
{
    public class Tenant
    {
        public long Id { get; set; }
        public string Name { get; set; }
            
        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime CreationDate { get; set; }

        // nav prop

        public ICollection<User> Users { get; set; }
    }
}
