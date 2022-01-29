using Common;
using DataAccess.DBServices.DTO;
using DataAccess.DBServices.Entities;
using DataAccess.DBServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.DBServices
{
    public class ClientProductRepository : ConnectionToSql, IClientProductRepository
    {
        public IEnumerable<ClientProductDTO> GetAllClientProduct()
        {
            var clientProductDTOList = new List<ClientProductDTO>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string query = @"
                                        SELECT
		                                        ClienteProduto.ID
		                                        ,Clientes.RazaoSocial
		                                        ,Produtos.NomeProduto
		                                        ,ClienteProduto.Email
		                                        ,ClienteProduto.StatusClienteProduto
                                        FROM	dbo.ClienteProduto	INNER JOIN dbo.Produtos
		                                        ON ClienteProduto.ProdutoID = Produtos.ID
		                                        INNER JOIN dbo.Clientes
		                                        ON ClienteProduto.ClienteID = Clientes.ID
                                        ORDER BY
		                                         Clientes.RazaoSocial
		                                         ,Produtos.NomeProduto
		                                         ,ClienteProduto.StatusClienteProduto
                                    ";

                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var clientProductDTO = new ClientProductDTO
                            {
                                Id = (int)reader[0],
                                RazaoSocial = reader[1].ToString(),
                                NomeProduto = reader[2].ToString(),
                                EmailEnvioCobranca = reader[3].ToString(),
                                StatusClienteProduto = reader[4].ToString()
                            };
                            clientProductDTOList.Add(clientProductDTO);
                        }
                    }
                }
            }
            return clientProductDTOList;
        }

        public IEnumerable<ClientProductDTORed> GetAllClientProduct(int clientProductId)
        {
            var clientProductDTORedList = new List<ClientProductDTORed>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string query = @"
                                    SELECT  ClienteProduto.ID
                                            ,Produtos.NomeProduto
                                            ,Email
		                                    ,ClienteProduto.StatusClienteProduto
                                    FROM	dbo.ClienteProduto	INNER JOIN dbo.Produtos
		                                    ON ClienteProduto.ProdutoID = Produtos.ID
                                    ";

                    query += " Where	ClienteProduto.ClienteID = " + clientProductId;
                    query += " ORDER BY Produtos.NomeProduto,ClienteProduto.StatusClienteProduto";

                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var clientProductDTORed = new ClientProductDTORed
                            {
                                Id = (int)reader[0],
                                NomeProduto = reader[1].ToString(),
                                EmailEnvioCobranca = reader[2].ToString(),
                                StatusClienteProduto = reader[3].ToString()
                            };
                            clientProductDTORedList.Add(clientProductDTORed);
                        }
                    }
                }
            }
            return clientProductDTORedList;
        }

        public int InsertClientProduct(ClientProductEntity clientProduct)
        {
            int result = -1;

            using (var connection = GetConnection())
            {
                connection.Open();
                var userActive = new UserRepository().GetUserById(ActiveUser.Id);
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"insert into ClienteProduto 
                                            VALUES
                                                (@ClienteID
                                                ,@ProdutoID
                                                ,@Email
                                                ,@StatusClienteProduto)
                                            ";
                    command.Parameters.AddWithValue("@ClienteID", clientProduct.ClientID);
                    command.Parameters.AddWithValue("@ProdutoID", clientProduct.ProdutoID);
                    command.Parameters.AddWithValue("@Email", clientProduct.EmailEnvioCobranca);
                    command.Parameters.AddWithValue("@StatusClienteProduto", clientProduct.StatusClienteProduto);

                    command.CommandType = CommandType.Text;
                    result = command.ExecuteNonQuery();
                }
            }
            return result;
        }

        public List<ClientProductDTO> GetClientProductByValue(string value)
        {
            var clientProductDTOList = new List<ClientProductDTO>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    string query = @"
                                        SELECT
		                                        ClienteProduto.ID
		                                        ,Clientes.RazaoSocial
		                                        ,Produtos.NomeProduto
		                                        ,ClienteProduto.Email
		                                        ,ClienteProduto.StatusClienteProduto
                                        FROM	dbo.ClienteProduto	INNER JOIN dbo.Produtos
		                                        ON ClienteProduto.ProdutoID = Produtos.ID
		                                        INNER JOIN dbo.Clientes
		                                        ON ClienteProduto.ClienteID = Clientes.ID
                                ";
                    query += @"         WHERE   Clientes.RazaoSocial like '%"+ value + "%'";

                    query += @"         ORDER BY
		                                         Clientes.RazaoSocial
		                                         ,Produtos.NomeProduto
		                                         ,ClienteProduto.StatusClienteProduto
                                ";

                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var clientProductDTO = new ClientProductDTO
                            {
                                Id = (int)reader[0],
                                RazaoSocial = reader[1].ToString(),
                                NomeProduto = reader[2].ToString(),
                                EmailEnvioCobranca = reader[3].ToString(),
                                StatusClienteProduto = reader[4].ToString()
                            };
                            clientProductDTOList.Add(clientProductDTO);
                        }
                    }
                }
            }
            return clientProductDTOList;
        }

        public int ModifyClientProduct(ClientProductEntity clientProduct)
        {
            int result = -1;

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                                            Update	ClienteProduto
                                            Set		ClienteID = @ClientId
		                                            ,ProdutoID = @ProductId
		                                            ,Email = @Email
		                                            ,StatusClienteProduto = @StatusClienteProduto
	                                        where   id=@id ";
                    command.Parameters.AddWithValue("@id", clientProduct.Id);
                    command.Parameters.AddWithValue("@ClientId", clientProduct.ClientID);
                    command.Parameters.AddWithValue("@ProductId", clientProduct.ProdutoID);
                    command.Parameters.AddWithValue("@Email", clientProduct.EmailEnvioCobranca);
                    command.Parameters.AddWithValue("@StatusClienteProduto", clientProduct.StatusClienteProduto);

                    //var userActive = new UserRepository().GetUserById(ActiveUser.Id);

                    //command.Parameters.AddWithValue("@OperadorManutencao", userActive.Username);
                    //command.Parameters.AddWithValue("@DataMautencao", DateTime.Now);

                    command.CommandType = CommandType.Text;
                    result = command.ExecuteNonQuery();
                }
            }
            return result;
        }

        List<ClientProductEntity> IClientProductRepository.GetClientProductByValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
