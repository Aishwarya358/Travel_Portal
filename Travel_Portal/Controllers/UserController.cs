using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Entity_Model_Layer.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace Travel_Portal.Controllers
{
    public class UserController : Controller
    {
        private TravelPortalContext db = new TravelPortalContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult package()
        {
            return View();
        }
        public IActionResult LoginPage(string msg)
        {
            ViewBag.mes = msg;
            return View();
        }
        [HttpPost]
        public IActionResult LoginPage(string email, string PassWord)
        {
            try
            {
                Customer usr = db.Customers.Where(v => v.EmailId == email && v.Password == PassWord).FirstOrDefault();
                if (usr != null)
                {
                    var token = Createtoken();
                    savetoken(token);
                    return RedirectToAction("Index","User");
                }
                return RedirectToAction("Index","User");
            }
            catch
            {
                return RedirectToAction("Index","User");
            }
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

        [HttpGet]
        public ActionResult viewtour()
        {
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                return View(travelPortalContext.TourPackages.ToList());
            }

        }
        [HttpGet]
        public ActionResult Booking(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult Booking(Booking booking)
        {
            try
            {
                SqlParameter p3 = new SqlParameter("TourId", @booking.TourId);
                TourPackage package = db.TourPackages.Where(v => v.TourId == booking.TourId).FirstOrDefault();
                booking.Source = package.Source;
                booking.Destination = package.Destination;
                booking.ReturnDate = booking.DepartureDate.AddDays(package.Duration);
                booking.Amount = ((package.Fare) + (package.FoodAmount) + (package.StayAmount)) * booking.TotalPassengers;
                booking.Status = "Booked";
                db.Add(booking);
                db.SaveChanges();
                ViewBag.BookingId = booking.BookingId;
                ViewBag.TotalFare = booking.Amount;
                ViewBag.Source = booking.Source;
                ViewBag.Destination = booking.Destination;
                Bill b = new Bill();
                //return View();
                //ViewBag.Message=String.Format
                //return RedirectToAction("Makepayment");
                return View("BookingSuccessful");
            }
            catch
            {
                return View("dash");
            }
        }
        
        public ActionResult CancelTicket()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckCancellation(int BookingId, string CustomerEmail)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                Booking booking = null;
                SqlParameter p = new SqlParameter("@BookingId", BookingId);
                SqlParameter p1 = new SqlParameter("@Status", "Cancelled");
                SqlParameter p2 = new SqlParameter("@CustomerEmail", CustomerEmail);

                //int RowFound = travelPortalContext.Database.ExecuteSqlRaw($"select * from Booking where BookingId=@BookingId and CustomerEmail=@CustomerEmail", p, p2);
                //if (RowFound>0)
                //{
                booking = db.Bookings.Where(v => v.BookingId == BookingId).FirstOrDefault();
                DateTime DeptTime = booking.DepartureDate;
                if (dateTime < DeptTime)
                {
                    db.Database.ExecuteSqlRaw($"update booking set Status=@Status,Amount=0.5*Amount where BookingId=@BookingId", p1, p);
                    ViewBag.Message = "Your Ticket Is Cancelled Successfully!";
                    ViewBag.RefundFare = ((booking.Amount) * 50) / 100;
                    return View("CancellationPage");
                }
                else
                {
                    ViewBag.Message = "You Cannot Cancel Your Ticket Because The Departure Date Is Exceeded.";
                    return View("CancellationPage");
                }
            }
            catch
            {
                return View("dash");
            }
        }
        public IActionResult bill(Bill obj)
        {
            IEnumerable<Customer> a =db.Customers.FromSqlRaw($"select * from customer where customerid='{obj.CustomerId}' order by customerid desc");
            foreach (var i in a)
            {
                ViewBag.id = i.CustomerId;
                ViewBag.name = i.CustomerName;
                ViewBag.phone = i.Phone;
                break;
            }
            ViewBag.stayamount = obj.StayCost;
            ViewBag.foodcost = obj.FoodCost;
            ViewBag.travellingcost = 600;
            ViewBag.passengers = obj.Passengers;
            ViewBag.totalamount = obj.StayCost + obj.FoodCost + 600;
            return View();
        }
        [HttpGet]
        public IActionResult Makepayment()
        {
            return View();
        }

    }
}

