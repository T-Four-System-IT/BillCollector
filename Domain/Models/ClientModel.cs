using DataAccess.DBServices;
using DataAccess.DBServices.Entities;
using DataAccess.MailServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ClientModel
    {
        #region -> Atributos
        private int _id;
        private string _cnpj;
        private string _razaoSocial;
        private string _nomeFantasia;
        private string _codeERP;
        private string _emailGestorComercial;
        private string _emailDiretorComercial;
        private string _tipoCliente;
        private string _operadorManutencao;
        private DateTime _dataManutencao;
        private string _statusCliente;

        private ClientRepository _clientRepository;
        #endregion

        #region -> Constructores
        public ClientModel()
        {
            _clientRepository = new ClientRepository();
        }

        public ClientModel(int id, string cnpj, string razaoSocial, string nomeFantasia, string codeERP, string emailGestorComercial, string emailDiretorComercial, string tipoCliente, string operadorManutencao, DateTime dataManutencao, string statusCliente)
        {
            Id = id;
            CNPJ = cnpj;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            CodeERP = codeERP;
            EmailGestorComercial = emailGestorComercial;
            EmailDiretorComercial = emailDiretorComercial;
            TipoCliente = tipoCliente;
            OperadorManutencao = operadorManutencao;
            DataManutencao = dataManutencao;
            StatusCliente = statusCliente;

            _clientRepository = new ClientRepository();
        }
        #endregion

        #region -> Propiedades + Validação e Visualizacão de Dados
        //Posição 0 
        [DisplayName("ID")]
        [Browsable(true)]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        //Posição 1
        [DisplayName("CNPJ")]
        [Required(ErrorMessage = "Informe o CNPJ.")]
        public string CNPJ
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }

        //Posição 2
        [DisplayName("Razão Social")]
        [Required(ErrorMessage = "Informe o Razão Social do cliente.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O Razão Social deve conter um mínimo de 5 caracteres.")]
        public string RazaoSocial
        {
            get { return _razaoSocial; }
            set { _razaoSocial = value; }
        }

        //Posição 3
        [DisplayName("Nome Fantasia")]
        [Required(ErrorMessage = "Informe o Nome Fantasia do cliente.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O Nome Fantaasia deve conter um mínimo de 5 caracteres.")]
        public string NomeFantasia
        {
            get { return _nomeFantasia; }
            set { _nomeFantasia = value; }
        }

        //Posição 4
        [DisplayName("CodERP")]
        [Required(ErrorMessage = "Por favor informe o Codigo do ERP.")]
        public string CodeERP
        {
            get { return _codeERP; }
            set { _codeERP = value; }
        }

        //Posição 5
        [DisplayName("Tipo de Cliente")]
        [Browsable(true)]
        //[Required(ErrorMessage = "Por favor informe o nome")]
        //[RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "O nome deve conter somente letras")]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve conter um mínimo de 3 caracteres.")]
        public string TipoCliente
        {
            get { return _tipoCliente; }
            set { _tipoCliente = value; }
        }

        //Posição 6
        [DisplayName("Email Gestor Comercial")]
        [Browsable(false)]
        [Required(ErrorMessage = "Por favor informe o email do Gestor Comercial")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O email deve conter um mínimo de 3 caracteres.")]
        public string EmailGestorComercial
        {
            get { return _emailGestorComercial; }
            set { _emailGestorComercial = value; }
        }

        //Posição 7
        [DisplayName("Email Gestor Comercial")]
        [Browsable(false)]
        [Required(ErrorMessage = "Por favor informe o email do Diretor Comercial")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O email deve conter um mínimo de 3 caracteres.")]
        public string EmailDiretorComercial
        {
            get { return _emailDiretorComercial; }
            set { _emailDiretorComercial = value; }
        }

        //Posição 8
        [DisplayName("Status do Cliente")]
        [Browsable(false)]
        public string StatusCliente
        {
            get { return _statusCliente; }
            set { _statusCliente = value; }
        }

        //Posição 9
        [DisplayName("Operador Manutenção")]
        [Browsable(false)]
        public string OperadorManutencao
        {
            get { return _operadorManutencao; }
            set { _operadorManutencao = value; }
        }

        //Posição 10
        [DisplayName("Data Manuntenção")]
        [Browsable(false)]
        public DateTime DataManutencao
        {
            get { return _dataManutencao; }
            set { _dataManutencao = value; }
        }
        #endregion

        #region -> Métodos Públicos
        public int CreateClient()
        {
            ClientEntity clientEntity = MapClientEntity(this);
            return _clientRepository.CreateClient(clientEntity);
        }

        public int ModifyClient()
        {
            ClientEntity clientEntity = MapClientEntity(this);
            _clientRepository.ModifyClient(clientEntity);
            return 1;
        }

        public ClientModel GetClientById(int id)
        {
            var result = _clientRepository.GetClientById(id);
            if (result != null)
                return MapClientModel(result);
            else
                return null;
        }

        public IEnumerable<ClientModel> GetAllClients()
        {
            var result = _clientRepository.GetAllClients();
            return MapClientModel(result);
        }

        public IEnumerable<ClientModel> GetByValue(string value)
        {
            var clientEntityList = _clientRepository.GetClientsByValue(value);
            return MapClientModel(clientEntityList);
        }
        #endregion

        #region -> Métodos Privados (Mapear dados)
        private ClientEntity MapClientEntity(ClientModel clientModel)
        {
            var clientEntity = new ClientEntity
            {
                Id = clientModel.Id,
                CNPJ = clientModel.CNPJ,
                RazaoSocial = clientModel.RazaoSocial,
                NomeFantasia = clientModel.NomeFantasia,
                CodeERP = clientModel.CodeERP,
                EmailGestorComercial = clientModel.EmailGestorComercial,
                EmailDiretorComercial = clientModel.EmailDiretorComercial,
                TipoCliente = clientModel.TipoCliente,
                OperadorManutencao = clientModel.OperadorManutencao,
                DataManutencao = clientModel.DataManutencao,
                StatusCliente = clientModel.StatusCliente
            };
            return clientEntity;
        }

        private ClientModel MapClientModel(ClientEntity clientEntity)
        {
            var clientModel = new ClientModel()
            {
                Id = clientEntity.Id,
                CNPJ = clientEntity.CNPJ,
                RazaoSocial = clientEntity.RazaoSocial,
                NomeFantasia = clientEntity.NomeFantasia,
                CodeERP = clientEntity.CodeERP,
                EmailGestorComercial = clientEntity.EmailGestorComercial,
                EmailDiretorComercial = clientEntity.EmailDiretorComercial,
                TipoCliente = clientEntity.TipoCliente,
                OperadorManutencao = clientEntity.OperadorManutencao,
                DataManutencao = clientEntity.DataManutencao,
                StatusCliente = clientEntity.StatusCliente
            };
            return clientModel;
        }

        private IEnumerable<ClientModel> MapClientModel(IEnumerable<ClientEntity> clientEntities)
        {
            var clientModels = new List<ClientModel>();
            foreach (var client in clientEntities)
            {
                clientModels.Add(MapClientModel(client));
            }
            return clientModels;
        }
        #endregion
    }
}
