using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBServices.Entities
{
   public class EmailTemplateEntity
   {
       public int Id { get; set; }
       public string Descricao { get; set; }
       public string Assunto { get; set; }
       public string Paragrafo1 { get; set; }
       public string Paragrafo2 { get; set; }
       public string Paragrafo3 { get; set; }
    }
}
