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
            return Ok(userManager.Users.ToList());
        }

        [HttpGet("GetUsersWithClaims")]
        public async Task<IActionResult> GetUsersWithClaims()
        {
            var users = userManager.Users.ToList();
            var usersAndClaims = new List<UserAndClaimsVM>();
            foreach(var user in users)
            {
                var claims = await userManager.GetClaimsAsync(user);
                usersAndClaims.Add(new UserAndClaimsVM
                {
                    User = user,
                    Claims = claims.ToList()
                       
                });
            }
         
            return Ok(usersAndClaims);            
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {

                await signInManager.SignInAsync(user,true);
                return Ok(String.Format("{0} is signed in, registered age is {1}",userName, user.Age));
            }
            return NotFound();
            
        }
       
    }
}
