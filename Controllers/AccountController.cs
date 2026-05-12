using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public AccountController(IUserService userService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }

    // ================= LOGIN =================
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _userService.AuthenticateAsync(username, password);

        if (user != null)
        {
            var role = _roleService.GetById(user.RoleId);
            var roleName = role?.RoleName ?? "User";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, roleName)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity)
            );

            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Invalid username or password";
        return View();
    }

    // ================= REGISTER =================
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(User user)
    {
        user.RoleId = 2; // default user role

        var result = await _userService.RegisterAsync(user);

        if (result)
            return RedirectToAction("Login");

        ViewBag.Error = "User already exists";
        return View();
    }

    // ================= LOGOUT =================
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        return RedirectToAction("Login");
    }

    // ================= SETUP (FIXED) =================
    [HttpGet]
    public async Task<IActionResult> Setup()
    {
        var roles = _roleService.GetAll();

        // Create Admin Role
        if (!roles.Any(r => r.RoleName == "Admin"))
        {
            _roleService.Add(new Role
            {
                RoleName = "Admin",
                RoleDescription = "Administrator Role"
            });
        }

        // Create User Role
        if (!roles.Any(r => r.RoleName == "User"))
        {
            _roleService.Add(new Role
            {
                RoleName = "User",
                RoleDescription = "Standard User Role"
            });
        }

        roles = _roleService.GetAll();
        var adminRole = roles.First(r => r.RoleName == "Admin");

        // Create Admin User
        var users = _userService.GetAll();

        if (!users.Any(u => u.UserName == "admin"))
        {
            await _userService.RegisterAsync(new User
            {
                UserName = "admin",
                Email = "admin@system.com",
                PasswordHash = "admin123",
                Address = "System Admin",
                RoleId = adminRole.Id
            });
        }

        return Content("Setup Complete! Admin Created Successfully.");
    }
}