using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.Material
{
    [Table("material_picture")]
    public class Picture : Material
    {
        [MaxLength(256)]
        public string ThumbPath { set; get; }
    }
}
