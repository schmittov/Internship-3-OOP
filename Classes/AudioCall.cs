using Domaci_3.Enums;

namespace Domaci_3.Classes
{
    public class AudioCall
    {
        public Guid Id { get; }
        public DateTime CallConnectionTime { get; set; }
        public TimeSpan Duration { get; set; }
        public AudioCallStatus AudioCallStatuses { get; set; }
        public Contact Contact { get; set; }

        public AudioCall(DateTime callConnectionTime, TimeSpan duration, AudioCallStatus audioCallStatuses, Contact contact)
        {
            Id = Guid.NewGuid();
            CallConnectionTime = callConnectionTime;
            Duration = duration;
            AudioCallStatuses = audioCallStatuses;
            Contact = contact;
        }

        public static bool CheckForOtherCallsInProgress(List<AudioCall> audioCalls)
        {
            int i=0;
            foreach(AudioCall audioCall in audioCalls) 
            { 
                if((audioCall.CallConnectionTime.Add(audioCall.Duration)) > DateTime.Now)
                {
                    i++;
                } 
            }
            if (i > 0)
            {
                Console.WriteLine("Nemoze");
                return false;
            }
            else
            {
                Console.WriteLine("Moze");
                return true;
            }
        }
        public static void AllAudioCallsPrint(List<Contact> contacts, List<AudioCall> audioCalls)
        {
            Console.WriteLine("|  Ime    | Prezime   |   Broj    |   Vrijeme spajanja  | Trajanje |");
            Console.WriteLine("|---------|-----------|-----------|---------------------|----------|");

            foreach (var audioCall in audioCalls)
            {              
                Console.WriteLine($"| {audioCall.Contact.FirstName,-7} | {audioCall.Contact.LastName,-9} | {audioCall.Contact.MobilePhone,-9} | {audioCall.CallConnectionTime,-15} | {audioCall.Duration,-8} |");
            }
            Console.WriteLine("Pritisnite bilo koju tipku za nastaviti.");
            Console.ReadKey();
        }

        public static void ChoosenContactAllAudioCallsPrint(List<Contact> contacts, List<AudioCall> audioCalls)
        {
            Console.WriteLine("Odaberite kontakt");
            Contact.PrintContacts(contacts);

            string firstName = Functions.Functions.GetUserInput("Unesi ime kontakta: ").ToLower();
            string lastName = Functions.Functions.GetUserInput("Unesi prezime kontakta: ").ToLower();
            Contact choosenContact = contacts.Find(contact => (contact.FirstName + contact.LastName).ToLower() == (firstName + lastName));
            var sortedCalls = audioCalls.OrderBy(call => call.CallConnectionTime).ToList();
            foreach (var sortedCall in sortedCalls)
            {
                if (choosenContact == sortedCall.Contact)
                {
                    Console.WriteLine($"| {sortedCall.Contact.FirstName,-7} | {sortedCall.Contact.LastName,-9} | {sortedCall.Contact.MobilePhone,-9} | {sortedCall.CallConnectionTime,-15} | {sortedCall.Duration,-8} |");

                }
            }
        }


        public static void MakeACall(List<Contact> contacts, List<AudioCall> audioCalls)
        {
            Contact.PrintContacts(contacts);

            Console.WriteLine("Unesi ime i prezime kontakta kojeg želiš nazvat");
            string firstName = Functions.Functions.GetUserInput("Unesi ime kontakta: ").ToLower();
            string lastName = Functions.Functions.GetUserInput("Unesi prezime kontakta: ").ToLower();

            
            Contact callingContact = contacts.Find(contact => (contact.FirstName + contact.LastName).ToLower() == (firstName + lastName));
            
            if(callingContact != null)
            {
                if (Contact.CheckIsNotBlocked(callingContact))
                {
                    if (!CheckForOtherCallsInProgress(audioCalls))
                    {
                        Console.WriteLine("Drugi poziv je u tijeku, pokušajte kasnije.");
                        Console.WriteLine("Pritisnite bilo koju tipku za nastaviti.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Random random = new Random();
                        int randomDurationNumber = random.Next(1, 21);
                        TimeSpan randomDurationTime = TimeSpan.FromSeconds(randomDurationNumber);
                        DateTime newCallConnectionTime = DateTime.Now;


                        AudioCall theMostNewCall = new AudioCall(newCallConnectionTime, randomDurationTime, AudioCallStatus.Zavrsen, callingContact);
                        audioCalls.Add(theMostNewCall);
                        Console.WriteLine("Novi poziv stvoren.");
                        Console.WriteLine("Pritisnite bilo koju tipku za nastaviti.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Kontakt je blokiran, odblokiraj te ga");
                    Console.WriteLine("Pritisnite bilo koju tipku za nastaviti.");
                    Console.ReadKey();

                }
            }
            else
            {
                Console.WriteLine("Kontakt ne postoji");
                Console.WriteLine("Pritisnite bilo koju tipku za nastaviti.");
                Console.ReadKey();
            }
            
            
        }
    }
}
