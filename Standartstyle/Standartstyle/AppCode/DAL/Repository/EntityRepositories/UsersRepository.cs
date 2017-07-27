using Standartstyle.AppCode.DAL.Model;
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
    }
}