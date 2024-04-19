using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Models.ViewModels
{
    public class ProgramDetailsVM
    {

        public UserProgramDetail UserProgramDetail { get; set; } = new UserProgramDetail();

        public List<SelectListItem> ProgramDetailList = new List<SelectListItem>();

    }
}
