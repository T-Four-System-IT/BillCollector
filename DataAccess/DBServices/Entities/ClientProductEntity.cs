using System;

namespace DataAccess.DBServices.Entities
{
    public class ClientProductEntity
    {
        public int Id { get; set; }
        public int ClientID { get; set; }
        public int ProdutoID { get; set; }
        public string EmailEnvioCobranca { get; set; }
		public string StatusClienteProduto { get; set; }
        //public string OperadorManutencao { get; set; }
        //public DateTime DataManutencao { get; set; }
    }
}
