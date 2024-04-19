using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.DataAccessLayer.Infrastructure.IRepository
{
    public interface IUnitOfWork
    {
        IProgramDetailRepository ProgramDetails { get; }

        IClassRepository Classes { get; }

        IUserClassRepository UserClass { get; }

        IUserProgramDetailRepository UserProgramDetails { get; }

        void Save();
    }
}
