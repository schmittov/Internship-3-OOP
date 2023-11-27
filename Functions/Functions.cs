using System.Text.RegularExpressions;

namespace Domaci_3.Functions
{
    public class Functions
    {
        public static string NotNull(string stringVariable)
        {
            do
            {
                stringVariable = Console.ReadLine();
            } 
            while (string.IsNullOrEmpty(stringVariable));

            return stringVariable;
        }
        public static string GetUserInput(string message)
        {
            string input;

            do
            {
                Console.Write(message);
                input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(input))
                    break;
                else
                    Console.Clear();
            } while (true);

            return input;
        }
        public static bool ConfirmTheAction()
        {
            string confirm;
            confirm = GetUserInput("Napišite 'Da' za potvrdu ili 'Ne' za odbijanje izvršavanja.\n");
            if (confirm.ToLower().Trim() == "da")
                return true;
            else
                return false;
        }
        public static bool IsPhoneNumberFormatCorrect(string phoneNumber) //nađeno na stackoverflow-u ne znam bili ovo spadalo pod neobrađeno gradivo
        {
            string pattern = @"^\d{3}-\d{4}$";

            if (Regex.IsMatch(phoneNumber, pattern)) 
            {
                return true;
            }
            else
            {
                Console.WriteLine("Neispravan format broja.");
                return false;
            }
        }
        
    }
}
