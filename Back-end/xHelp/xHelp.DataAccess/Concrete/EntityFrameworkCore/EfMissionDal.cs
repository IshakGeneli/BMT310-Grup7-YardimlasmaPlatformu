﻿using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Core.DataAccess.EntityFrameworkCore;
using xHelp.DataAccess.Abstract;
using xHelp.Entity.Concrete;

namespace xHelp.DataAccess.Concrete.EntityFrameworkCore
{
    public class EfMissionDal : EfEntityRepositoryBase<Mission, xHelpContext>, IMissionDal
    {
    }
}