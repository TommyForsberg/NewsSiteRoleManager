using NewsSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Linq;

namespace NewsSite.Controllers
{

    [Route("news")]
    public class NewsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
       

        public NewsController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpGet, Route("OpenNews")]
        [AllowAnonymous]
        public IActionResult ViewOpenNews()
        {
            return Ok("Open news!");
        }

        [Authorize(Policy = "HiddenNews")]
        [HttpGet, Route("HiddenNews")]
        public IActionResult ViewHiddenNews()
        {
            return Ok("Hidden news!");
        }

        [Authorize(Policy = "MinimumAgePolicy")]
        [Authorize(Policy = "HiddenNews")]
        [HttpGet, Route("adultnews")]
        public IActionResult ViewAdultNews()
        {
            return Ok("Adult News");
        }

        [HttpGet, Route("publishsports")]
        [Authorize(Policy = "SportsPublisher")]
        public IActionResult PublishSports()
        {
            return Ok("Your sports article was published!");
        }

        [HttpGet, Route("publishculture")]
        [Authorize(Policy = "CulturePublisher")]
        public IActionResult PublishCulture()
        {
            return Ok("Your culture article was published!");
        }
    }
}
