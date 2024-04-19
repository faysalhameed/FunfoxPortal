﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Models;

namespace WebPortal.DataAccessLayer.Infrastructure.IRepository
{
    public interface IProgramDetailRepository : IRepository<ProgramDetail>
    {
        void Update(ProgramDetail programDetail);
    }
}
