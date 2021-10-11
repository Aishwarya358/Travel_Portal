using Entity_Model_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel_Portal.Models;

namespace Travel_Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TravelPortalContext db = new TravelPortalContext();
        Auth p = new Auth();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
        public IActionResult Register()
        {
            return View();
        }
       [HttpPost]
       public async Task<IActionResult> Register(Customer obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Customers.Add(obj);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("LoginPage","User");
            }
            catch
            {
                return View();
            }
            
       }
        public IActionResult LoginPage(string msg)
        {
            ViewBag.mes = msg;
            return View();
        }
        [HttpPost]
        public IActionResult LoginPage(string email, string PassWord)
        {
            Admin usr = db.Admins.Where(v => v.Email ==email  && v.Password == PassWord).FirstOrDefault();
            if (usr != null)
            {
                var token = Createtoken();
                savetoken(token);
                return RedirectToAction("dash", "Admin");
            }
            return BadRequest("Invalid Admin");
        }
        private string Createtoken()
        {
            var Skey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnopqrst"));
            var credentials = new SigningCredentials(Skey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(

                issuer: "abc",
                audience: "abc",
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void savetoken(string token)
        {
            var cookdet = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddHours(2),
            };
            Response.Cookies.Append("mycookie", token, cookdet);
        }
        public IActionResult index2()
        {
            return View();
        }
        public IActionResult contact()
        {
            return View();
        }
        public IActionResult about()
        {
            return View();
        }
        public IActionResult Logout(string token)
        {
            try
            {
                var cookdet = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddHours(2),
                };
                Response.Cookies.Delete(token);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ViewBag.Message = "Please Login";
                return RedirectToAction("Index","Home");
            }


        }
    }
}
