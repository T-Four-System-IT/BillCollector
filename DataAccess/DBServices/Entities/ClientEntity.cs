using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBServices.Entities
{
    public class ClientEntity
    {
        public int Id { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string CodeERP { get; set; }
		public string EmailGestorComercial { get; set; }
		public string EmailDiretorComercial { get; set; }
        public string TipoCliente { get; set; }
        public string OperadorManutencao { get; set; }
		public DateTime DataManutencao { get; set; }
		public string StatusCliente { get; set; }
	}
}
