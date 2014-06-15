using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalMVC.Models;

namespace CrystalMVC.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        //post handles submitting a message
        [HttpPost]
        public JsonResult Index(Message message)
        {
            message.SubmittedDate = DateTime.Now;
            var IsGreatSuccess = message.Save();
            return this.Json(IsGreatSuccess);
        }
    }
}
