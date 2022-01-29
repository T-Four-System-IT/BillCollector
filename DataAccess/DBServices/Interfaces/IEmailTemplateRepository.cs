using DataAccess.DBServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBServices.Interfaces
{
    interface IEmailTemplateRepository
    {
        int CreateEmailTemplate(EmailTemplateEntity emailTemplate);
        int ModifyEmailTemplate(EmailTemplateEntity emailTemplate);
        EmailTemplateEntity GetEmailTemplateById(int id);
        EmailTemplateEntity GetEmailTemplateByName(string name);
        IEnumerable<EmailTemplateEntity> GetAllEmailTemplate();
        List<EmailTemplateEntity> GetEmailTemplateByValue(string value);
    }
}
