namespace Silvernet.DTOs
{
    public class UserCreateDTO
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public long TenantId { get; set; }
    }
}
