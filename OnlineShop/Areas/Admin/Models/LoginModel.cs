using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Chưa nhập user")]
        public string username { set; get; }
        [Required(ErrorMessage = "Chưa nhập password")]
        public string password { set; get; }
        public bool Remember { set; get; }
    }
}