using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.System
{
    [Table("system_action_log")]
    public class UserActionLog : BaseEntity
    {
        [MaxLength(40)]
        public string Operation { set; get; }

        [MaxLength(30)]
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

        public User User { set; get; }
    }
}
