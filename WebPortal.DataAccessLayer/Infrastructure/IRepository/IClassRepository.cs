using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Models;

namespace WebPortal.DataAccessLayer.Infrastructure.IRepository
{
    public interface IClassRepository : IRepository<Classes>
    {
        void Update(Classes cls);
    }
}
