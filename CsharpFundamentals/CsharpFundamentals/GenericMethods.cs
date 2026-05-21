using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpFundamentals
{
    public class Generic
    {
        // Generic method to display elements of an array
        public static void ShowArray<T>(T[] arr)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }
    }
    internal class GenericMethods
    {
        public static void Main(string[] args)
        {
            int[] intArray = { 1, 2, 3, 4, 5 };
            string[] stringArray = { "Hello", "World", "Generic", "Methods" };
            Console.WriteLine("Integer Array:");
            Generic.ShowArray(intArray);
            Console.WriteLine("\nString Array:");
            Generic.ShowArray(stringArray);
        }
        
    }
}
