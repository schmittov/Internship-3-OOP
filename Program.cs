﻿using Domaci_3.Classes;
using Domaci_3.Enums;
using Domaci_3.Functions.FunctionsMenu;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;

int menuSelector;
List<Contact> contacts = new()
{
    new Contact("John",     "Doe",      "555-1234", ContactPreference.normalan),
    new Contact("Jane",     "Smith",    "555-5678", ContactPreference.normalan),
    new Contact("Mike",     "Johnson",  "555-9876", ContactPreference.favorit),
    new Contact("Emily",    "Smith",    "555-4321", ContactPreference.normalan),
    new Contact("Alex",     "Brown",    "555-8765", ContactPreference.normalan),
    new Contact("Sara",     "Miller",   "555-1111", ContactPreference.favorit),
    new Contact("David",    "Jones",    "555-2222", ContactPreference.normalan),
    new Contact("Emma",     "Davis",    "555-3333", ContactPreference.normalan),
    new Contact("Daniel",   "Wilson",   "555-4444", ContactPreference.blokiran),
    new Contact("Olivia",   "Moore",    "555-5555", ContactPreference.favorit)
};
List<AudioCall> audioCalls = new()
{
    new AudioCall(DateTime.Now.AddSeconds(-20), TimeSpan.FromSeconds(5), AudioCallStatus.Zavrsen, contacts[0]),
    new AudioCall(DateTime.Now.AddSeconds(-40), TimeSpan.FromSeconds(10), AudioCallStatus.Propusten, contacts[1]),
    new AudioCall(DateTime.Now.AddSeconds(-100), TimeSpan.FromSeconds(7), AudioCallStatus.Zavrsen, contacts[2])

};
do
{
    AudioCall.AudioCallListPrint(contacts, audioCalls);
    Console.ReadKey();
    AudioCall.MakeACall(contacts, audioCalls);
    Console.ReadKey();

    Console.Clear();
    do
    {
        Methode.PrintMenuOptions();
        Console.Write("\nOdaberi opciju: ");
        if (int.TryParse(Console.ReadLine(), out menuSelector) && menuSelector >= 0 && menuSelector <= 6)
            break;
        else if(menuSelector==7)
            Environment.Exit(0);
        else
            Console.Clear();
    } while (true);

    Contact.SelectMenuOption(contacts, menuSelector);

    Console.WriteLine("Klikni enter");
    Console.ReadKey();
} while(true);

