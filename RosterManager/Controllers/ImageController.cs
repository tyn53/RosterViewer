using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gmi.RosterManager.DataAccess;
using System.IO;

namespace Gmi.RosterManager.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Image(int id)
        {
            if (id > 0)
            {
                var imageRepo = new ImageRepository();
                var image = imageRepo.GetImageById(id);
                return File(image.imageContent.ToArray(), image.imageContentType);
            }
            else
            {
                return File(Server.MapPath(Url.Content("~/Content/Images/default.jpg")), "image/jpg", "default.jpg");
            }
        }

       
        public static byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageData;
            var reader = new BinaryReader(image.InputStream);
            imageData = reader.ReadBytes((int)image.ContentLength);
            return imageData;
        }
    }
}