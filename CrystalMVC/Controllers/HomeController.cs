using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrystalMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var gallery = new Models.ThumbnailGallery(20);
            return View(gallery);
        }

        public JsonResult GetThumbList()
        {
            var gallery = new Models.ThumbnailGallery(0);
            return this.Json(gallery, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPreviousPicture(string currentSrc)
        {
            var thumbs = new Models.ThumbnailGallery(0).Gallery.ToArray();
            var currentIndex = Array.FindIndex(thumbs, p => p.Filename.Equals(currentSrc));
            
            //if first pic currently shown, show last, otherwise show previous
            var previousIndex = (currentIndex == 0 ? thumbs.Length - 1 : currentIndex - 1);

            return this.Json(thumbs[previousIndex], JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNextPicture(string currentSrc)
        {
            var thumbs = new Models.ThumbnailGallery(0).Gallery.ToArray();
            var currentIndex = Array.FindIndex(thumbs, p => p.Filename.Equals(currentSrc));

            //if last pic currently shown, show first, otherwise show next
            var nextIndex = (currentIndex == thumbs.Length - 1 ? 0 : currentIndex + 1);

            return this.Json(thumbs[nextIndex], JsonRequestBehavior.AllowGet);
        }
    }
}
