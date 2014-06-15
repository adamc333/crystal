using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace CrystalMVC.Models
{
    public class Thumbnail
    {
        public int ID { get; set; }
        public string Filename { get; set; }
        public string OriginalPath { get; set; }
        public string Extension { get; set; }
        public int HeightInPixels { get; set; }
        public int WidthInPixels { get; set; }
        public string ThumbPath
        {
            get
            {
                return GetThumbnailPath(OriginalPath);
            }
        }

        private static string GetThumbnailPath(string pathOfOriginal)
        {
            var thumbPath = pathOfOriginal.Replace("Pictures", "Pictures/Thumbs");

            if (File.Exists(HttpContext.Current.Server.MapPath("~" + thumbPath)))
            {
                return thumbPath;
            }

            var bigPic = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~" + pathOfOriginal));
            var maxHeight = 75;
            
            if (bigPic.Height > maxHeight)
            {
                double scale = (double)maxHeight / (double)bigPic.Height;
                double width = bigPic.Width * scale;
                System.Drawing.Image.GetThumbnailImageAbort dummyCallBack = null;

                var smallPic = bigPic.GetThumbnailImage((int)width, maxHeight, dummyCallBack, IntPtr.Zero);

                var filename = System.IO.Path.GetFileNameWithoutExtension(pathOfOriginal);
                var smallPicPath = "/Content/Pictures/Thumbs/" + filename + ".jpg";

                smallPic.Save(HttpContext.Current.Server.MapPath(smallPicPath));

                return smallPicPath;
            }
            return pathOfOriginal;
        }
    }
}