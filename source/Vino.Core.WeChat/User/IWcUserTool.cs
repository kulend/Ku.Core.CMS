using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.WeChat.AccessToken;

namespace Vino.Core.WeChat.User
{
    public interface IWcUserTool
    {
        /// <summary>
        /// 取得用户标签列表
        /// </summary>
        Task<WcReply<WcUserTagListRsp>> GetUserTagListAsync(WcAccessToken token);

        /// <summary>
        /// 创建用户标签
        /// </summary>
        Task<WcReply<WcUserTagCreateRsp>> CreateUserTag(WcAccessToken token, string name);

        /// <summary>
        /// 更新用户标签
        /// </summary>
        Task<WcReply<string>> UpdateUserTag(WcAccessToken token, int tagId, string name);

        /// <summary>
        /// 删除用户标签
        /// </summary>
        Task<WcReply<string>> DeleteUserTag(WcAccessToken token, int tagId);

        /// <summary>
        /// 取得微信用户列表
        /// </summary>
        Task<WcReply<WcUserListRsp>> GetUserListAsync(WcAccessToken token, string nextOpenId);

        /// <summary>
        /// 取得微信用户信息
        /// </summary>
        Task<WcReply<WcUserDetailRsp>> GetUserDetailAsync(WcAccessToken token, string openId);

        /// <summary>
        /// 更新用户备注名
        /// </summary>
        Task<WcReply<string>> UpdateUserRemark(WcAccessToken token, string openid, string remark);
    }
}
