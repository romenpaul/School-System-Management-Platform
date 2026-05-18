using Microsoft.AspNetCore.Mvc;
using SE_1st_projects.Models;
using SE_1st_projects.Service.Interface;
using System.Diagnostics;

namespace SE_1st_projects.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserService _userService;
        private readonly IStudentService _studentService;
        private readonly IRoleService _roleService;

        public HomeController(
            ILogger<HomeController> logger,
            IUserService userService,
            IStudentService studentService,
            IRoleService roleService
        )
        {
            _logger = logger;

            _userService = userService;
            _studentService = studentService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userService.GetAll();
            var students = await _studentService.GetAllAsync();
            var roles = _roleService.GetAll();

            ViewBag.TotalUsers = users.Count;
            ViewBag.TotalStudents = students.Count();
            

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true
        )]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId =
                        Activity.Current?.Id
                        ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}