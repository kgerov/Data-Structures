using System;

namespace _02.StringEditor
{
    class StringEditor
    {
        static void Main(string[] args)
        {
            
            string input = Console.ReadLine();

            while (input != "END")
            {
                string[] tokens = input.Split(' ');

                switch (input)
                {
                    case "APPEND":
                        break;
                    case "INSERT":
                        break;
                    case "DELETE":
                        break;
                    case "PRINT":
                        break;
                    case "REPLACE":
                        break;
                }

                input = Console.ReadLine();
            }
        }
    }
}
