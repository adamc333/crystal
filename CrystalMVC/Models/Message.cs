using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;

namespace CrystalMVC.Models
{
    public class Message
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string SubmittedBy { get; set; }

        public Message() { }

        public bool Save()
        {
            var xmlSerializer = new XmlSerializer(this.GetType());
            var pathOfMessages = ConfigurationManager.AppSettings["MessagesDirectory"];
            using (var stream = new FileStream(HttpContext.Current.Server.MapPath(pathOfMessages), FileMode.Append))
            {
                //var stream = new FileStream(HttpContext.Current.Server.MapPath(pathOfMessages), FileMode.Append);
                xmlSerializer.Serialize(stream, this);
            }
            return true;
        }
    }
}
