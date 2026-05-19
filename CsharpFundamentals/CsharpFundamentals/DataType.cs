using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpFundamentals
{
    internal class DataType
    {
        static void Main(string[] args)
        {
            byte age = 25;
            short temperature = -10;
            int number = 1000;
            long population = 8000000000;

            float price = 99.99f;
            double pi = 3.1415926535;
            decimal balance = 1500.75m;

            char grade = 'A';
            string name = "Boktiar";

            bool isActive = true;

            Console.WriteLine($"Byte: {age}");
            Console.WriteLine($"Short: {temperature}");
            Console.WriteLine($"Int: {number}");
            Console.WriteLine($"Long: {population}");

            Console.WriteLine($"Float: {price}");
            Console.WriteLine($"Double: {pi}");
            Console.WriteLine($"Decimal: {balance}");

            Console.WriteLine("Char: {0}", grade);
            Console.WriteLine($"String: {name}");

            Console.WriteLine($"Bool: {isActive}");
            // Wait for user input before closing
            Console.ReadLine();
        }
    }
}
