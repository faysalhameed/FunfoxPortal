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
    public class UserClassRepository : Repository<UserClasses>, IUserClassRepository
    {
        private readonly ApplicationDbContext _context;
        public UserClassRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(UserClasses userClass)
        {
            var usrcls = _context.UserClasses.Where(x => x.Id == userClass.Id).FirstOrDefault();
            if (usrcls != null)
            {
                usrcls.UserId = userClass.UserId;
                usrcls.ClassId = userClass.ClassId;

                _context.UserClasses.Update(usrcls);
            }
        }
    }
}
