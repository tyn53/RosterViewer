using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gmi.RosterManager.Models
{
    /// <summary>
    /// Image View Model
    /// </summary>
    public class ImageModel
    {
        public string Filename { get; set; }
        public byte[]  Content { get; set; }
        public string ContentType { get; set; }
    }
}