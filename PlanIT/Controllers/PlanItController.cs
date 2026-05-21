using PlanIT.Models.Context;
using PlanIT.Models.Tables;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PlanIT.Controllers
{
    public class PlanItController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult AdminDashboard()
        {
            return View();
        }
        public ActionResult ViewShow()
        {
            return View();
        }
        public ActionResult CreateEvent()
        {
            return View();
        }
        public ActionResult DeleteEvent()
        {
            return View();
        }
        public ActionResult EditEvent()
        {
            return View();
        }
        public ActionResult UserList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RegisterUser(tbl_users_model userData)
        {
            try
            {
                using (var db = new PlanITContext())
                {
                    userData.RoleID = 2;
                    userData.CreatedAt = DateTime.Now;
                    userData.UpdatedAt = DateTime.Now;

                    db.tbl_users.Add(userData);
                    db.SaveChanges();
                }
                return Json("Success");
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return Json("Error: " + inner);
            }
        }

        public JsonResult LoginUser(tbl_users_model loginData)
        {
            try
            {
                using (var db = new PlanITContext())
                {
                    var user = db.tbl_users.FirstOrDefault(u => u.Email == loginData.Email
                                                             && u.Password == loginData.Password);

                    if (user != null)
                    {
                        return Json(new
                        {
                            status = "Success",
                            roleID = user.RoleID,
                            firstName = user.FirstName,
                            UserID = user.UserID
                        });
                    }
                    return Json(new { status = "Invalid", message = "Incorrect Email or Password" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = ex.Message });
            }
        }

        public JsonResult GetAllUsers()
        {
            using (var db = new PlanITContext())
            {
                try
                {
                    var users = db.tbl_users.ToList();
                    return Json(users, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult SaveEvent(tbl_events_model eventData)
        {
            try
            {
                using (var db = new PlanITContext())
                {
                    eventData.CreatedAt = DateTime.Now;

                    db.tbl_events.Add(eventData);
                    db.SaveChanges();

                    return Json("Success");
                }
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return Json("Error: " + msg);
            }
        }

        public JsonResult GetEvents()
        {
            try
            {
                using (var db = new PlanITContext())
                {
                    var data = db.tbl_events.ToList();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateEvent(tbl_events_model eventData)
        {
            try
            {
                using (var db = new PlanITContext())
                {
                    var existing = db.tbl_events.FirstOrDefault(e => e.EventID == eventData.EventID);

                    if (existing != null)
                    {
                        existing.Title = eventData.Title;
                        existing.EventDate = eventData.EventDate;
                        existing.Location = eventData.Location;
                        existing.TotalTickets = eventData.TotalTickets;
                        existing.Description = eventData.Description;
                        existing.Price = eventData.Price;

                        db.SaveChanges();
                        return Json("Success");
                    }
                    return Json("Error: Event not found");
                }
            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message);
            }
        }

        public JsonResult DeleteEventData(int EventID)
        {
            try
            {
                using (var db = new PlanITContext())
                {
                    var eventToDelete = db.tbl_events.FirstOrDefault(e => e.EventID == EventID);
                    if (eventToDelete != null)
                    {
                        db.tbl_events.Remove(eventToDelete);
                        db.SaveChanges();
                        return Json("Success");
                    }
                    return Json("Event not found");
                }
            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message);
            }
        }

        public JsonResult PurchaseTicket(int eventID, int userID)
        {
            using (var db = new PlanITContext())
            {
                var evnt = db.tbl_events.FirstOrDefault(e => e.EventID == eventID);

                if (evnt != null && evnt.TotalTickets > 0)
                {
                    var newBooking = new tbl_bookings_model
                    {
                        UserID = userID,
                        EventID = eventID,
                        TicketCount = 1,
                        BookingDate = DateTime.Now,
                        Status = "Confirmed"
                    };
                    db.tbl_bookings.Add(newBooking);

                    evnt.TotalTickets -= 1;

                    db.SaveChanges();
                    return Json("Success");
                }
                return Json("Sold Out or Error");
            }
        }

        public JsonResult GetUserTickets(int UserID)
        {
            using (var db = new PlanITContext())
            {
                try
                {
                    var myTickets = (from b in db.tbl_bookings
                                     join e in db.tbl_events on b.EventID equals e.EventID
                                     where b.UserID == UserID
                                     select new
                                     {
                                         b.BookingID,
                                         b.BookingDate,
                                         e.Title,
                                         e.Location,
                                         e.EventDate,
                                         e.Price
                                     }).ToList();

                    return Json(myTickets, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}