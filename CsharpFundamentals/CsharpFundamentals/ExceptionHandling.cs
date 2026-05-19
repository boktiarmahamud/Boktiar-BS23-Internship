using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpFundamentals
{
    internal class ExceptionHandling
    {
        public static void Main(string[] args)
        {
            try
            {
                int a = 10;
                int b = 0;
                int result = a / b;
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Execution completed.");
            }
        }
    }
}
