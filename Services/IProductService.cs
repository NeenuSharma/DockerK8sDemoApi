using DockerK8sDemoApi.Data;

namespace DockerK8sDemoApi.Services
{
    public interface IProductService
    {
        public List<Products> GetProducts(int id);

        public List<Products> CreateProduct(Products product);
    }
}
