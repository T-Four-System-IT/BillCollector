using DataAccess.DBServices;
using DataAccess.DBServices.Entities;
using DataAccess.MailServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class EmailTemplateModel
    {
        #region -> Atributos
        private int _id;
        private string _descricao;
        private string _assunto;
        private string _paragrafo1;
        private string _paragrafo2;
        private string _paragrafo3;

        private EmailTemplateRepository _emailTemplateRepository;
        #endregion

        #region -> Constructores
        public EmailTemplateModel()
        {
            _emailTemplateRepository = new EmailTemplateRepository();
        }

        public EmailTemplateModel(int id, string descricao, string assunto, string paragrafo1, string paragrafo2, string paragrafo3)
        {
            Id = id;
            Descricao = descricao;
            Assunto = assunto;
            Paragrafo1 = paragrafo1;
            Paragrafo2 = paragrafo2;
            Paragrafo3 = paragrafo3;

            _emailTemplateRepository = new EmailTemplateRepository();
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
        [DisplayName("Descrição Template")]
        [Required(ErrorMessage = "Informe a descrição do Template.")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "A descrição deve conter um mínimo de 10 caracteres.")]
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        //Posição 2
        [DisplayName("Assunto")]
        [Required(ErrorMessage = "Informe o assunto do Template.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "O assunto deve conter um mínimo de 5 caracteres.")]
        [Browsable(false)]
        public string Assunto
        {
            get { return _assunto; }
            set { _assunto = value; }
        }

        //Posição 3
        [DisplayName("Paragrafo 1")]
        [Required(ErrorMessage = "Informe o primeiro parágrafo do Template.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "O primeiro paragrafo deve conter um mínimo de 5 caracteres.")]
        [Browsable(false)]
        public string Paragrafo1
        {
            get { return _paragrafo1; }
            set { _paragrafo1 = value; }
        }


        //Posição 4
        [DisplayName("Paragrafo 2")]
        [Required(ErrorMessage = "Informe o segundo parágrafo do Template.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "O segundo paragrafo deve conter um mínimo de 5 caracteres.")]
        [Browsable(false)]
        public string Paragrafo2
        {
            get { return _paragrafo2; }
            set { _paragrafo2 = value; }
        }


        //Posição 5
        [DisplayName("Paragrafo 3")]
        [Required(ErrorMessage = "Informe o terceiro parágrafo do Template.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "O terceiro paragrafo deve conter um mínimo de 5 caracteres.")]
        [Browsable(false)]
        public string Paragrafo3
        {
            get { return _paragrafo3; }
            set { _paragrafo3 = value; }
        }
        #endregion


        #region -> Métodos Públicos
        public int CreateEmailTemplate()
        {
            EmailTemplateEntity emailTemplateEntity = MapModelToEntity(this);
            return _emailTemplateRepository.CreateEmailTemplate(emailTemplateEntity);
        }

        public int ModifyEmailTemplate()
        {
            EmailTemplateEntity emailTemplateEntity = MapModelToEntity(this);
            return _emailTemplateRepository.ModifyEmailTemplate(emailTemplateEntity);
        }

        public int RemoveEmailTemplate(int id)
        {
            //return _emailTemplateRepository.RemoveEmailTemplate(id);
            return -1;
        }

        public EmailTemplateModel GetEmailTemplateById(int id)
        {
            var result = _emailTemplateRepository.GetEmailTemplateById(id);
            if (result != null)
                return MapEntityToModel(result);
            else
                return null;
        }

        public IEnumerable<EmailTemplateModel> GetAllEmailTemplates()
        {
            IEnumerable<EmailTemplateEntity> result = _emailTemplateRepository.GetAllEmailTemplate();
            return MapEntityToModel(result);
        }

        public IEnumerable<EmailTemplateModel> GetByValue(string value)
        {
            var emailTemplateEntityList = _emailTemplateRepository.GetEmailTemplateByValue(value);
            return MapEntityToModel(emailTemplateEntityList);
        }
        #endregion

        #region -> Métodos Privados (Mapear dados)
        private EmailTemplateEntity MapModelToEntity(EmailTemplateModel emailTemplateModel)
        {
            var emailTemplateEntity = new EmailTemplateEntity
            {
                Id = emailTemplateModel.Id,
                Descricao = emailTemplateModel.Descricao,
                Assunto = emailTemplateModel.Assunto,
                Paragrafo1 = emailTemplateModel.Paragrafo1,
                Paragrafo2 = emailTemplateModel.Paragrafo2,
                Paragrafo3 = emailTemplateModel.Paragrafo3,
            };
            return emailTemplateEntity;
        }

        private EmailTemplateModel MapEntityToModel(EmailTemplateEntity emailTemplateEntity)
        {
            var emailTemplateModel = new EmailTemplateModel()
            {
                Id = emailTemplateEntity.Id,
                Descricao = emailTemplateEntity.Descricao,
                Assunto = emailTemplateEntity.Assunto,
                Paragrafo1 = emailTemplateEntity.Paragrafo1,
                Paragrafo2 = emailTemplateEntity.Paragrafo2,
                Paragrafo3 = emailTemplateEntity.Paragrafo3,
            };
            return emailTemplateModel;
        }

        private IEnumerable<EmailTemplateModel> MapEntityToModel(IEnumerable<EmailTemplateEntity> emailTemplateEntityList)
        {
            List<EmailTemplateModel> emailTemplateModelList = new List<EmailTemplateModel>();

            foreach (var item in emailTemplateEntityList)
            {
                var emailTemplateModel = new EmailTemplateModel()
                {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Assunto = item.Assunto,
                    Paragrafo1 = item.Paragrafo1,
                    Paragrafo2 = item.Paragrafo2,
                    Paragrafo3 = item.Paragrafo3,
                };
                emailTemplateModelList.Add(emailTemplateModel);
            }
            return emailTemplateModelList;
        }

        //private IEnumerable<EmailTemplateModel> MapEmailTemplateModel(IEnumerable<EmailTemplateEntity> emailTemplateEntities)
        //{
        //    //var emailTemplateModels = new List<EmailTemplateModel>();
        //    //foreach (var emailTemplate in emailTemplateEntities)
        //    //{
        //    //    emailTemplateModels.Add(MapEmailTemplateModel(emailTemplate));
        //    //}
        //    //return emailTemplateModels;
        //}

        #endregion
    }
}
