using System;

namespace _03.CollectionOfProducts.DataStructure
{
    public class Product : IComparable<Product>
    {
        public Product(int id, string title, string supplier, double price)
        {
            this.Id = id;
            this.Title = title;
            this.Supplier = supplier;
            this.Price = price;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Supplier { get; set; }

        public double Price { get; set; }
        
        public int CompareTo(Product other)
        {
            return this.Id.CompareTo(other.Id);
        }
    }
}