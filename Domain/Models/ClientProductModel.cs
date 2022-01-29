using DataAccess.DBServices;
using DataAccess.DBServices.DTO;
using DataAccess.DBServices.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Domain.Models
{
    public class ClientProductModel
    {
        #region -> Atributos
        private int _id;

        private string _razaoSocial;
        private string _nomeProduto;
        private string _emailCobrancaCliente;
        private string _statusClientProduto;
        //private string _operadorManutencao;
        //private DateTime _dataManutencao;

        private ClientProductRepository _clientProductRepository;
        #endregion

        #region -> Constructores
        public ClientProductModel()
        {
            _clientProductRepository = new ClientProductRepository();
        }
        #endregion

        //Posição 0 
        [DisplayName("ID")]
        [Browsable(true)]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        //Posição 1
        [DisplayName("Razão Social")]
        [Browsable(true)]
        public string RazaoSocial
        {
            get { return _razaoSocial; }
            set { _razaoSocial = value; }
        }

        //Posição 2
        [DisplayName("Nome Produto")]
        [Browsable(true)]
        public string NomeProduto
        {
            get { return _nomeProduto; }
            set { _nomeProduto = value; }
        }

        //Posição 3
        [DisplayName("Email Cobrança")]
        [Browsable(true)]
        public string EmailCobrancaCliente
        {
            get { return _emailCobrancaCliente; }
            set { _emailCobrancaCliente = value; }
        }

        //Posição 4
        [DisplayName("Status do Produto Cliente")]
        [Browsable(true)]
        public string StatusClienteProduto
        {
            get { return _statusClientProduto; }
            set { _statusClientProduto = value; }
        }

        public IEnumerable<ClientProductModel> GetAllClientProducts()
        {
            IEnumerable<ClientProductDTO> result = _clientProductRepository.GetAllClientProduct();
            return MapDTOToModel(result);
        }

        public IEnumerable<ClientProductDTORed> GetAllClientProducts(int clientProductId)
        {
            IEnumerable<ClientProductDTORed> result = _clientProductRepository.GetAllClientProduct(clientProductId);
            return result;
        }

        public int CreateClientProduct(ClientProductEntity clientProductEntity)
        {
            return _clientProductRepository.InsertClientProduct(clientProductEntity);
        }

        public int ModifyClientProduct(ClientProductEntity clientProductEntity)
        {
            _clientProductRepository.ModifyClientProduct(clientProductEntity);
            return 1;
        }

        public IEnumerable<ClientProductModel> GetClientProductByValue(string value)
        {
            IEnumerable<ClientProductDTO> result = _clientProductRepository.GetClientProductByValue(value);
            return MapDTOToModel(result);
        }

        #region Metodos Privados
        private IEnumerable<ClientProductModel> MapDTOToModel(IEnumerable<ClientProductDTO> clientProductDTOs)
        {
            var clientProductModels = new List<ClientProductModel>();
            foreach (var clientProductDTO in clientProductDTOs)
            {
                clientProductModels.Add(MapDTOToModel(clientProductDTO));
            }
            return clientProductModels;
        }

        private ClientProductModel MapDTOToModel(ClientProductDTO clientProductDTO)
        {
            var clienteProductModel = new ClientProductModel
            {
                Id = clientProductDTO.Id,
                RazaoSocial = clientProductDTO.RazaoSocial,
                NomeProduto = clientProductDTO.NomeProduto,
                EmailCobrancaCliente = clientProductDTO.EmailEnvioCobranca,
                StatusClienteProduto = clientProductDTO.StatusClienteProduto
            };
            return clienteProductModel;
        }
        #endregion
    }
}