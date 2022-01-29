using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBServices.DTO
{
    public class ClientProductDTORed
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public string EmailEnvioCobranca { get; set; }
        public string StatusClienteProduto { get; set; }
    }
}
