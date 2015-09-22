using Facebook;
using Newtonsoft.Json.Linq;
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
            try {
                //string myAccessToken = HttpContext.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == "FacebookAccessToken").First().Value;
                //FacebookClient client = new FacebookClient(myAccessToken);

                //var friendListData = client.Get("/me/friends");
                //JObject friendListJson = JObject.Parse(friendListData.ToString());

                //List<FbUser> fbUsers = new List<FbUser>();
                //foreach (var friend in friendListJson["data"].Children())
                //{
                //    FbUser fbUser = new FbUser();
                //    fbUser.Id = friend["id"].ToString().Replace("\"", "");
                //    fbUser.Name = friend["name"].ToString().Replace("\"", "");
                //    fbUsers.Add(fbUser);
                //}
                ViewBag.Message = "";
                return View();
            }

        catch
            {
                ViewBag.Message = "Nie mozna pobrać wydarzeń z Facebooka";
            }
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
                string user = User.Identity.Name;

                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! user???
                Event ev = new Event(new DateTime(SYear, SMonth, SDay ), collection["Title"], collection["Description"], user);
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
                string user = User.Identity.Name;
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
            ViewBag.Id = id;
            string message = "";
            var ev = EventProvider.getEvent(id).First();
            message = "Wydarzenie \"" + ev.Title + "\" odbędzie się dnia " + ev.Start.ToShortDateString() + " o godzinie " + ev.Start.ToLongTimeString()+ "." + Environment.NewLine + "Opis: " + ev.Description + Environment.NewLine + "Komentarze:\n";
            foreach (var kom in ev.Comments)
            {
                message += kom.User + ": " + kom.Content +Environment.NewLine;
            }
            ViewBag.Message = message;

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
                var access_token = HttpContext.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == "FacebookAccessToken").First().Value;
                string message = collection["message"];
                PostToPage(message, access_token);
                int id = Int32.Parse( collection["id"]);
                return RedirectToAction("Details", new { id = id });
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        private static void PostToPage(string message, string pageAccessToken)
        {
            var fb = new FacebookClient(pageAccessToken);
            var argList = new Dictionary<string, object>();
            argList["message"] = message;
            fb.Post("me/feed", argList);
        }

    }
}
