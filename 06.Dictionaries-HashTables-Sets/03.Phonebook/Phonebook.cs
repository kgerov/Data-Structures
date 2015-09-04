using DDS = DictionaryDataStructure;
using System;
using System.Collections.Generic;

namespace _03.Phonebook
{
    class Phonebook
    {
        private const string SearchKeyWord = "search";

        static void Main()
        {
            List<string> searchList = new List<string>();
            DDS.Dictionary<string, string> contacts = new DDS.Dictionary<string, string>();
            string inputLine = Console.ReadLine();
            bool shouldAddContactsToSearch = false;

            while (inputLine != "")
            {
                if (inputLine == SearchKeyWord)
                {
                    shouldAddContactsToSearch = true;
                }
                else
                {
                    if (shouldAddContactsToSearch)
                    {
                        searchList.Add(inputLine);
                    }
                    else
                    {
                        string[] tokens = inputLine.Split('-');
                        string name = tokens[0]; // contact name always comes first
                        string phoneNumber = tokens[1]; // phone number always comes second

                        contacts.Add(name, phoneNumber);
                    }
                }

                inputLine = Console.ReadLine();
            }

            foreach (var contactName in searchList)
            {
                var contact = contacts.Find(contactName);

                if (contact != null)
                {
                    Console.WriteLine(contact);
                }
                else
                {
                    Console.WriteLine("Contact {0} does not exist.", contactName);
                }
            }
        }
    }
}
