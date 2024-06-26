﻿using Contracts;
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


        public IEnumerable<UserInfo> GetAllUsers()
        {
            return FindAll()
                .OrderBy(ow => ow.UserName)
                .ToList();
        }

        public UserInfo GetUser(string email, string password)
        {
            return  FindByCondition(account => account.Email.Equals(email) && account.Password.Equals(password)).FirstOrDefault();
            
        }

        public UserInfo GetUserByUserName(string username)
        {
            return FindByCondition(account => account.UserName.Equals(username)).FirstOrDefault();

        }

        public void UpdateUser(UserInfo userInfo) => Update(userInfo);
    }
}
