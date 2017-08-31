using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Dto.Material
{
    public class MaterialDto: BaseDto
    {
        /// <summary>
        /// 文件展示名称
        /// </summary>
        [MaxLength(256)]
        public string Title { set; get; }

        /// <summary>
        /// 文件名
        /// </summary>
        [MaxLength(256)]
        public string FileName { set; get; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [MaxLength(20)]
        public string FileType { set; get; }

        /// <summary>
        /// MD5码
        /// </summary>
        [Required, MaxLength(32)]
        public string Md5Code { set; get; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { set; get; }

        /// <summary>
        /// 上传用户ID
        /// </summary>
        public long UploadUserId { set; get; }

        /// <summary>
        /// 上传用户
        /// </summary>
        public UserDto UploadUser { set; get; }

        public string FilePath { set; get; }
    }
}
