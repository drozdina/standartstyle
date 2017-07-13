using Standartstyle.AppCode.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.AppCode.DAL.Repository.EntityRepositories
{
    public class ReplyRepository : GenericRepository<REPLY>
    {
        public ReplyRepository(Entities context) : base(context) { }
    }
}