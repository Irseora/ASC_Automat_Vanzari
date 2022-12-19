using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat_Vanzari;

namespace ASC_Automat_Vanzari
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new Context(new StateA());

            while (true)
            {
                context.ShowUI();

                string insertedCoin = Console.ReadLine();
                switch (insertedCoin)
                {
                    case "N":
                    case "n":
                    case "Nickel":
                    case "nickel":
                    case "5c":
                        context.InsertNickel();
                        break;
                    case "D":
                    case "d":
                    case "Dime":
                    case "dime":
                    case "10c":
                        context.InsertDime();
                        break;
                    case "Q":
                    case "q":
                    case "Quarter":
                    case "quarter":
                    case "25c":
                        context.InsertQuarter();
                        break;
                    case "Stop":
                    case "stop":
                    case "Exit":
                    case "exit":
                    case "S":
                    case "s":
                        return;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid coin!");
                        Console.WriteLine("Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}