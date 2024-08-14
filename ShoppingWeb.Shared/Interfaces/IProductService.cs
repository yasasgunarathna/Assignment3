using ShoppingWeb.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWeb.Shared.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        ProductDto GetProductById(int id);
        void AddProduct(ProductDto productDto);
        void UpdateProduct(int id, ProductDto productDto);
        void DeleteProduct(int id);
    }


}
