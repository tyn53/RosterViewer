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
                return File(Server.MapPath(Url.Content("~/Content/Images/default.jpg")), "image/jpeg", "default.jpg");
            }
        }

        public static bool ValidateImage (HttpPostedFileBase image)
        {
            if (image?.ContentLength > 4194304)
                return false;

            if (image?.ContentType != "image/jpeg" &&
                image?.ContentType != "image/png" &&
                image?.ContentType != "image/gif" &&
                image?.ContentType != "image/svg+xml")
                return false;
            
            return true;

        }

        public static Image ConverToDbModel(HttpPostedFileBase image)
        {
            var dbImage = new Image()
            {
                imageFileName = image.FileName,
                imageContent = ConvertToBytes(image),
                imageContentType = image.ContentType
            };

            return dbImage;
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