using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageSaverService
{
    public class SendImageModel
    {
        public string base64 { get; set; }
        public string userGuid { get; set; }
    }
}