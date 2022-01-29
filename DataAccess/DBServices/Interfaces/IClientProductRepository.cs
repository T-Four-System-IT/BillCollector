using DataAccess.DBServices.DTO;
using DataAccess.DBServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBServices.Interfaces
{
    interface IClientProductRepository
    {
        IEnumerable<ClientProductDTO> GetAllClientProduct();
        int InsertClientProduct(ClientProductEntity clientProduct);
        int ModifyClientProduct(ClientProductEntity clientProduct);
        List<ClientProductEntity> GetClientProductByValue(string value);
    }
}
