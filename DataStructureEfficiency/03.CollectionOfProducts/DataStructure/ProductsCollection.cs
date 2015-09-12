using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

namespace _03.CollectionOfProducts.DataStructure
{
    public class ProductsCollection
    {
        private Dictionary<int, Product> products;
        private OrderedDictionary<double, SortedSet<Product>> productsByPriceRange;
        private Dictionary<string, SortedSet<Product>> productsByTitle;
        private Dictionary<string, SortedSet<Product>> productsByTitleAndPrice;
        private Dictionary<string, OrderedDictionary<double, SortedSet<Product>>> productsByPriceRangeAndTitle;
        private Dictionary<string, SortedSet<Product>> productsBySupplierAndPrice;
        private Dictionary<string, OrderedDictionary<double, SortedSet<Product>>> productsByPriceRangeAndSupplier;

        public ProductsCollection()
        {
            this.products = new Dictionary<int, Product>();
            this.productsByPriceRange = new OrderedDictionary<double, SortedSet<Product>>();
            this.productsByTitle = new Dictionary<string, SortedSet<Product>>();
            this.productsByTitleAndPrice = new Dictionary<string, SortedSet<Product>>();
            this.productsByPriceRangeAndTitle = new Dictionary<string, OrderedDictionary<double, SortedSet<Product>>>();
            this.productsBySupplierAndPrice = new Dictionary<string, SortedSet<Product>>();
            this.productsByPriceRangeAndSupplier = new Dictionary<string, OrderedDictionary<double, SortedSet<Product>>>();
        }

        public int Count
        {
            get { return this.products.Count; }
        }

        public void Add(int id, string name, string supplier, double price)
        {
            if (products.ContainsKey(id))
            {
                this.Replace(id, name, supplier, price);
            }

            Product newProduct = new Product(id, name, supplier, price);

            // Add product by Id
            this.products.Add(id, newProduct);

            // Add product by Price Range
            this.productsByPriceRange.AppendValueToKey(price, newProduct);

            // Add product by Title
            this.productsByTitle.AppendValueToKey(name, newProduct);

            // Add product by Title and Price
            this.productsByTitleAndPrice.AppendValueToKey(this.CombineTwoParams(name, price), newProduct);

            // Add product by Title and Price range
            this.productsByPriceRangeAndTitle.EnsureKeyExists(name);
            this.productsByPriceRangeAndTitle[name].AppendValueToKey(price, newProduct);

            // Add product by Supplier and Price
            this.productsBySupplierAndPrice.AppendValueToKey(this.CombineTwoParams(supplier, price), newProduct);

            // Add product by Supplier and Price Range
            this.productsByPriceRangeAndSupplier.EnsureKeyExists(supplier);
            this.productsByPriceRangeAndSupplier[supplier].AppendValueToKey(price, newProduct);
        }

        public bool Remove(int id)
        {
            Product product = GetProductById(id);

            if (product == null)
            {
                return false;
            }

            string titlePriceCombined = this.CombineTwoParams(product.Title, product.Price);
            string supplierPriceCombined = this.CombineTwoParams(product.Supplier, product.Price);

            this.products.Remove(id);
            this.productsByPriceRange[product.Price].Remove(product);
            this.productsByTitleAndPrice[titlePriceCombined].Remove(product);
            this.productsByPriceRangeAndTitle[product.Title][product.Price].Remove(product);
            this.productsBySupplierAndPrice[supplierPriceCombined].Remove(product);
            this.productsByPriceRangeAndSupplier[product.Supplier][product.Price].Remove(product);

            return true;
        }

        public Product GetProductById(int id)
        {
            Product product = null;
            this.products.TryGetValue(id, out product);

            return product;
        }

        public IEnumerable<Product> FindProducts(int minPrice, int maxPrice)
        {
            var productsInRange = this.productsByPriceRange.Range(minPrice, true, maxPrice, true);

            foreach (var productInRange in productsInRange)
            {
                foreach (var product in productInRange.Value)
                {
                    yield return product;
                }
            }
        }

        public IEnumerable<Product> FindProducts(string title)
        {
            return this.productsByTitle.GetValuesForKey(title);
        }

        public IEnumerable<Product> FindProducts(string title, double price)
        {
            string combinedTitlePrice = this.CombineTwoParams(title, price);

            return this.productsByTitleAndPrice.GetValuesForKey(combinedTitlePrice);
        }

        public IEnumerable<Product> FindProducts(string title, int minPrice, int maxPrice)
        {
            var productsInRange = this.productsByPriceRangeAndTitle[title].Range(minPrice, true, maxPrice, true);

            foreach (var productInRange in productsInRange)
            {
                foreach (var product in productInRange.Value)
                {
                    yield return product;
                }
            } 
        }

        public IEnumerable<Product> FindProductsBySupplier(string supplier, double price)
        {
            string combinedSupplierPrice = this.CombineTwoParams(supplier, price);

            return this.productsBySupplierAndPrice.GetValuesForKey(combinedSupplierPrice);
        }

        public IEnumerable<Product> FindProductsBySupplier(string supplier, int minPrice, int maxPrice)
        {
            var productsInRange = this.productsByPriceRangeAndTitle[supplier].Range(minPrice, true, maxPrice, true);

            foreach (var productInRange in productsInRange)
            {
                foreach (var product in productInRange.Value)
                {
                    yield return product;
                }
            } 
        }

        private void Replace(int id, string name, string supplier, double price)
        {
            throw new NotImplementedException();
        }

        private string CombineTwoParams(string text, double number)
        {
            double formatedNumber = Math.Truncate(number * 100) / 100;

            return text + "|" + string.Format("{0:N2}%", formatedNumber);
        }
    }
}