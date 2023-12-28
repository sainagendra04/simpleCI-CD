using WebApi.Model;

namespace WebApi.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly List<Product> products;

        public ProductRepository()
        {
            products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Realme9pro", ProductBrand = "Realme", ProductQuantity = 5, ProductPrice = 26000.0m },
                new Product { ProductId = 2, ProductName = "Shoes", ProductBrand = "Bata", ProductQuantity = 2, ProductPrice = 5000.0m }
            };
        }
        public List<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProductById(int productId)
        {
            return products.FirstOrDefault(p => p.ProductId == productId);
        }

        public List<Product> GetProductByName(string productName)
        {
            return products.Where(p=>p.ProductName.ToLower().Contains(productName.ToLower())).ToList();
        }

        public Product AddProduct(Product product)
        {
            product.ProductId = products.Count + 1;
            products.Add(product);
            return product;
        }

        public Product UpdateProduct(int productId, Product updatedProduct)
        {
            var existingProduct=products.FirstOrDefault(p=>p.ProductId == productId);
            if(existingProduct != null)
            {
                existingProduct.ProductName=updatedProduct.ProductName;
                existingProduct.ProductBrand=updatedProduct.ProductBrand;
                existingProduct.ProductQuantity=updatedProduct.ProductQuantity;
                existingProduct.ProductPrice=updatedProduct.ProductPrice;
                return existingProduct;
            }
            return null;
        }

        public string DeleteProduct(int productId)
        {
            var product = products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                products.Remove(product);
                return product.ProductName;
            }
            return null;
        }
    }
}
