using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Models.ViewModels
{
    public class ClassVM
    {

        public Classes Cls { get; set; } = new Classes();

        public List<SelectListItem> ProgramList = new List<SelectListItem>();

    }
}
