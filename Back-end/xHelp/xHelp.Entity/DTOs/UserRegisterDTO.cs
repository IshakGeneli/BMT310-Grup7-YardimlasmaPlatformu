using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace xHelp.Entity.DTOs
{
    public class UserRegisterDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
