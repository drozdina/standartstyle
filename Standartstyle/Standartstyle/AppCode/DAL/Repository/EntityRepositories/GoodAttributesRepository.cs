﻿using Standartstyle.AppCode.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standartstyle.AppCode.DAL.Repository.EntityRepositories
{
    public class GoodAttributesRepository : GenericRepository<GOOD_ATTRIBUTES>
    {
        public GoodAttributesRepository(Entities context) : base(context) { }
    }
}