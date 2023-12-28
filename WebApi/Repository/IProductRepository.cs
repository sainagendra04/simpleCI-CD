using WebApi.Model;

namespace WebApi.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetAllProducts();
        public Product GetProductById(int id);
        public List<Product> GetProductByName(string productName);
        public Product AddProduct(Product product);
        public Product UpdateProduct(int productId, Product updatedProduct);
        public string DeleteProduct(int productId);
    }
}
