using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vino.Core.CMS.Web.Admin.Models
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
