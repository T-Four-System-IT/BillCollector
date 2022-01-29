using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Utils
{
   public abstract class ExceptionManager
    {
       public static string GetMessage(Exception exception)
       {
           System.Data.SqlClient.SqlException sqlEx = exception as System.Data.SqlClient.SqlException;

           if (sqlEx != null && sqlEx.Number == 2627)
           {
               string value = sqlEx.Message.Split('(', ')')[1];
               return "Registro já cadastrado.\n    ■ " + value;
           }
           else
           {
               return "Erro não definido.\nDetalhes:\n" + exception.Message;
           }
       }
    }
}
