using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Standartstyle.App_Code.Entities;

namespace Standartstyle.App_Code.DAL.Generic.EntityRepos
{
    public class GoodsRepository : GenericRepository<GOODS>
    {
        public GoodsRepository(StandartstyleEntities context) : base(context) { }
    }
}