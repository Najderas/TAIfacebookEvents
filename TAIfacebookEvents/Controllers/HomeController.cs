using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Facebook;
using TAIfacebookEvents.Models;
using Newtonsoft.Json.Linq;

namespace TAIfacebookEvents.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.identity = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            ViewBag.authType = HttpContext.GetOwinContext().Authentication.User.Identity.AuthenticationType;

            ViewBag.cookies = Request.Cookies.AllKeys;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Friends = new List<FbUser>();
            ViewBag.FriendsCount =0;
            ViewBag.Message = "Your application description page.";

            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            //var access_token = claimsIdentity.FindAll("name").First().ToString();
            //var access_token = claimsIdentity.FindAll("BLEE");
            //var access_token2 = access_token.First();
            //var access_token3 = access_token2.ToString();
            
            var claimlist = from claims in HttpContext.GetOwinContext().Authentication.User.Claims
                            select new ExtPropertyViewModel
                            {
                                Issuer = claims.Issuer,
                                Type = claims.Type,
                                Value = claims.Value
                            };
            try
            {
                var access_token = HttpContext.GetOwinContext().Authentication.User.Claims.Where(c => c.Type == "FacebookAccessToken").First().Value;
                ViewBag.Token = access_token;

                var fb = new FacebookClient(access_token);

                //var friendListData = fb.Get("/me/friends");
                //JObject friendListJson = JObject.Parse(friendListData.ToString());
                //List<FbUser> fbUsers = new List<FbUser>();
                //foreach (var friend in friendListJson["data"].Children())
                //{
                //    FbUser fbUser = new FbUser();
                //    fbUser.Id = friend["id"].ToString().Replace("\"", "");
                //    fbUser.Name = friend["name"].ToString().Replace("\"", "");
                //    fbUsers.Add(fbUser);
                //}
                //ViewBag.Friends = fbUsers;
                //ViewBag.FriendsCount = fbUsers.Count;

                string ans = fb.Get("me").ToString();
                ViewBag.Me = ans;
                string ans2 = fb.Get("me/permissions").ToString();
                ViewBag.Permissions = ans2;
                
                string ans4 = fb.Get("me/friends", new { fields = new[] { "name, id" } }).ToString();
                ViewBag.Friends = ans4;


                return View(claimlist.ToList<ExtPropertyViewModel>());
            }
            catch
            {
                return View(new List<ExtPropertyViewModel>());
            }
            

            
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}