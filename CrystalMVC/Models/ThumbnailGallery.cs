using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace CrystalMVC.Models
{
    public class ThumbnailGallery
    {
        //page count
        public int Pages { set; get; }
        public List<Thumbnail> Gallery { set; get; }

        private string portfolioPicturesDirectory;

        public ThumbnailGallery(int neededPics)
        {
            portfolioPicturesDirectory = System.Configuration.ConfigurationManager.AppSettings["PortfolioPicsDirectory"];
            var physicalDirectoryPath = HttpContext.Current.Server.MapPath(portfolioPicturesDirectory);
            var directory = new DirectoryInfo(physicalDirectoryPath);
            var files = directory.GetFiles("*", SearchOption.TopDirectoryOnly);

            if (neededPics == 0)
            {
                neededPics = files.Count();
            }
            
            Gallery = new List<Thumbnail>();

            //figure out the current page at bottom
            if (files.Count() % neededPics == 0)
                Pages = files.Count() / neededPics;
            else
                Pages = (files.Count() / neededPics) + 1;

            //new up the thumb and add to the Gallery
            for (var count = 1; count <= neededPics; count++)
            {
                var thumb = new Thumbnail
                {
                    ID = count,
                    OriginalPath = portfolioPicturesDirectory + files[count - 1].Name,
                    Extension = files[count - 1].Extension,
                    Filename = files[count - 1].Name
                };

                Gallery.Add(thumb);
            }
        }
    }
}