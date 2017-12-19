using NewsSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NewsSite.Controllers
{

    [Route("check")]
    public class CheckController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public CheckController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpGet, Route("view/OpenNews")]
        public IActionResult ViewOpenNews()
        {
            return Ok();
        }

        // TODO: Skapa en Policy i Startup.cs och avkommentera sedan nedan
        // Controllern behöver inte innehålla någon mer kod

        //[Authorize(Policy = "HiddenNews")]
        //[HttpGet, Route("view/HiddenNews")]
        //public IActionResult ViewHiddenNews()
        //{
        //    return Ok();
        //}

        [HttpGet, Route("view/roles")]
        public async Task AddRolesToDataBase()
        {

            string[] roleNames = { "Administrator", "Publisher", "Subscriber" };

            foreach(var role in roleNames)
            {
                var adminRole = await roleManager.FindByNameAsync(role);
                if (adminRole == null)
                {
                    adminRole = new IdentityRole(role);
                    await roleManager.CreateAsync(adminRole);
                }
            }           
        }
}
}
