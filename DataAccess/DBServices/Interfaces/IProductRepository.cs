using DataAccess.DBServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBServices.Interfaces
{
    interface IProductRepository
    {
        int CreateProduct(ProductEntity product);
        int ModifyProduct(ProductEntity product);
        int RemoveProduct(int id);
        ProductEntity GetProductById(int id);
        ProductEntity GetProductByName(string productName);
        IEnumerable<ProductEntity> GetAllProducts();
        List<ProductEntity> GetProductsByValue(string value);
    }
}
