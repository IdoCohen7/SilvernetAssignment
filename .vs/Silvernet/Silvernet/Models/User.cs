using Silvernet.Validators;
using System.ComponentModel.DataAnnotations;

namespace Silvernet.Models
{
    public class User
    {
        private long _id;
        private string _phone;
        public long Id 
        { 
            get => _id;
            set
            {
                if (!AppValidator.IsValidIsraeliId(value))
                {
                    throw new ArgumentException("Invalid Israeli ID.", nameof(Id));
                }
                _id = value;
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone
        {
            get => _phone;
            set
            {
                if (!AppValidator.IsValidIsraeliPhoneNumber(value))
                {
                    throw new ArgumentException("Invalid Israeli Phone Number.", nameof(Phone));
                }
                _phone = value;
            }
        }

        public string Email { get; set; }

        public DateTime CreationDate { get; set; }

        public long TenantId { get; set; }

        // nav prop
        public Tenant Tenant { get; set; }
    }
}
