using DataAccess.DBServices.Entities;
using DataAccess.DBServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataAccess.DBServices
{
    public class ClientRepository : ConnectionToSql, IClientRepository
    {
        public int CreateClient(ClientEntity client)
        {
            int result = -1;

            using (var connection = GetConnection())
            {
                connection.Open();
                var userActive = new UserRepository().GetUserById(ActiveUser.Id);
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"insert into Clientes 
                                            values (@Cnpj,@RazaoSocial,@NomeFantasia,@CodeERP,
                                                    @EmailGestorComercial,@EmailDiretorComercial,
                                                    @TipoCliente,@OperadorManutencao,
                                                    @DataManutencao,@StatusCliente    
                                            )";
                    command.Parameters.AddWithValue("@Cnpj", client.CNPJ);
                    command.Parameters.AddWithValue("@RazaoSocial", client.RazaoSocial);
                    command.Parameters.AddWithValue("@NomeFantasia", client.NomeFantasia);
                    command.Parameters.AddWithValue("@CodeERP", client.CodeERP);
                    command.Parameters.AddWithValue("@EmailGestorComercial", client.EmailGestorComercial);
                    command.Parameters.AddWithValue("@EmailDiretorComercial", client.EmailDiretorComercial);
                    command.Parameters.AddWithValue("@TipoCliente", client.TipoCliente);
                    command.Parameters.AddWithValue("@OperadorManutencao", userActive.Username);
                    command.Parameters.AddWithValue("@DataManutencao", client.DataManutencao);
                    command.Parameters.AddWithValue("@StatusCliente", client.StatusCliente);

                    command.CommandType = CommandType.Text;
                    result = command.ExecuteNonQuery();
                }
            }
            return result;
        }

        public int ModifyClient(ClientEntity clientEntity)
        {//Actualizar usuario.
            int result = -1;

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"update  Clientes	
                                         set    Cnpj=@Cnpj,
                                                RazaoSocial=@RazaoSocial,
                                                NomeFantasia=@NomeFantasia,
                                                CodeERP=@CodERP,
                                                EmailGestorComercial=@EmailGestorComercial,
                                                EmailDiretorComercial=@EmailDiretorComercial,
                                                TipoCliente=@TipoCliente,
                                                OperadorManutencao=@OperadorManutencao,
                                                DataManutencao=@DataManutencao,
                                                StatusCliente=@StatusCliente    
                                         where id=@id ";
                    command.Parameters.AddWithValue("@id", clientEntity.Id);

                    command.Parameters.AddWithValue("@Cnpj", clientEntity.CNPJ);
                    command.Parameters.AddWithValue("@RazaoSocial", clientEntity.RazaoSocial);
                    command.Parameters.AddWithValue("@NomeFantasia", clientEntity.NomeFantasia);
                    command.Parameters.AddWithValue("@CodERP", clientEntity.CodeERP);
                    command.Parameters.AddWithValue("@EmailGestorComercial", clientEntity.EmailGestorComercial);
                    command.Parameters.AddWithValue("@EmailDiretorComercial", clientEntity.EmailDiretorComercial);

                    command.Parameters.AddWithValue("@TipoCliente", clientEntity.TipoCliente);

                    var userActive = new UserRepository().GetUserById(ActiveUser.Id);
                    command.Parameters.AddWithValue("@OperadorManutencao", userActive.Username);

                    command.Parameters.AddWithValue("@DataManutencao", clientEntity.DataManutencao);

                    command.Parameters.AddWithValue("@StatusCliente", clientEntity.StatusCliente);

                    command.CommandType = CommandType.Text;
                    result = command.ExecuteNonQuery();
                }
            }
            return result;
        }

        public ClientEntity GetClientById(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from Clientes where id=@id";
                    command.Parameters.AddWithValue("@id", id);
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var userObj = new ClientEntity
                        {
                            Id = (int)reader[0],
                            CNPJ = reader[1].ToString(),
                            RazaoSocial = reader[2].ToString(),
                            NomeFantasia = reader[3].ToString(),
                            CodeERP = reader[4].ToString(),
                            EmailGestorComercial = reader[5].ToString(),
                            EmailDiretorComercial = reader[6].ToString(),
                            TipoCliente = reader[7].ToString(),
                            OperadorManutencao = reader[8].ToString(),
                            DataManutencao = (DateTime)reader[9],
                            StatusCliente = reader[10].ToString(),
                        };
                        return userObj;
                    }
                    else
                        return null;
                }
            }
        }

        public IEnumerable<ClientEntity> GetAllClients()
        {
            var clientList = new List<ClientEntity>();//Crear lista generica de usuarios.

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from clientes order by RazaoSocial";
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())//Agregar los resultados en la lista mientras el lector siga leyendo las filas.
                        {
                            var clientObj = new ClientEntity
                            {
                                Id = (int)reader[0],
                                CNPJ = reader[1].ToString(),
                                RazaoSocial = reader[2].ToString(),
                                NomeFantasia = reader[3].ToString(),
                                CodeERP = reader[4].ToString(),
                                //IncluidoPor = reader[5].ToString(),
                                //DataInsercao = (DateTime)reader[6],
                                //AlteradoPor = reader[7].ToString(),
                                //DataAlteracao = (DateTime)reader[8],
                                //Status = (int)reader[9]
                            };
                            clientList.Add(clientObj);
                        }
                    }
                }
            }
            return clientList;
        }

        public List<ClientEntity> GetClientsByValue(string value)
        {
            var clientList = new List<ClientEntity>();

            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    string query = "select * from Clientes Where RazaoSocial like '%" + value + "%'";
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var clientObj = new ClientEntity
                            {
                                Id = (int)reader[0],
                                CNPJ = reader[1].ToString(),
                                RazaoSocial=reader[2].ToString(),
                                NomeFantasia=reader[3].ToString(),
                                CodeERP=reader[4].ToString(),
                                EmailGestorComercial=reader[5].ToString(),
                                EmailDiretorComercial=reader[6].ToString(),
                                TipoCliente=reader[7].ToString(),
                                OperadorManutencao=reader[8].ToString(),
                                DataManutencao=(DateTime)reader[9],
                                StatusCliente=reader[10].ToString()
                            };
                            clientList.Add(clientObj);
                        }
                    }
                }
            }
            return clientList;
        }
    }
}
