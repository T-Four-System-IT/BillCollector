using Common;
using DataAccess.DBServices.Entities;
using DataAccess.DBServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.DBServices
{
    public class ProductRepository : ConnectionToSql, IProductRepository
    {
        public int CreateProduct(ProductEntity product)
        {
            int result = -1;

            using (var connection = GetConnection())
            {
                connection.Open();
                var userActive = new UserRepository().GetUserById(ActiveUser.Id);

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"insert into Produtos values (@CodigoProduto,@NomeProduto,@OperadorManutencao,@DataManutencao)";
                    command.Parameters.AddWithValue("@CodigoProduto", product.CodigoProduto);
                    command.Parameters.AddWithValue("@NomeProduto", product.NomeProduto);
                    command.Parameters.AddWithValue("@OperadorManutencao", userActive.Username);
                    command.Parameters.AddWithValue("@DataManutencao", product.DataManutencao);

                    command.CommandType = CommandType.Text;
                    result = command.ExecuteNonQuery();
                }
            }
            return result;
        }

        public int ModifyProduct(ProductEntity product)
        {
            int result = -1;

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"update  Produtos 
                                            set     CodigoProduto=@CodigoProduto,
                                                    NomeProduto=@NomeProduto,
                                                    OperadorManutencao=@OperadorManutencao,
                                                    DataManutencao=@DataMautencao  
	                                        where   id=@id ";
                    command.Parameters.AddWithValue("@id", product.Id);
                    command.Parameters.AddWithValue("@CodigoProduto", product.CodigoProduto);
                    command.Parameters.AddWithValue("@NomeProduto", product.NomeProduto);

                    var userActive = new UserRepository().GetUserById(ActiveUser.Id);

                    command.Parameters.AddWithValue("@OperadorManutencao", userActive.Username);
                    command.Parameters.AddWithValue("@DataMautencao", DateTime.Now);

                    command.CommandType = CommandType.Text;
                    result = command.ExecuteNonQuery();
                }
            }
            return result;
        }

        public int RemoveProduct(int id)
        {
            int result = -1;

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"delete from Produtos where id=@id ";
                    command.Parameters.AddWithValue("@id", id);

                    command.CommandType = CommandType.Text;
                    result = command.ExecuteNonQuery();
                }
            }
            return result;
        }

        public ProductEntity GetProductById(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from Produtos where id=@id";
                    command.Parameters.AddWithValue("@id", id);
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var productObj = new ProductEntity
                        {
                            Id = (int)reader[0],
                            CodigoProduto = reader[1].ToString(),
                            NomeProduto = reader[2].ToString(),
                            OperadorManutencao = reader[3].ToString(),
                            DataManutencao = (DateTime)reader[4],
                        };
                        return productObj;
                    }
                    else
                        return null;
                }
            }
        }

        public ProductEntity GetProductByName(string productName)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from Produtos where NomeProduto = @ProductName";
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var productObj = new ProductEntity
                        {
                            Id = (int)reader[0],
                            CodigoProduto = reader[1].ToString(),
                            NomeProduto = reader[2].ToString(),
                            OperadorManutencao = reader[3].ToString(),
                            DataManutencao = (DateTime)reader[4],
                        };
                        return productObj;
                    }
                    else
                        return null;
                }
            }
        }

        public IEnumerable<ProductEntity> GetAllProducts()
        {
            var productList = new List<ProductEntity>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from Produtos order by NomeProduto";
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var productObj = new ProductEntity
                            {
                                Id = (int)reader[0],
                                CodigoProduto = reader[1].ToString(),
                                NomeProduto = reader[2].ToString(),
                                OperadorManutencao = reader[3].ToString(),
                                DataManutencao = (DateTime)reader[4],
                            };
                            productList.Add(productObj);
                        }
                    }
                }
            }
            return productList;
        }

        public List<ProductEntity> GetProductsByValue(string value)
        {
            var productList = new List<ProductEntity>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    string query = "select * from Produtos Where NomeProduto like '%" + value + "%'";
                    command.CommandText = query;
                    //command.Parameters.AddWithValue("@ProductName", value);
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var productObj = new ProductEntity
                            {
                                Id = (int)reader[0],
                                CodigoProduto = reader[1].ToString(),
                                NomeProduto = reader[2].ToString(),
                                OperadorManutencao = reader[3].ToString(),
                                DataManutencao = (DateTime)reader[4],
                            };
                            productList.Add(productObj);
                        }
                    }
                }
            }
            return productList;
        }
    }
}
