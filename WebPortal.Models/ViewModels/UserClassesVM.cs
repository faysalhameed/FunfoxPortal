using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Models.ViewModels
{
    public class UserClassesVM
    {

        public IEnumerable<UserClasses> UserClasses = new List<UserClasses>();

        public List<SelectListItem> ClassesList = new List<SelectListItem>();

        public UserClasses UserClass { get; set; } = new UserClasses();

    }
}
