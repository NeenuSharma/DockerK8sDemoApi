using DockerK8sDemoApi.Context;
using DockerK8sDemoApi.Data;

namespace DockerK8sDemoApi.Repository
{
    public class ProdcutServiceRepository
    {
        private readonly DataBaseDbContext _context;
        public ProdcutServiceRepository(DataBaseDbContext context)
        {
            _context = context;
        }
        public List<Products> GetProduct(int id)
        {
            var product = _context.product.FirstOrDefault(pid => pid.Id == id);
            if (product != null)
            {
                return new List<Products> { product };
            }
            else
            {
                return new List<Products>();
            }
        }
        public List<Products>  createProduct(Products product)
        {
            _context.product.Add(product);
            _context.SaveChanges();
            return new List<Products> { product  };
        }
    }
}
