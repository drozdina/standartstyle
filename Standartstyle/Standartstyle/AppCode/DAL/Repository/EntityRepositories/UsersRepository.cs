using Standartstyle.AppCode.DAL.Model;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.AppCode.DAL.Repository.EntityRepositories
{
    public class UsersRepository : GenericRepository<USERS>
    {
        public UsersRepository(Entities context) : base(context) { }

        public USERS Login(string login, string password)
        {
            return this.All.FirstOrDefault(elem => elem.USERNAME.Equals(login) && elem.PASSWORD.Equals(password));
        }

        public UserModel CheckUser(string login)
        {
            UserModel existingUser = null;
            var user = this.All.FirstOrDefault(elem => elem.USERNAME.Equals(login));
            if (user != null)
            {
                existingUser = new UserModel
                {
                    Code = user.USERCODE,
                    Name = user.USERNAME,
                    Password = user.PASSWORD
                };
            }
            return existingUser;
        }
    }
}