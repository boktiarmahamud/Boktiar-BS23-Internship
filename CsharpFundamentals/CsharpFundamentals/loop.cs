using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpFundamentals
{
    internal class loop
    {
        public static void Main(string[] args)
        {
            // For loop
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"For loop iteration: {i}");
            }
            // While loop
            int j = 0;
            while (j < 3)
            {
                Console.WriteLine($"While loop iteration: {j}");
                j++;
            }
            // Do-while loop
            int k = 0;
            do
            {
                Console.WriteLine($"Do-while loop iteration: {k}");
                k++;
            } while (k < 3);
        }
    }
}
