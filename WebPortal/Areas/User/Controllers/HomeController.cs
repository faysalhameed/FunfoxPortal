using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPortal.DataAccessLayer.Infrastructure.IRepository;
using WebPortal.Models;
using static System.Reflection.Metadata.BlobBuilder;
using WebPortal.DataAccessLayer.Data;
using WebPortal.Models.ViewModels;

namespace WebPortal.Areas.User.Controllers
{

    [Area("User")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);


            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Index(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {

                var usr = await _userManager.GetUserAsync(User);

                usr.FirstName = user.FirstName;
                usr.LastName = user.LastName;
                usr.Email = user.Email;
                usr.UserName = user.Email;

                var result = await _userManager.UpdateAsync(usr);

                if (result.Succeeded)
                {
                    TempData["success"] = "Profile Updated";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Something went wrong";
                    return RedirectToAction("Index");
                }

                
            }

            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> Classes()
        {

            UserClassesVM vm = new UserClassesVM();

            var user = await _userManager.GetUserAsync(User);

            vm.UserClass.UserId = user.Id;

            vm.UserClasses = _unitOfWork.UserClass.GetAll("Class").Where(x => x.UserId == user.Id);

            var classIds = _context.UserClasses.Where(x => x.UserId == user.Id).Select(x => x.ClassId).ToList();

            var classes = _unitOfWork.Classes.GetAll();

            foreach (var cls in classes)
            {
                if (!classIds.Contains(cls.ClassId))
                {
                    vm.ClassesList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Text = cls.ClassName,
                        Value = cls.ClassId.ToString()
                    });
                }
            }

            return View(vm);

        }


        [HttpPost]
        public IActionResult Classes(UserClassesVM vm)
        {

            var cls = _unitOfWork.Classes.Get(x => x.ClassId == vm.UserClass.ClassId);

            var users = _unitOfWork.UserClass.GetAll().Where(x => x.ClassId == vm.UserClass.ClassId).ToList();

            if (users.Count() < cls.MaximumSize)
            {
                _unitOfWork.UserClass.Add(vm.UserClass);
                _unitOfWork.Save();
                return RedirectToAction("Classes");
            }
            else
            {
                TempData["error"] = "Class Size Limit Reached";
                return RedirectToAction("Classes");
            }
        }



        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ProfileVM vm)
        {

            if (vm.Password.NewPassword != null && vm.Password.ConfirmPassword != null)
            {
                if (vm.Password.NewPassword == vm.Password.ConfirmPassword)
                {
                    var user = await _userManager.GetUserAsync(User);

                    var result = await _userManager.ChangePasswordAsync(user, vm.Password.OldPassword, vm.Password.NewPassword);
                    if (result.Succeeded)
                    {
                        TempData["success"] = "Password Changed Successfully";
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        TempData["error"] = "Something went wrong";
                    }
                }
            }

            return RedirectToAction("Profile");

        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
