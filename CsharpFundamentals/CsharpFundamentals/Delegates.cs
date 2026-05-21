using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpFundamentals
{
    // A delegate is a type that represents references to methods with a particular parameter list and return type.
    
    internal class Delegates
    {
        public delegate void DelegateAdd(int a, int b);
        public delegate void DelegateShow();
        private static void Add(int a, int b)
        {
            Console.WriteLine($"The sum of {a} and {b} is: {a + b}");
        }
        private static void Subtract(int a, int b)
        {
            Console.WriteLine($"The difference of {a} and {b} is: {a - b}");
        }
        private static void show() 
        { 
            Console.WriteLine("This is a show method");
        }
        public static void Main(string[] args)
        {
            DelegateAdd delAdd = new DelegateAdd(Add);
            delAdd(5, 10);
            delAdd = Subtract;
            delAdd(10, 5);

            DelegateShow delShow = new DelegateShow(show);
            delShow();
        }
    }
}
