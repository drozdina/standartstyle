using Standartstyle.App_Code.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.App_Code.DAL.Generic.EntityRepos
{
    public class ImageRepository : GenericRepository<IMAGE>
    {
        public ImageRepository(StandartstyleEntities context) : base(context) { }
    }
}