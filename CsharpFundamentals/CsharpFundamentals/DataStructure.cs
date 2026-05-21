using System;
using System.Collections.Generic;

namespace CsharpFundamentals
{
    // Model class
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
    public class CSE
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Animal(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    internal class DataStructure
    {
        public static void Main(string[] args)
        {
            int[] arr = new int[] { 1, 2, 3, 4 };

            Console.WriteLine("Array:");

            foreach (int i in arr)
            {
                Console.WriteLine(i);
            }

            // Create student list
            List<Student> data = new List<Student>();

            // Add data into list
            data.Add(new Student()
            {
                Id = 214006,
                Name = "Boktiar",
                Address = "Rangpur"
            });

            data.Add(new Student()
            {
                Id = 214007,
                Name = "Sojib",
                Address = "Jamalpur"
            });

            data.Add(new Student()
            {
                Id = 214001,
                Name = "Nayan",
                Address = "Rajshahi"
            });

            // Display data
            Console.WriteLine("\nStudent Information:");
            Console.WriteLine("---------------------");

            foreach (Student x in data)
            {
                Console.WriteLine(
                    "Id: " + x.Id +
                    ", Name: " + x.Name +
                    ", Address: " + x.Address
                );
            }
            var depDictionary = new Dictionary<int, CSE>()
            {
                { 214006, new CSE{ Name = "Boktiar", Address = "Rangpur"}},
                { 214007, new CSE{ Name = "Sojib", Address = "Rangpur"}},
            };

            Console.WriteLine("Dictionary data: ");
            foreach (KeyValuePair<int, CSE> x in depDictionary)
            {
                Console.WriteLine(
                        "Id: " + x.Key +
                        ", Name: " + x.Value.Name +
                        ", Address: " + x.Value.Address
                    );
            }

            Console.WriteLine("HashSets: ");
            HashSet<Animal> animals = new HashSet<Animal>()
            {
                    new Animal(1, "Tiger"),
                    new Animal(2, "Lion"),
                    new Animal(3, "Cat")
            };

            foreach (Animal animal in animals)
            {
                Console.WriteLine(
                   "Id: " + animal.Id +
                   ", Name: " + animal.Name
               );
            }

            Console.WriteLine("Stack: ");
            Stack<int> st = new Stack<int>();

            st.Push(5);
            st.Push(6);
            st.Push(7);
            st.Pop();
            foreach (var x in st)
            {
                Console.WriteLine(x);
            }

            Console.WriteLine("Queue");

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(5);
            queue.Enqueue(6);
            queue.Enqueue(8);
            queue.Dequeue();

            foreach (var x in queue)
            {
                Console.WriteLine(x);
            }

            Console.WriteLine("LINQ to Filter");

            var dep = new List<string> { "CSE", "ME", "EEE", "ARCH", "IPE" };
            dep = dep.Where(item => item != "ME").ToList();

            foreach (var d in dep)
            {
                Console.WriteLine(d);
            }
        }
    }
}