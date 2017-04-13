using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Gmi.RosterManager.DataAccess
{
    /// <summary>
    /// Data access repositoy for image objects.
    /// </summary>
    public class ImageRepository : IImageRepository
    {

        public ImageRepository()
        {
            
        }

        public Image GetImageById(int imageId)
        {
            var dbContext = new RosterManagerDataContext();
            var result = (from image in dbContext.Images
                          where image.imageId == imageId
                          select image).First();
            
            return result;
        }
        public void DeleteImage (int imageId)
        {
            using (var dbContext = new RosterManagerDataContext())
            {
                var unwantedImage = (from image in dbContext.Images
                                     where image.imageId == imageId
                                     select image).First();

                dbContext.Images.DeleteOnSubmit(unwantedImage);
                dbContext.SubmitChanges();
            }
        }
        
    }

    /// <summary>
    /// Image repository interface.
    /// </summary>
    public interface IImageRepository
    {
        Image GetImageById(int imageId);
        void DeleteImage(int imageId);
    }
}