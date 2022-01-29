using Common;
using DataAccess.DBServices.Entities;
using DataAccess.DBServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.DBServices
{
    public class EmailTemplateRepository : ConnectionToSql, IEmailTemplateRepository
    {
        public int CreateEmailTemplate(EmailTemplateEntity emailTemplate)
        {
            int result = -1;

            using (var connection = GetConnection())
            {
                connection.Open();
                var userActive = new UserRepository().GetUserById(ActiveUser.Id);

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"insert into EmailTemplate values (@Descricao,@Assunto,@Paragrafo1,@Paragrafo2,@Paragrafo3)";
                    command.Parameters.AddWithValue("@Descricao", emailTemplate.Descricao);
                    command.Parameters.AddWithValue("@Assunto", emailTemplate.Assunto);
                    command.Parameters.AddWithValue("@Paragrafo1", emailTemplate.Paragrafo1);
                    command.Parameters.AddWithValue("@Paragrafo2", emailTemplate.Paragrafo2);
                    command.Parameters.AddWithValue("@Paragrafo3", emailTemplate.Paragrafo3);

                    command.CommandType = CommandType.Text;
                    result = command.ExecuteNonQuery();
                }
            }
            return result;
        }

        public IEnumerable<EmailTemplateEntity> GetAllEmailTemplate()
        {
            var emailTemplateList = new List<EmailTemplateEntity>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from EmailTemplate order by Descricao";
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var emailTemplateObj = new EmailTemplateEntity
                            {
                                Id = (int)reader[0],
                                Descricao = reader[1].ToString(),
                                Assunto = reader[2].ToString(),
                                Paragrafo1 = reader[3].ToString(),
                                Paragrafo2 = reader[4].ToString(),
                                Paragrafo3 = reader[5].ToString()

                            };
                            emailTemplateList.Add(emailTemplateObj);
                        }
                    }
                }
            }
            return emailTemplateList;
        }

        public EmailTemplateEntity GetEmailTemplateById(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from EmailTemplate where id=@id";
                    command.Parameters.AddWithValue("@id", id);
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        var emailTemplateObj = new EmailTemplateEntity
                        {
                            Id = (int)reader[0],
                            Descricao = reader[1].ToString(),
                            Assunto = reader[2].ToString(),
                            Paragrafo1 = reader[3].ToString(),
                            Paragrafo2 = reader[4].ToString(),
                            Paragrafo3 = reader[5].ToString()
                        };
                        return emailTemplateObj;
                    }
                    else
                        return null;
                }
            }
        }

        public EmailTemplateEntity GetEmailTemplateByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<EmailTemplateEntity> GetEmailTemplateByValue(string value)
        {
            var emailTemplateList = new List<EmailTemplateEntity>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    string query = "select * from EmailTemplate Where Descricao like '%" + value + "%'";
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var emailTemplateObj = new EmailTemplateEntity
                            {
                                Id = (int)reader[0],
                                Descricao = reader[1].ToString(),
                                Assunto = reader[2].ToString(),
                                Paragrafo1 = reader[3].ToString(),
                                Paragrafo2 = reader[4].ToString(),
                                Paragrafo3 = reader[5].ToString()
                            };
                            emailTemplateList.Add(emailTemplateObj);
                        }
                    }
                }
            }
            return emailTemplateList;
        }

        public int ModifyEmailTemplate(EmailTemplateEntity emailTemplate)
        {
            int result = -1;

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @" update  EmailTemplate
                                             set     Descricao=@Descricao,
                                                     Assunto=@Assunto,
                                                     Paragrafo1=@Paragrafo1,
                                                     Paragrafo2=@Paragrafo2,  
                                                     Paragrafo3=@Paragrafo3  
                                             where   id=@id ";
                    command.Parameters.AddWithValue("@id", emailTemplate.Id);
                    command.Parameters.AddWithValue("@Descricao", emailTemplate.Descricao);
                    command.Parameters.AddWithValue("@Assunto", emailTemplate.Assunto);
                    command.Parameters.AddWithValue("@Paragrafo1", emailTemplate.Paragrafo1);
                    command.Parameters.AddWithValue("@Paragrafo2", emailTemplate.Paragrafo2);
                    command.Parameters.AddWithValue("@Paragrafo3", emailTemplate.Paragrafo3);

                    command.CommandType = CommandType.Text;
                    result = command.ExecuteNonQuery();
                }
            }
            return result;
        }
    }
}

