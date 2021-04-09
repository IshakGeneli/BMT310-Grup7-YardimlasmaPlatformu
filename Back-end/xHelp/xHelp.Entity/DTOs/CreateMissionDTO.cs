using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using xHelp.Entity.Concrete;

namespace xHelp.Entity.DTOs
{
    public class CreateMissionDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Difficulty { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime CreatedDate { get; set; }
        public IFormFile ImageFile { get; set; }

        public string OwnerUserId { get; set; }
    }
}
