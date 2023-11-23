using Domaci_3.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domaci_3.Classes
{
    internal class Contact
    {
        public Guid Id { get; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        [Phone] public string MobilePhone { get; set; }
        public ContactPreference Preference { get; set; }



        public Contact(String firstName, String lastName, [Phone] string mobilePhone,ContactPreference contactPreference)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;    
            MobilePhone = mobilePhone;
            Preference = contactPreference;
        }

        public static bool CheckIsBlocked(ContactPreference preference)
        {
            return (preference != ContactPreference.Blokiran);
        }
    }
}
