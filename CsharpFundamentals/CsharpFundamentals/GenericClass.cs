using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpFundamentals
{
    // Base entity 
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }

    // Product entity
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    // Customer entity
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
    }

    // Generic Repository
    public class Repository<T> where T : BaseEntity
    {
        private readonly List<T> _data = new List<T>();

        // CREATE
        public void Add(T entity)
        {
            _data.Add(entity);
        }

        // READ (by Id)
        public T GetById(int id)
        {
            return _data.FirstOrDefault(x => x.Id == id);
        }

        // READ ALL
        public List<T> GetAll()
        {
            return _data;
        }

        // DELETE
        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _data.Remove(entity);
            }
        }

        // UPDATE
        public void Update(T entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                int index = _data.IndexOf(existing);
                _data[index] = entity;
            }
        }
    }

    internal class GenericClass
    {
        public static void Main(string[] args)
        {
            // Repository for Product
            var productRepo = new Repository<Product>();

            productRepo.Add(new Product { Id = 1, Name = "Laptop", Price = 80000 });
            productRepo.Add(new Product { Id = 2, Name = "Mouse", Price = 200 });

            var product = productRepo.GetById(1);

            Console.WriteLine($"Product: {product.Name}, Price: {product.Price}");

            // Repository for Customer
            var customerRepo = new Repository<Customer>();

            customerRepo.Add(new Customer { Id = 6, FullName = "Boktiar" });

            var customer = customerRepo.GetById(1);

            Console.WriteLine($"Customer: {customer.FullName}");
        }
    }
}