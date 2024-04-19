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
    public class UserProgramDetailRepository : Repository<UserProgramDetail>, IUserProgramDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public UserProgramDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(UserProgramDetail userProgramDetail)
        {
            var usrProDetail = _context.UserProgramDetails.Where(x => x.UserId == userProgramDetail.UserId).FirstOrDefault();
            if (usrProDetail != null)
            {
                usrProDetail.UserId = userProgramDetail.UserId;
                usrProDetail.ProgramDetailId = userProgramDetail.ProgramDetailId;

                _context.UserProgramDetails.Update(usrProDetail);
            }
        }
    }
}
