using DockerK8sDemoApi.Data;
using DockerK8sDemoApi.Repository;

namespace DockerK8sDemoApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ProdcutServiceRepository _prodcutServiceRepository;
        public ProductService(ProdcutServiceRepository prodcutServiceRepository)
        {
            _prodcutServiceRepository = prodcutServiceRepository;
        }
        public List<Products> CreateProduct(Products product)
        {
           var result = _prodcutServiceRepository.createProduct(product);
             return result;
        }

        public List<Products> GetProducts(int id)
        {
            var products = _prodcutServiceRepository.GetProduct(id);
            return products;
        }
    } 
}
