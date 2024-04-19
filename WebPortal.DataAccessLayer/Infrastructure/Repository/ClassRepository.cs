using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortal.DataAccessLayer.Data;
using WebPortal.DataAccessLayer.Infrastructure.IRepository;
using WebPortal.Models;

namespace WebPortal.DataAccessLayer.Infrastructure.Repository
{
    public class ClassRepository : Repository<Classes>, IClassRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Classes cls)
        {
            var clss = _context.Classes.Where(x => x.ClassId == cls.ClassId).FirstOrDefault();
            if (clss != null)
            {
                clss.ClassName = cls.ClassName;
                clss.ProgramId = cls.ProgramId;
                clss.Level = cls.Level;
                clss.StartTime = cls.StartTime;
                clss.EndTime = cls.EndTime;
                clss.MaximumSize = cls.MaximumSize;

                _context.Classes.Update(clss);
            }
        }
    }
}
