using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBServices.Entities
{
   public class ProductEntity
   {
       public int Id { get; set; }
       public string CodigoProduto { get; set; }
       public string NomeProduto { get; set; }
       public string OperadorManutencao { get; set; }
       public DateTime DataManutencao { get; set; }
    }
}
