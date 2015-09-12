
namespace CollectionOfProducts.UnitTests
{
    using _03.CollectionOfProducts.DataStructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;

    [TestClass]
    public class UnitTestsProductsCollection
    {
        private ProductsCollection products;

        [TestInitialize]
        public void TestInitialize()
        {
            this.products = new ProductsCollection();
        }

        [TestMethod]
        public void AddProduct_ShouldWorkCorrectly()
        {
            // Arrange

            // Act
            this.products.Add(1, "Kufte", "Sofia", 12.0);

            // Assert
            Assert.AreEqual(1, this.products.Count);
        }

        [TestMethod]
        public void AddProduct_DuplicatedId_ShouldWorkCorrectly()
        {
            // Arrange

            // Act
            this.products.Add(1, "Kufte123", "Sofi2134a", 234);
            this.products.Add(1, "Kufte", "Sofia", 12.0);

            // Assert
            Assert.AreEqual(1, this.products.Count);
            Assert.AreEqual("Kufte", this.products.GetProductById(1).Title);
        }

        [TestMethod]
        public void FindProduct_ExistingProduct_ShouldReturnProduct()
        {
            // Arrange
            this.products.Add(1, "Kufte", "Sofia", 12.0);

            // Act
            var product = this.products.GetProductById(1);

            // Assert
            Assert.IsNotNull(product);
        }

        [TestMethod]
        public void FindProduct_NonExistingProduct_ShouldReturnNothing()
        {
            // Arrange

            // Act
            var product = this.products.GetProductById(1);

            // Assert
            Assert.IsNull(product);
        }

        [TestMethod]
        public void DeleteProduct_ShouldWorkCorrectly()
        {
            // Arrange
            this.products.Add(1, "Kufte", "Sofia", 12.0);

            // Act
            bool isDeletedExisting = this.products.Remove(1);
            bool isDeletedNonExisting = this.products.Remove(1);

            // Assert
            Assert.IsTrue(isDeletedExisting);
            Assert.IsFalse(isDeletedNonExisting);
            Assert.AreEqual(0, products.Count);
        }

