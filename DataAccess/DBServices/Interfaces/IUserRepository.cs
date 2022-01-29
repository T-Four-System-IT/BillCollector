using DataAccess.DBServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBServices.Interfaces
{
    interface IUserRepository
    {
        UserEntity Login(string username, string password);
        bool ValidateActiveUser();
        int CreateUser(UserEntity user);
        int ModifyUser(UserEntity user);
        int RemoveUser(int id);
        UserEntity GetUserById(int id);
        UserEntity GetUserByUsername(string user);
        IEnumerable<UserEntity> GetAllUsers();
    }
}
