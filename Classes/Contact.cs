﻿using Domaci_3.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domaci_3.Classes
{
    public class Contact
    {
        public Guid Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public ContactPreference Preference { get; set; }


        public Contact(string firstName, string lastName, [Phone] string mobilePhone, ContactPreference contactPreference)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            MobilePhone = mobilePhone;
            Preference = contactPreference;
        }
        public static bool CheckIsNotBlocked(Contact contact)
        {
            return (contact.Preference != ContactPreference.blokiran);
        }

        public static void PrintContacts(List<Contact> contacts)
        {
            Console.Clear();
            Console.WriteLine("| Ime     | Prezime   | Broj telefona | Preference |");
            Console.WriteLine("|---------|-----------|---------------|------------|");
            foreach (var contact in contacts)
            {
                Console.WriteLine($"| {contact.FirstName,-7} | {contact.LastName,-9} | {contact.MobilePhone,-13} | {contact.Preference,-10} |");
            }

            Console.WriteLine("Pritisnite bilo koju tipku za nastaviti.");
            Console.ReadKey();
            
        }

        public static void AddNewContact(List<Contact> contacts) //potencijalno dodaj bolje uvjete za unos podataka
        {
            Console.Clear();
            Console.WriteLine("Kreiranje novog kontakta");
            bool checkIfNumbersAreSame = false;
            string firstName, lastName, newPreference, phoneNumber;
            firstName = Functions.Functions.GetUserInput("Unesi ime: ");
            lastName = Functions.Functions.GetUserInput("Unesi prezime: ");
            phoneNumber = Functions.Functions.GetUserInput("Unesi broj: ");
            newPreference = Functions.Functions.GetUserInput("Unesi preferencu: ");

            Enum.TryParse<ContactPreference>(newPreference, out ContactPreference novaPreferenca);
                
            foreach (Contact contact in contacts)
            {
                if (phoneNumber.Equals(contact.MobilePhone))
                    checkIfNumbersAreSame = true;   
            }
            if (Functions.Functions.ConfirmTheAction())
            {
                if (!checkIfNumbersAreSame)
                    contacts.Add(new Contact(firstName, lastName, phoneNumber, novaPreferenca));
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Odustali ste od radnje.");
            }
            
        }
        public static void DeleteContact(List<Contact> contacts)
        {
            PrintContacts(contacts);
            Console.WriteLine("Odaberite kontakt za brisanje.");
            string firstName, lastName;
            firstName = Functions.Functions.GetUserInput("Unesi ime kontakta: ").ToLower();
            lastName = Functions.Functions.GetUserInput("Unesi prezime kontakta: ").ToLower();
            Contact deletedContact = contacts.Find(contact => (contact.FirstName + contact.LastName).ToLower() == (firstName + lastName));

            Console.Clear();
            if (Functions.Functions.ConfirmTheAction())
            {
                if (deletedContact != null)
                {
                    contacts.Remove(deletedContact);
                    Console.WriteLine("Ažurirani kontakti.");
                    PrintContacts(contacts);
                }
                else
                {
                    Console.WriteLine("Ne postoji takav kontakt.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Odustali ste od radnje.");
            }
        }
        public static void EditContactPreference(List<Contact> contacts)
        {
            PrintContacts(contacts);
            Console.Write("\nOdaberite kontakt za urediti.");


            string firstName, lastName, newPreference;
            firstName = Functions.Functions.GetUserInput("Unesi ime kontakta: ").ToLower();
            lastName = Functions.Functions.GetUserInput("Unesi prezime kontakta: ").ToLower();
            newPreference = Functions.Functions.GetUserInput("Unesi preferencu:: ").ToLower();

            if (Functions.Functions.ConfirmTheAction())
            {
                if (Enum.TryParse<ContactPreference>(newPreference, out ContactPreference enumPreference))
                {
                    foreach (Contact contact in contacts)
                    {
                        if ((contact.FirstName + contact.LastName).ToLower() == (firstName + lastName))
                        {
                            contact.Preference = enumPreference;
                            Console.WriteLine("Preferenca uspješno ažurirana.");
                        }
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Odustali ste od radnje.");
            } 
        }
        
    }
}
