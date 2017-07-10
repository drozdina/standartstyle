using Standartstyle.App_Code.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.App_Code.DAL.Generic.EntityRepos
{
    public class CBDManufacturerRepository : GenericRepository<CBD_MANUFACTURER>
    {
        public CBDManufacturerRepository(StandartstyleEntities context) : base(context) { }
    }
}