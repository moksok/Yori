using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("logout")]
    public class LogOutController : Controller
    {
        // GET: LogOut
        [Route("index")]
        public ActionResult Logout()
        {
            UserService.Logout();
            return RedirectToAction("index", "home");
        }
    }
}