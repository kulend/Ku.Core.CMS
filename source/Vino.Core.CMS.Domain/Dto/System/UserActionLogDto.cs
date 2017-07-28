using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class UserActionLogDto
    {
        [MaxLength(40)]
        public string Operation { set; get; }

        [MaxLength(60)]
        public string ControllerName { set; get; }

        [MaxLength(30)]
        public string ActionName { set; get; }

        public long? UserId { set; get; }

        [MaxLength(256)]
        public string Url { set; get; }

        [MaxLength(256)]
        public string UrlReferrer { set; get; }

        [MaxLength(46)]
        public string Ip { set; get; }

        [MaxLength(500)]
        public string ActionResult { set; get; }
    }
}
