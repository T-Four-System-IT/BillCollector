using DataAccess.DBServices;
using DataAccess.DBServices.Entities;
using DataAccess.MailServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ProductModel
    {
        #region -> Atributos
        private int _id;
        private string _codigoProduto;
        private string _nomeProduto;
        private string _operadorManutencao;
        private string _dataManutencao;

        private ProductRepository _productRepository;
        #endregion

        #region -> Constructores
        public ProductModel()
        {
            _productRepository = new ProductRepository();
        }

        public ProductModel(int id, string codigoProduto, string nomeProduto, string operadorManutencao, string dataManutencao)
        {
            Id = id;
            CodigoProduto = codigoProduto;
            NomeProduto = nomeProduto;
            OperadorManutencao = operadorManutencao;
            DataManutencao = dataManutencao;

            _productRepository = new ProductRepository();
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
        [DisplayName("Código Produto")]
        [Required(ErrorMessage = "Informe o Código do Produto.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O código deve conter um mínimo de 3 caracteres.")]
        public string CodigoProduto
        {
            get { return _codigoProduto; }
            set { _codigoProduto = value; }
        }

        //Posição 2
        [DisplayName("Nome Produto")]
        [Browsable(true)]
        [Required(ErrorMessage = "Por favor informe o nome do Produto.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "O nome do Produto deve conter um mínimo de 5 caracteres.")]
        public string NomeProduto
        {
            get { return _nomeProduto; }
            set { _nomeProduto = value; }
        }

        //Posição 3
        [DisplayName("Operador Manutenção")]
        [Browsable(false)]
        public string OperadorManutencao
        {
            get { return _operadorManutencao; }
            set { _operadorManutencao = value; }
        }

        //Posição 4
        [DisplayName("Data Manutenção")]
        [Browsable(false)]
        public string DataManutencao
        {
            get { return _dataManutencao; }
            set { _dataManutencao = value; }
        }
        #endregion

        #region -> Métodos Públicos
        public int CreateProduct()
        {
            ProductEntity productEntity = MapProductEntity(this);
            return _productRepository.CreateProduct(productEntity);
        }

        public int ModifyProduct()
        {
            ProductEntity productEntity = MapProductEntity(this);
            return _productRepository.ModifyProduct(productEntity);
        }

        public int RemoveProduct(int id)
        {
            return _productRepository.RemoveProduct(id);
        }

        public ProductModel GetProductById(int id)
        {
            var result = _productRepository.GetProductById(id);
            if (result != null)
                return MapProductModel(result);
            else
                return null;
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            IEnumerable<ProductEntity> result = _productRepository.GetAllProducts();
            return MapProductModel(result);
        }

        public IEnumerable<ProductModel> GetByValue(string value)
        {
            var productEntityList = _productRepository.GetProductsByValue(value);
            return MapProductModel(productEntityList);
        }
        #endregion

        #region -> Métodos Privados (Mapear dados)
        private ProductEntity MapProductEntity(ProductModel productModel)
        {
            var productEntity = new ProductEntity
            {
                Id = productModel.Id,
                CodigoProduto = productModel.CodigoProduto,
                NomeProduto = productModel.NomeProduto,
                OperadorManutencao = "teste",
                DataManutencao = DateTime.Now
            };
            return productEntity;
        }

        private ProductModel MapProductModel(ProductEntity productEntity)
        {
            var productModel = new ProductModel()
            {
                Id = productEntity.Id,
                CodigoProduto = productEntity.CodigoProduto,
                NomeProduto = productEntity.NomeProduto,
                OperadorManutencao = productEntity.OperadorManutencao,
                DataManutencao = productEntity.DataManutencao.ToString(),
            };
            return productModel;
        }

        //private List<ProductModel> MapProductModel(List<ProductEntity> productEntityList)
        //{
        //    List<ProductModel> productModelList = new List<ProductModel>();

        //    foreach (var item in productEntityList)
        //    {
        //        var productModel = new ProductModel()
        //        {
        //            Id = item.Id,
        //            CodigoProduto = item.CodigoProduto,
        //            NomeProduto = item.NomeProduto,
        //            OperadorManutencao = item.OperadorManutencao,
        //            DataManutencao = item.DataManutencao.ToString(),
        //        };
        //        productModelList.Add(productModel);
        //    }
        //    return productModelList;
        //}

        private IEnumerable<ProductModel> MapProductModel(IEnumerable<ProductEntity> productEntities)
        {
            var productModels = new List<ProductModel>();
            foreach (var product in productEntities)
            {
                productModels.Add(MapProductModel(product));
            }
            return productModels;
        }

        #endregion
    }
}
