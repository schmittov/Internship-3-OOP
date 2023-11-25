using Domaci_3.Enums;
using Domaci_3.Classes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Domaci_3.Classes
{
    internal class AudioCall
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

        public static void AudioCallStatusChanger(DateTime callConnectionTime, TimeSpan duration)
        {

            if (callConnectionTime + duration > DateTime.Now) { }
            else if (callConnectionTime + duration <= DateTime.Now) { }
        }

        public static bool CheckForOtherCallsInProgress(List<AudioCall> audioCalls)
        {
            int i=0;
            foreach(AudioCall audioCall in audioCalls) 
            { 
                if(audioCall.CallConnectionTime + audioCall.Duration < DateTime.Now)
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
        public static void AudioCallListPrint(List<Contact> contacts, List<AudioCall> audioCalls)
        {
            Console.WriteLine("| Ime     | Prezime   |   Broj    |   Vrijeme spajanja   | Trajanje |");
            Console.WriteLine("|---------|-----------|-----------|----------------------|----------|");
            foreach (AudioCall audioCall in audioCalls)
            {              
                Console.WriteLine($"| {audioCall.Contact.FirstName,-7} | {audioCall.Contact.LastName,-9} | {audioCall.Contact.MobilePhone,-9} | {audioCall.CallConnectionTime,-15} | {audioCall.Duration,-8} |");
            }
        }


        public static AudioCall MakeACall(List<Contact> contacts, List<AudioCall> audioCalls)
        {
            Contact.PrintContacts(contacts);

            Console.WriteLine("Unesi ime i prezime kontakta kojeg želiš nazvat");
            string firstName = Functions.Functions.GetUserInput("Unesi ime kontakta: ").ToLower();
            string lastName = Functions.Functions.GetUserInput("Unesi prezime kontakta: ").ToLower();
            Contact callingContact = contacts.Find(contact => (contact.FirstName + contact.LastName).ToLower() == (firstName + lastName));
            
            if (Contact.CheckIsNotBlocked(callingContact))
            {
                if (CheckForOtherCallsInProgress(audioCalls))
                {
                    return null;
                }
                else
                {
                    Random random = new Random();
                    int randomDurationNumber = random.Next(1, 21);
                    TimeSpan randomDurationTime= TimeSpan.FromSeconds(randomDurationNumber);
                    DateTime newCallConnectionTime = DateTime.Now;


                    AudioCall theMostNewCall = new AudioCall(newCallConnectionTime, randomDurationTime, AudioCallStatus.Zavrsen, callingContact);
                    Console.WriteLine("Poziv je uspješno stvoren.");
                    return theMostNewCall;

                }    
            }
            else
            {
                Console.WriteLine("Kontakt je blokiran");
                return null;
            }
        }
    }
}
