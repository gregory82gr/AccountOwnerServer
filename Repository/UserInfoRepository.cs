using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserInfoRepository:RepositoryBase<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }


        public UserInfo GetUser(string email, string password)
        {
            return  FindByCondition(account => account.Email.Equals(email) && account.Password.Equals(password)).FirstOrDefault();
            
        }
    }
}
