using System.ComponentModel.DataAnnotations;
using Silvernet.Validators;

namespace Silvernet.Models
{
    public class Tenant
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
        
        public string Name { get; set; }
            
        public string Email { get; set; }

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

        public DateTime CreationDate { get; set; }

        // nav prop
        public ICollection<User> Users { get; set; }
    }
}
