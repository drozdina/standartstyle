using Standartstyle.AppCode.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.AppCode.DAL.Repository.EntityRepositories
{
    public class AttributesRepository : GenericRepository<ATTRIBUTES>
    {
        public AttributesRepository(Entities context) : base(context) { }
    }
}