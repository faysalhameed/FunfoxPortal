using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortal.DataAccessLayer.Data;
using WebPortal.DataAccessLayer.Infrastructure.IRepository;

namespace WebPortal.DataAccessLayer.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        public IProgramDetailRepository ProgramDetails { get; private set; }
        public IClassRepository Classes { get; private set; }

        public IUserClassRepository UserClass { get; private set; }

        public IUserProgramDetailRepository UserProgramDetails { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ProgramDetails = new ProgramDetailRepository(context);
            Classes = new ClassRepository(context);
            UserClass = new UserClassRepository(context);
            UserProgramDetails = new UserProgramDetailRepository(context);
        }

        

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
