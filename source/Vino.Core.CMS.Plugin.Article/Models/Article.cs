using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Plugin.Article.Models
{
    [Table("article_article")]
    public class Article : BaseProtectedEntity
    {
        [Required, MaxLength(256)]
        public string Title { set; get; }
    }
}
