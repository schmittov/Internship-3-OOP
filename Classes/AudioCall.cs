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
        public static Enum AudioCallStatusChooser()
        {
            Random randomCallStatus = new Random();
            int randomCallStatusNumber = randomCallStatus.Next(0, 2);
            
            if (randomCallStatusNumber == 0) 
            {
                return AudioCallStatus.Traje;
            }
            else 
            {
                return AudioCallStatus.Propusten;
            }
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
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void AllAudioCallsPrint(List<Contact> contacts, List<AudioCall> audioCalls)
        {
            Console.WriteLine("|  Ime    | Prezime   |   Broj    |   Vrijeme spajanja  |  Trajanje |   Status  |");
            Console.WriteLine("|---------|-----------|-----------|---------------------|-----------|-----------|");

            foreach (var audioCall in audioCalls)
            {
                if ((audioCall.CallConnectionTime.Add(audioCall.Duration)) < DateTime.Now)
                    if(audioCall.AudioCallStatuses==AudioCallStatus.Traje)
                        audioCall.AudioCallStatuses = AudioCallStatus.Zavrsen;
                
                Console.WriteLine($"| {audioCall.Contact.FirstName,-7} | {audioCall.Contact.LastName,-9} | {audioCall.Contact.MobilePhone,-9} | {audioCall.CallConnectionTime,-15} | {audioCall.Duration,-8} | {audioCall.AudioCallStatuses,-9} |");
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
                        var newCallStatus = (AudioCallStatus)AudioCallStatusChooser();
                        
                        Random randomDuration = new Random();
                        int randomDurationNumber = randomDuration.Next(1, 21);
                        TimeSpan randomDurationTime = TimeSpan.FromSeconds(randomDurationNumber);
                        DateTime newCallConnectionTime = DateTime.Now;
                        
                        if(newCallStatus==AudioCallStatus.Propusten)
                            randomDurationTime = TimeSpan.FromSeconds(0);

                        AudioCall theMostNewCall = new AudioCall(newCallConnectionTime, randomDurationTime, newCallStatus, callingContact);
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
