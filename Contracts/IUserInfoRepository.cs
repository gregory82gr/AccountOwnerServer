using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserInfoRepository : IRepositoryBase<UserInfo>
    {
        UserInfo GetUser(string email, string password);

        UserInfo GetUserByUserName(string username);

        void UpdateUser(UserInfo userInfo);
    }
}
