using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity_Model_Layer.Models;
using System.Text;
using Travel_Portal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Travel_Portal.Controllers
{
    public class AdminController : Controller
    {
        private TravelPortalContext db = new TravelPortalContext();

        public IActionResult dash()
        {   
            return View();
        }

        [HttpGet]
        public IActionResult ViewCustomer()
        {
            try
            {
                IEnumerable<Customer> rlist = db.Customers.ToList();
                return View(rlist);
            }
            catch
            {
                return View("dash");
            }
        }
        public IActionResult updatecustomer(int id)
        {
            try
            {
                return View(db.Customers.Where(x => x.CustomerId == id).FirstOrDefault());
            }
            catch
            {
                return View("dash");
            }
        }
        [HttpPost]
        public IActionResult updatecustomer(int id, Customer cust)
        {
            try
            {
                db.Entry(cust).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                return View("dash");
            }
            return View();
        }
        public IActionResult customerdetails(int id)
        {
            return View(db.Customers.Where(x => x.CustomerId == id).FirstOrDefault());
        }
        // GET: CustomerController/Delete/5
        public ActionResult DeleteCustomer(int id)
        {
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                return View(travelPortalContext.Customers.Where(x => x.CustomerId == id).FirstOrDefault());

            }
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomer(int id, IFormCollection collection)
        {
            try
            {
                using (TravelPortalContext travelPortalContext = new TravelPortalContext())
                {
                    Customer customer = new Customer();
                    var change = String.Empty;
                    customer = travelPortalContext.Customers.Find(id);
                    if (customer.Status == "0")
                    {
                        change = "1";
                    }
                    else if (customer.Status == "1")
                    {
                        change = "0";
                    }

                    customer.Status = change;
                    travelPortalContext.Entry(customer).State = EntityState.Modified;
                    travelPortalContext.SaveChanges();
                }
                return RedirectToAction("ViewCustomer", "Admin");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Index()
        {
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                return View(travelPortalContext.TourPackages.ToList());
            }

        }

        // GET: TourController/Details/5
        public ActionResult Details(int id)
        {
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                return View(travelPortalContext.TourPackages.Where(x => x.TourId == id).FirstOrDefault());
            }
        }

        // GET: TourController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TourController/Create
        [HttpPost]
        public async Task<ActionResult> Create(TourPackage tourPackage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(tourPackage);
                    await db.SaveChangesAsync();
                }
                IEnumerable<TourPackage> a = db.TourPackages.FromSqlRaw($"select * from tourpackage where tourid='{tourPackage.TourId}'");
                Bill b = new Bill();
                foreach (var i in a)
                {
                    b.PlaceName = i.Destination;
                    //b.StayCost = Convert.ToInt32(i.StayAmount);
                    b.FoodCost = i.FoodAmount;
                    b.StayCost = i.StayAmount;
                    b.TravelingCost = 500;
                }
                db.Bills.Add(b);
                return View("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TourController/Edit/5
        public ActionResult Edit(int id)
        {
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                return View(travelPortalContext.TourPackages.Where(x => x.TourId == id).FirstOrDefault());

            }
        }

        // POST: TourController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TourPackage tourPackage)
        {
            try
            {
                using (TravelPortalContext travelPortalContext = new TravelPortalContext())
                {
                    travelPortalContext.Entry(tourPackage).State = EntityState.Modified;
                    travelPortalContext.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TourController/Delete/5
        public ActionResult Delete(int id)
        {
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                return View(travelPortalContext.TourPackages.Where(x => x.TourId == id).FirstOrDefault());

            }
        }

        // POST: TourController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                using (TravelPortalContext travelPortalContext = new TravelPortalContext())
                {
                    TourPackage tourPackage = travelPortalContext.TourPackages.Where(x => x.TourId == id).FirstOrDefault();
                    travelPortalContext.TourPackages.Remove(tourPackage);
                    travelPortalContext.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: TourController/Delete/5

        public JsonResult CheckTourName(string Username)
        {
            try
            {
                using (TravelPortalContext travelPortalContext = new TravelPortalContext())
                {
                    var isTourNameExists = travelPortalContext.TourPackages.Where(x => x.TourName == Username).FirstOrDefault();


                    if (isTourNameExists != null)
                    {

                        return Json(data: true);
                    }
                    else
                    {
                        return Json(data: false);
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public ActionResult viewbus()
        {
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                return View(travelPortalContext.buses.ToList());
            }
        }

        // GET: AllBusController/Details/5
        public ActionResult busdetails(int id)
        {
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                return View(travelPortalContext.buses.Where(x => x.BusId == id).FirstOrDefault());

            }

        }
        public ActionResult ViewAllBus(int Id)
        {

            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                TempData["mydata"] = Id;
                return View(travelPortalContext.buses.Where(x => x.TourId == Id).ToList());

            }
        }


        // GET: AllBusController/Create
        public ActionResult createbus()
        {
            var data = TempData["mydata"];
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                var items = travelPortalContext.TourPackages.Where(x => x.TourId == Convert.ToInt32(data)).ToList();
                if (items != null)
                {
                    ViewBag.data = items;
                }
            }

            return View();
        }

        // POST: AllBusController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createbus(Bu buses)
        {
            try
            {
                using (TravelPortalContext travelPortalContext = new TravelPortalContext())
                {

                    travelPortalContext.buses.Add(buses);

                    travelPortalContext.SaveChanges();

                }
                var id = buses.TourId;
                //return RedirectToAction(nameof(Tour/Details));
                return RedirectToAction("ViewAllBus", "Admin", new { id = id });
                //return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AllBusController/Edit/5
        public ActionResult updatebus(int id)
        {
            var data = TempData["mydata"];
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                var items = travelPortalContext.TourPackages.Where(x => x.TourId == Convert.ToInt32(data)).ToList();
                if (items != null)
                {
                    ViewBag.data = items;
                }
                return View(travelPortalContext.buses.Where(x => x.BusId == id).FirstOrDefault());

            }
        }

        // POST: AllBusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult updatebus(int id, Bu bus)
        {
            try
            {
                using (TravelPortalContext travelPortalContext = new TravelPortalContext())
                {
                    travelPortalContext.Entry(bus).State = EntityState.Modified;
                    travelPortalContext.SaveChanges();
                }
                var Tid = bus.TourId;
                return RedirectToAction("ViewAllBus", "Admin");
            }
            catch
            {
                return View();
            }
        }

        // GET: AllBusController/Delete/5
        public ActionResult deletebus(int id)
        {
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                return View(travelPortalContext.buses.Where(x => x.BusId == id).FirstOrDefault());

            }
        }

        // POST: AllBusController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult deletebus(int id, Bu bus)
        {
            try
            {
                using (TravelPortalContext travelPortalContext = new TravelPortalContext())
                {
                    bus = travelPortalContext.buses.Where(x => x.BusId == id).FirstOrDefault();

                    travelPortalContext.buses.Remove(bus);
                    travelPortalContext.SaveChanges();
                }
                var Tid = bus.TourId;
                return RedirectToAction("ViewAllBus", "Admin");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult viewbooking()
        {
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                return View(travelPortalContext.Bookings.ToList());
            }
        }

        // GET: BookingsController/Details/5
        public ActionResult bookingdetails(int id)
        {
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                return View(travelPortalContext.Bookings.Where(x => x.TourId == id).FirstOrDefault());

            }
        }

        // GET: BookingsController/Create
        public ActionResult Createbooking()
        {
            return View();
        }

        // POST: BookingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createbooking(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingsController/Edit/5
        public ActionResult Editbooking(int id)
        {
            return View();
        }

        // POST: BookingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editbooking(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingsController/Delete/5
        public ActionResult Deletebooking(int id)
        {
            using (TravelPortalContext travelPortalContext = new TravelPortalContext())
            {
                return View(travelPortalContext.Bookings.Where(x => x.BookingId == id).FirstOrDefault());

            }
        }

        // POST: BookingsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deletebooking(int id, IFormCollection collection)
        {
            try
            {
                using (TravelPortalContext travelPortalContext = new TravelPortalContext())
                {
                    Booking booking = new Booking();
                    var change = String.Empty;
                    booking = travelPortalContext.Bookings.Find(id);
                    if (booking.Status == "0")
                    {
                        change = "1";
                    }
                    else if (booking.Status == "1")
                    {
                        change = "0";
                    }

                    booking.Status = change;
                    travelPortalContext.Entry(booking).State = EntityState.Modified;
                    travelPortalContext.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

}


