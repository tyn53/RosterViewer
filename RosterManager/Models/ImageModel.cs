using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gmi.RosterManager.Models
{
    public class ImageModel
    {
        public string Filename { get; set; }
        public byte[]  Content { get; set; }
        public string ContentType { get; set; }
    }
}