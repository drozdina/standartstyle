using Standartstyle.AppCode.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.AppCode.DAL.Repository.EntityRepositories
{
    public class GoodsCategoryRepository : GenericRepository<GOODS_CATEGORY>
    {
        public GoodsCategoryRepository(Entities context) : base(context) { }
    }
}