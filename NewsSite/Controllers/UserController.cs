using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsSite.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            //Todo vymodell med bara email
            return Ok(userManager.Users.ToList());
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {

                await signInManager.SignInAsync(user,true);
                return Ok(String.Format("{0} is signed in",userName));
            }
            return NotFound();
            
        }
       
    }
}
