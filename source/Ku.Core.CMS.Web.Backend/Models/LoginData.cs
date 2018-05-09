using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Web.Backend.Models
{
    public class LoginData
    {
        [Required]
        public string Account { set; get; }

        [Required]
        public string Password { set; get; }

        public string ImageCode { set; get; }
    }
}
