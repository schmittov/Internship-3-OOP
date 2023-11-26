using Domaci_3.Classes;

namespace Domaci_3.Functions.FunctionsMenu
{
        public class Methode
        {
            public static void PrintSubMenuOptions()
            {
                Console.Write("1. Ispis svih poziva\n");
                Console.Write("2. Kreiranje novog poziva\n");
                Console.Write("3. Izlaz iz podmenua\n");
            }
        public static void PrintMenuOptions()
        {
                Console.WriteLine("1. Ispis svih kontakata");
                Console.WriteLine("2. Dodavanje novih kontakata u imenik");
                Console.WriteLine("3. Brisanje kontakata iz imenika");
                Console.WriteLine("4. Editiranje preference kontakta");
                Console.WriteLine("5. Upravljanje kontaktom");
                Console.WriteLine("6. Ispis svih poziva");
                Console.WriteLine("7. Izlaz iz aplikacije");
        }
        public static void SelectMenuOption(List<Contact> contacts,List<AudioCall> audioCalls, int x)
        {
            if (x == 1) Contact.PrintContacts(contacts);
            else if (x == 2) Contact.AddNewContact(contacts);
            else if (x == 3) Contact.DeleteContact(contacts);
            else if (x == 4) Contact.EditContactPreference(contacts);
            else if (x == 5) PrintSubMenuOptions();
            else if (x == 6) Environment.Exit(0);  
        }

        public static void SelectSubMenuOption(List<Contact> contacts, List<AudioCall> audioCalls, int x)
        {
            if (x == 1) AudioCall.AllAudioCallsPrint(contacts, audioCalls);
            else if (x == 2) AudioCall.MakeACall(contacts, audioCalls);
            else if (x == 3) { }//ne napravi ništa da bi se došlo do breaka direktno (izašlo iz podmenua)

        }
    } 
}
