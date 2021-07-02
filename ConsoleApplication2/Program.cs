using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Commands cmd = new Commands();
            Console.WriteLine(Commands.InfoText);
            string input = string.Empty;
            Console.Write(">");
            while ((input = Console.ReadLine()).ToLower() != "exit")
            {
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input is empty");
                    Console.WriteLine(">");
                    continue;
                }

                string result = cmd.ExecuteCommand(input);
                if (!string.IsNullOrEmpty(result))
                    Console.WriteLine(result);

                Console.Write(">");
            }
        }
    }
}