        [TestMethod]
        public void FindProductsByTitle_ShouldReturnMatchingProducts()
        {
            // Arrange
            this.products.Add(1, "Kufte", "Sofia", 1.6);
            this.products.Add(2, "Obuvka", "SofiaPres", 14.0);
            this.products.Add(3, "Supa", "Tesla", 24.0);
            this.products.Add(4, "Kebapche", "Microsoft", 2.0);
            this.products.Add(5, "Kebapche", "Tesla", 14.0);
            this.products.Add(6, "Kebapche", "SofiaPres", 24.0);
            this.products.Add(7, "Kufte", "Microsoft", 7.0);

            // Act
            var productsKebapche = this.products.FindProducts("Kebapche");
            var productsObuvka = this.products.FindProducts("Obuvka");
            var productsSupa = this.products.FindProducts("Supa123");

            // Assert
            CollectionAssert.AreEqual(
                new int[] {4, 5, 6},
                productsKebapche.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] {2},
                productsObuvka.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] {},
                productsSupa.Select(p => p.Id).ToList());
        }

        [TestMethod]
        public void FindProductsByTitleAndPrice_ShouldReturnMatchingProducts()
        {
            // Arrange
            this.products.Add(1, "Kufte", "Sofia", 7.0);
            this.products.Add(2, "Obuvka", "SofiaPres", 14.0);
            this.products.Add(3, "Supa", "Tesla", 24.0);
            this.products.Add(4, "Kebapche", "Microsoft", 2.0);
            this.products.Add(5, "Kebapche", "Tesla", 14.0);
            this.products.Add(6, "Kebapche", "SofiaPres", 24.0);
            this.products.Add(7, "Kufte", "Microsoft", 7.0);

            // Act
            var productsKufte7 = this.products.FindProducts("Kufte", 7.0);
            var productsKebapche = this.products.FindProducts("Kebapche", 2.0);
            var productsNoTitle = this.products.FindProducts(null, 1.0);
            var productsNoSuchPrice = this.products.FindProducts("Kebapche", 1000);

            // Assert
            CollectionAssert.AreEqual(
                new int[] { 1, 7 },
                productsKufte7.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] { 4 },
                productsKebapche.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] { },
                productsNoTitle.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] { },
                productsNoSuchPrice.Select(p => p.Id).ToList());
        }

        [TestMethod]
        public void FindProductsByPriceRange_ShouldReturnMatchingProducts()
        {
            // Arrange
            this.products.Add(1, "Kufte", "Sofia", 7.0);
            this.products.Add(2, "Obuvka", "SofiaPres", 14.0);
            this.products.Add(3, "Supa", "Tesla", 38.0);
            this.products.Add(4, "Kebapche", "Microsoft", 2.0);
            this.products.Add(5, "Kebapche", "Tesla", 14.0);
            this.products.Add(6, "Kebapche", "SofiaPres", 24.0);
            this.products.Add(7, "Kufte", "Microsoft", 7.0);

            // Act
            var productsPricedFrom2to7 = this.products.FindProducts(2, 7);
            var productsPricedFrom14to15 = this.products.FindProducts(14, 15);
            var productsPriced385 = this.products.FindProducts(38, 38);
            var productsPriced2 = this.products.FindProducts(2, 2);
            var productsPricedFrom0to1000 = this.products.FindProducts(0, 1000);

            var a = productsPricedFrom2to7.Select(p => p.Id).ToList();

            // Assert
            CollectionAssert.AreEqual(
                new int[] { 1, 4, 7 },
                productsPricedFrom2to7.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] { 2, 5 },
                productsPricedFrom14to15.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] { 3 },
                productsPriced385.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] { 4 },
                productsPriced2.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] { 1, 2, 3, 4, 5, 6, 7 },
                productsPricedFrom0to1000.Select(p => p.Id).ToList());
        }

        [TestMethod]
        public void FindProductsByPriceRangeAndTitle_ShouldReturnMatchingProducts()
        {
            // Arrange
            this.products.Add(1, "Kufte", "Sofia", 7.0);
            this.products.Add(2, "Obuvka", "SofiaPres", 14.0);
            this.products.Add(3, "Supa", "Tesla", 38.0);
            this.products.Add(4, "Kebapche", "Microsoft", 2.0);
            this.products.Add(5, "Kebapche", "Tesla", 14.0);
            this.products.Add(6, "Kebapche", "SofiaPres", 24.0);
            this.products.Add(7, "Kufte", "Microsoft", 7.0);

            // Act
            var productsPriced20to40Supa = this.products.FindProducts("Supa", 20, 40);
            var productsObuvka = this.products.FindProducts("Obuvka", 10, 14);
            var productsPriced0Obuvka = this.products.FindProducts("Obuvka", 0, 1);
            var productsPriced7Kufte = this.products.FindProducts("Kufte", 7, 7);
            var productsPricedFrom0to1000Kebapche = this.products.FindProducts("Kebapche", 0, 1000);
            var productsPricedFrom0to1000Pervol = this.products.FindProducts("Pervol", 0, 1000);

            // Assert
            CollectionAssert.AreEqual(
                new int[] { 3 },
                productsPriced20to40Supa.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] { 2 },
                productsObuvka.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] { },
                productsPriced0Obuvka.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] { 1, 7 },
                productsPriced7Kufte.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] { 4, 5, 6 },
                productsPricedFrom0to1000Kebapche.Select(p => p.Id).ToList());
            CollectionAssert.AreEqual(
                new int[] { },
                productsPricedFrom0to1000Pervol.Select(p => p.Id).ToList());
        }

        [TestMethod]
        public void FindDeletedProducts_ShouldReturnEmptyCollection()
        {
            // Arrange
            this.products.Add(1, "Kufte", "Sofia", 7.0);
            this.products.Add(2, "Obuvka", "SofiaPres", 14.0);
            this.products.Add(3, "Supa", "Tesla", 38.0);
            this.products.Add(4, "Kebapche", "Microsoft", 2.0);
            this.products.Add(5, "Kebapche", "Tesla", 14.0);
            this.products.Add(6, "Kebapche", "SofiaPres", 24.0);
            this.products.Add(7, "Kufte", "Microsoft", 7.0);

            this.products.Remove(1);
            this.products.Remove(2);
            this.products.Remove(3);
            this.products.Remove(4);
            this.products.Remove(5);
            this.products.Remove(6);
            this.products.Remove(7);

            // Act
            var productNull = this.products.GetProductById(1);
            var productNull2 = this.products.GetProductById(5);
            var productNull3 = this.products.GetProductById(7);

            var productsKebapche = this.products.FindProducts("Kebapche");

            var productsPricedFrom2to7 = this.products.FindProducts(2, 7);
            var productsPricedFrom0to1000 = this.products.FindProducts(0, 1000);

            var productsPriced20to40Supa = this.products.FindProducts("Supa", 20, 40);
            var productsObuvka = this.products.FindProducts("Obuvka", 10, 14);

            // Assert
            Assert.AreEqual(null, productNull);
            Assert.AreEqual(null, productNull2);
            Assert.AreEqual(null, productNull3);

            Assert.AreEqual(0, productsKebapche.Count());

            Assert.AreEqual(0, productsPricedFrom2to7.Count());
            Assert.AreEqual(0, productsPricedFrom0to1000.Count());

            Assert.AreEqual(0, productsPriced20to40Supa.Count());
            Assert.AreEqual(0, productsObuvka.Count());
        }
    }
}