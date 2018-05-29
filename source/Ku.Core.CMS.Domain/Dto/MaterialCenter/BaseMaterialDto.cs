using Ku.Core.CMS.Domain.Dto.UserCenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Dto.MaterialCenter
{
    public class BaseMaterialDto: BaseProtectedDto
    {
        /// <summary>
        /// 标题
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
        [MaxLength(32)]
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

        [MaxLength(256)]
        public string FilePath { set; get; }

        [MaxLength(256)]
        public string Folder { set; get; }

        /// <summary>
        /// 是否公开
        /// </summary>
        [DefaultValue(false)]
        [Display(Name = "是否公开", Prompt = "公开|私密")]
        public bool IsPublic { set; get; } = false;
    }
}
