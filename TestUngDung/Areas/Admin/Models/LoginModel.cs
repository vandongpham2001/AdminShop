using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestUngDung.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required]
        public String Username { get; set; }
        public String Password { get; set; }
        public bool RememberMe { get; set; }
    }
}