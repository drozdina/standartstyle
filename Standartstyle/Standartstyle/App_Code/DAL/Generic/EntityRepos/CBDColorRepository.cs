using Standartstyle.App_Code.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.App_Code.DAL.Generic.EntityRepos
{
    public class CBDColorRepository : GenericRepository<CBD_COLOR>
    {
        public CBDColorRepository(StandartstyleEntities context) : base(context) { }
    }
}