using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAIfacebookEvents.Models;


namespace TAIfacebookEvents.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            try
            {
                var access_token = HttpContext.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == "FacebookAccessToken").First().Value;
                ViewBag.FacebookEvents = "BLEEEEE";
            }
            catch
            {
                ViewBag.Message = " Musisz być zalogowany przez Facebooka";
                return View();
            }
            finally
            {
                //ViewBag.Events = EventProvider.getEvents();
            }
            return View(EventProvider.getEvents());

        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Event = EventProvider.getEvent(id).First();
            return View(EventProvider.getEvent(id));
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // TODO: import user events from facebook, attach to view
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                int SYear = int.Parse(collection["SYear"]);
                int SMonth = int.Parse(collection["SMonth"]);
                int SDay = int.Parse(collection["SDay"]);
                //ViewData["Title"] = collection["Title"];
                //ViewData["Description"] = collection["Description"];

                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! user???
                Event ev = new Event(new DateTime(SYear, SMonth, SDay), collection["Title"], collection["Description"], "BABA JAGA");
                EventProvider.Events.Add(ev);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // POST: Event/Comment
        [HttpPost]
        public ActionResult Comment(FormCollection collection)
        {
            try
            {
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                string user = "Baba jaga";
                int i = int.Parse(collection["Id"]);
                string content = collection["Content"];
                Event ev = EventProvider.getEvent(i).First();
                ev.AddComment(user, content);

                return RedirectToAction("Details",new { id = i });
            }
            catch
            {
                return View();
            }
        }

        // GET: Event/Send
        public ActionResult Send(int id)
        {
            ViewBag.Message = "Olaboga";        // !!!!!!!!!!!!!!!!!!!!!!

            try
            {
                var access_token = HttpContext.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == "FacebookAccessToken").First().Value;
            }
            catch
            {
                ViewBag.Message = " Musisz być zalogowany przez Facebooka";
                return View();
            }
            // 
            return View();
        }

        // POST: Event/Send
        [HttpPost]
        public ActionResult Send(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                try {
                    var access_token = HttpContext.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == "FacebookAccessToken").First().Value;
                }
                catch
                {
                    return RedirectToAction("Login");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private static void PostToPage(string message, string pageAccessToken)
        {
            var fb = new FacebookClient(pageAccessToken);
            var argList = new Dictionary<string, object>();
            argList["message"] = message;
            fb.Post("feed", argList);
        }

    }
}
