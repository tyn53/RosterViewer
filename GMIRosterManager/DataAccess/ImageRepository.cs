using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GMIRosterManager.DataAccess
{
    public class ImageRepository
    {
        private RosterManagerDataContext dbContext;

        public ImageRepository()
        {
            dbContext = new RosterManagerDataContext();
        }

        public byte[] getImageById(int imageId)
        {
            var result = (from image in dbContext.Images
                          where image.imageId == imageId
                          select image.imageData).First().ToArray();
            
            return result;
        }

        
    }
}