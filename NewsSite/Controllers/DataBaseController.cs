﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsSite.Controllers
{
    [Route("api/[controller]")]
    public class DataBaseController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DataBaseController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        //Run once after migration to add roles to db.
        [HttpGet, Route("AddRoles")]
        public async Task AddRolesToDataBase()
        {
            string[] roleNames = { "Administrator", "Publisher", "Subscriber" };

            foreach (var role in roleNames)
            {
                var adminRole = await roleManager.FindByNameAsync(role);
                if (adminRole == null)
                {
                    adminRole = new IdentityRole(role);
                    await roleManager.CreateAsync(adminRole);
                }
            }
        }


        [HttpGet("Seed")]
        public async Task<IActionResult> SeedDatabase()
        {
            var users = userManager.Users.ToList();
            foreach (var user in users)
            {
                await userManager.DeleteAsync(user);
            }


            var file = Path.Combine(Environment.CurrentDirectory, "data", "DefaultDbPopulation.csv");

            if (!System.IO.File.Exists(file))
                return NotFound();

            using (var streamReader = System.IO.File.OpenText(file))
            {

                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var data = line.Split(new[] { ',' });
                    int age = 0;
                    string role = data[2];

                    if (!String.IsNullOrWhiteSpace(data[3]))
                        age = int.Parse(data[3]);

                    var user = new ApplicationUser { UserName = data[1], Age = age };
                   

                    var result = await userManager.CreateAsync(user);
                    if (!result.Succeeded)
                    {
                        return BadRequest();
                    }
                    if (!String.IsNullOrWhiteSpace(data[2]))
                        await userManager.AddToRoleAsync(user, role);
                }
            }
            return Ok("Database was seeded");
        }
    }
}