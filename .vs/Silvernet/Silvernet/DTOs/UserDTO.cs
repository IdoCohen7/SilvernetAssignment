namespace Silvernet.DTOs
{
    public class UserDTO
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime CreationDate { get; set; }

        public long TenantId { get; set; }
    }
}
