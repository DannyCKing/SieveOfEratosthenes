using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieveOfEratosthenes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number to figure out if it is prime");

            bool continueProgram = true;

            while (continueProgram)
            {
                bool isInputValid = false;

                int inputNumber = 0;
                while (!isInputValid)
                {
                    Console.WriteLine("Enter a valid number-");
                    string inputNumberStr = Console.ReadLine();
                    try
                    {
                        inputNumber = int.Parse(inputNumberStr);
                        isInputValid = true;
                    }
                    catch (Exception e)
                    {
                        isInputValid = false;
                    }
                }

                //for (int i = 0; i < inputNumber; i++ )
                //{
                //    var isPrimeResult = data.IsNumberPrime(i);

                //    if(isPrimeResult)
                //    {
                //        Console.WriteLine(i + ": " + isPrimeResult);
                //    }
                //    Console.WriteLine(i + ": " + isPrimeResult);
                //}

                var result = PrimeNumberUtility.IsNumberPrime(inputNumber);
                Console.WriteLine(string.Format("{0} is {1}prime", inputNumber, result ? "" : "NOT "));
            }
        }
    }
}
