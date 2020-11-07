using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageSaverService
{
    public class ImageModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserGuid { get; set; }
        [Required]
        public string Guid { get; set; }
        [Required]
        public string Path { get; set; }
    }
}