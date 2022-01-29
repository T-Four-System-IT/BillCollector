using DataAccess.DBServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBServices.Interfaces
{
    interface IClientRepository
    {
        int CreateClient(ClientEntity user);
        int ModifyClient(ClientEntity user);
        ClientEntity GetClientById(int id);
        IEnumerable<ClientEntity> GetAllClients();
        List<ClientEntity> GetClientsByValue(string value);
    }
}
