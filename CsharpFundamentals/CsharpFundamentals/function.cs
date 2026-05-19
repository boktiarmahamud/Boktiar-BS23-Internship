using System;

namespace CsharpFundamentals
{
    internal class Function
    {
        public static void Main(string[] args)
        {
            int result = AddNumbers(10, 20, 30);

            Console.WriteLine("Result: " + result);
        }

        static int AddNumbers(int firstNumber, int secondNumber, int thirdNumber)
        {
            return firstNumber + secondNumber + thirdNumber;
        }
    }
}