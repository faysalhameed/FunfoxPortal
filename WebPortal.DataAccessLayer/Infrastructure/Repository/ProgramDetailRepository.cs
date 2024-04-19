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
    public class ProgramDetailRepository : Repository<ProgramDetail>, IProgramDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public ProgramDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ProgramDetail programDetail)
        {

            var proDetail = _context.ProgramDetails.Where(x => x.ProgramId == programDetail.ProgramId).FirstOrDefault();
            if (proDetail != null)
            {
                proDetail.ProgramName = proDetail.ProgramName;
                
                _context.ProgramDetails.Update(proDetail);
            }
        }
    }
}
