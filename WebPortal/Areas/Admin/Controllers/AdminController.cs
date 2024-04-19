using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPortal.CommonHelper;
using WebPortal.DataAccessLayer.Data;
using WebPortal.DataAccessLayer.Infrastructure.IRepository;
using WebPortal.Models;
using WebPortal.Models.ViewModels;

namespace WebPortal.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public AdminController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            IUnitOfWork unitOfWork,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpGet]
        [Route("/Admin/Register")]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [Route("/Admin/Register")]
        public async Task<IActionResult> Register(AdminRegister register)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser();

                await _userStore.SetUserNameAsync(user, register.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, register.Email, CancellationToken.None);

                user.FirstName = register.FirstName;
                user.LastName = register.LastName;

                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, WebsiteRoles.Role_Admin);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("/");
                }

            }

            return View(register);
        }

        [HttpGet]
        [Route("/Admin/Login")]
        public IActionResult Login()
        {

            return View();

        }


        [HttpPost]
        [Route("/Admin/Login")]
        public async Task<IActionResult> Login(AdminLogin login)
        {

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, lockoutOnFailure: false);
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(login);
            }

        }

        [Route("/Admin/Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            return View(user);
        }


        [Route("/Admin/Index")]
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
        public IActionResult ProgramDetail()
        {
            IEnumerable<ProgramDetail> programDetails = _unitOfWork.ProgramDetails.GetAll();
            return View(programDetails);
        }




        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();
            var usr = new List<ApplicationUser>();
            foreach(var user in users)
            {
                var result = await _userManager.IsInRoleAsync(user, WebsiteRoles.Role_User);

                if (result)
                {
                    usr.Add(user);
                }

            }

            return View(usr);
        }

        [HttpGet]
        public async Task<IActionResult> UserClasses(string username)
        {

            UserClassesVM vm = new UserClassesVM();

            var user = await _userManager.FindByNameAsync(username);

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
        public IActionResult UserClasses(UserClassesVM vm)
        {

            var cls = _unitOfWork.Classes.Get(x => x.ClassId == vm.UserClass.ClassId);

            var users = _unitOfWork.UserClass.GetAll().Where(x => x.ClassId == vm.UserClass.ClassId).ToList();

            if(users.Count() < cls.MaximumSize)
            {
                _unitOfWork.UserClass.Add(vm.UserClass);
                _unitOfWork.Save();
                return RedirectToAction("Users");
            }
            else
            {
                TempData["error"] = "Class Size Limit Reached";
                return RedirectToAction("Users");
            }
        }

        public IActionResult DeleteUserClass(int id)
        {
            var userClass = _unitOfWork.UserClass.Get(x => x.Id == id);
            _unitOfWork.UserClass.Delete(userClass);
            _unitOfWork.Save();
            return RedirectToAction("Users");

        }

        [HttpGet]
        public async Task<IActionResult> UserProgramDetail(string username)
        {

            ProgramDetailsVM vm = new ProgramDetailsVM();

            var user = await _userManager.FindByNameAsync(username);

            vm.UserProgramDetail = _unitOfWork.UserProgramDetails.Get(x => x.UserId == user.Id, "User,ProgramDetail");

            var programDetails = _unitOfWork.ProgramDetails.GetAll();

            if (vm.UserProgramDetail == null)
            {
                vm.UserProgramDetail = new Models.UserProgramDetail();
                vm.UserProgramDetail.UserId = user.Id;

                foreach (var programDetail in programDetails)
                {
                    vm.ProgramDetailList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Text = programDetail.ProgramName,
                        Value = programDetail.ProgramId.ToString()
                    });
                }
            }
            else
            {
                foreach (var programDetail in programDetails)
                {
                    if (programDetail.ProgramId != vm.UserProgramDetail.ProgramDetailId)
                    {
                        vm.ProgramDetailList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                        {
                            Text = programDetail.ProgramName,
                            Value = programDetail.ProgramId.ToString()
                        });
                    }
                }
            }



            return View(vm);

        }


        [HttpPost]
        public IActionResult UserProgramDetail(ProgramDetailsVM vm)
        {
            var exist = _unitOfWork.UserProgramDetails.Get(x => x.UserId == vm.UserProgramDetail.UserId);

            if (exist == null)
            {
                _unitOfWork.UserProgramDetails.Add(vm.UserProgramDetail);
            }
            else
            {
                _unitOfWork.UserProgramDetails.Update(vm.UserProgramDetail);
            }
            _unitOfWork.Save();
            return RedirectToAction("Users");
        }


        [HttpGet]
        public IActionResult AddProgramDetail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProgramDetail(ProgramDetail programDetail)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProgramDetails.Add(programDetail);
                _unitOfWork.Save();
                return RedirectToAction("ProgramDetail");
            }

            return View(programDetail);
        }

        [HttpGet]
        public IActionResult DeleteProgramDetail(int id)
        {
            var programDetail = _unitOfWork.ProgramDetails.Get(x => x.ProgramId == id);
            return View(programDetail);
        }

        [HttpPost]
        public IActionResult DeleteProgramDetail(ProgramDetail programDetail)
        {

            var proDetail = _unitOfWork.ProgramDetails.Get(x => x.ProgramId == programDetail.ProgramId);
            _unitOfWork.ProgramDetails.Delete(proDetail);
            _unitOfWork.Save();
            return RedirectToAction("ProgramDetail");
        }


        [HttpGet]
        public IActionResult Classes()
        {
            IEnumerable<Classes> classes = _unitOfWork.Classes.GetAll();
            return View(classes);
        }

        [HttpGet]
        public IActionResult AddClass()
        {
            ClassVM vm = new ClassVM();

            var programs = _unitOfWork.ProgramDetails.GetAll();

            foreach(var program in programs)
            {
                vm.ProgramList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = program.ProgramName,
                    Value = program.ProgramId.ToString()
                });
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddClass(ClassVM vm)
        {

            if (ModelState.IsValid)
            {

                if(vm.Cls.StartTime < vm.Cls.EndTime)
                {

                    var checkOverlap = _unitOfWork.Classes.GetAll().Where(x => x.ProgramId == vm.Cls.ProgramId && x.Level == vm.Cls.Level).ToList();

                    var chk = true;

                    foreach(var check in checkOverlap)
                    {

                        if(!(vm.Cls.EndTime < check.StartTime || check.EndTime < vm.Cls.StartTime))
                        {
                            chk = false;
                            break;
                        }

                    }

                    if (chk)
                    {
                        _unitOfWork.Classes.Add(vm.Cls);
                        _unitOfWork.Save();
                        return RedirectToAction("Classes");
                    }
                    else
                    {
                        TempData["error"] = "Class Timing Overlapped";
                        return RedirectToAction("Classes");
                    }

                    
                }
                else
                {

                    var programs = _unitOfWork.ProgramDetails.GetAll();
                    foreach (var program in programs)
                    {
                        vm.ProgramList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                        {
                            Text = program.ProgramName,
                            Value = program.ProgramId.ToString()
                        });
                    }

                    TempData["error"] = "Start time should be less than End Time";
                    return View(vm);
                }
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult EditClass(int id)
        {
            ClassVM vm = new ClassVM();

            vm.Cls = _unitOfWork.Classes.Get(x => x.ClassId == id);

            var programs = _unitOfWork.ProgramDetails.GetAll();

            foreach (var program in programs)
            {
                vm.ProgramList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = program.ProgramName,
                    Value = program.ProgramId.ToString()
                });
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult EditClass(ClassVM vm)
        {

            if (ModelState.IsValid)
            {

                if (vm.Cls.StartTime < vm.Cls.EndTime)
                {
                    _unitOfWork.Classes.Update(vm.Cls);
                    _unitOfWork.Save();
                    return RedirectToAction("Classes");
                }
                else
                {

                    var programs = _unitOfWork.ProgramDetails.GetAll();
                    foreach (var program in programs)
                    {
                        vm.ProgramList.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                        {
                            Text = program.ProgramName,
                            Value = program.ProgramId.ToString()
                        });
                    }

                    TempData["error"] = "Start time should be less than End Time";
                    return View(vm);
                }


            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult DeleteClass(int id)
        {
            DeleteClassVM vm = new DeleteClassVM();

            vm.Cls = _unitOfWork.Classes.Get(x => x.ClassId == id,"Program");

            var users = _unitOfWork.UserClass.GetAll().Where(x => x.ClassId == id);

            if(users.Count() > 0)
            {
                vm.UserExists = true;
            }

            return View(vm);
        }

        [Route("/Admin/DeleteClass/{id}")]
        public IActionResult DelClass(int id)
        {
            var clss = _unitOfWork.Classes.Get(x => x.ClassId == id);
            
            if(clss != null)
            {
                _unitOfWork.Classes.Delete(clss);
                _unitOfWork.Save();

                return Json(new { success = true, message = "Class Deleted Successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Class Not Found" });
            }
        }


        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Report()
        {
            var ClassList = new List<SelectListItem>();

            var classes = _unitOfWork.Classes.GetAll();

            foreach(var cls in classes)
            {

                ClassList.Add(new SelectListItem
                {
                    Text = cls.ClassName,
                    Value = cls.ClassId.ToString()
                });

            }

            return View(ClassList);
        }



        public async Task<IActionResult> ReportGet(int id)
        {

            var userIds = _unitOfWork.UserClass.GetAll().Where(x => x.ClassId == id).Select(x => x.UserId);

            var users = new List<ApplicationUser>();

            foreach(var Id in userIds)
            {

                users.Add(await _userManager.FindByIdAsync(Id));

            }

            var count = users.Count();

            return Json(new { users, count });

        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ProfileVM vm)
        {

            if(vm.Password.NewPassword !=null && vm.Password.ConfirmPassword != null)
            {
                if(vm.Password.NewPassword == vm.Password.ConfirmPassword)
                {
                    var user =await _userManager.GetUserAsync(User);

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



        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
