using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gmi.RosterManager.DataAccess;
using System.IO;

namespace Gmi.RosterManager.Controllers
{
    /// <summary>
    /// MVC Controller for Image actions.
    /// </summary>
    public class ImageController : Controller
    {
        private readonly IImageRepository imageRepo;

        public ImageController(IImageRepository iRepo)
        {
            this.imageRepo = iRepo;
        }
        
        public ActionResult Image(int id)
        {
            if (id > 0)
            {
                var image = imageRepo.GetImageById(id);
                return File(image.imageContent.ToArray(), image.imageContentType);
            }
            else
            {
                return File(Server.MapPath(Url.Content("~/Content/Images/default.jpg")), "image/jpeg", "default.jpg");
            }
        }

        public static void ValidateImage (HttpPostedFileBase image)
        {
            bool valid = true;
            if (image != null)
            {
                if (image.ContentLength > 4194304)
                    valid = false;

                if (image?.ContentType != "image/jpeg" &&
                    image?.ContentType != "image/png" &&
                    image?.ContentType != "image/gif" &&
                    image?.ContentType != "image/svg+xml")
                    valid = false;
            
            }
            if (!valid)
                throw new ArgumentException("Invalid image. Images must be less than 4MB and a valid image file.");

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