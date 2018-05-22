//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：KuDbContext.cs
// 功能描述：DbContext
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:05
// 说明：此代码由工具自动生成，对此文件的更改可能会导致不正确的行为，
// 并且如果重新生成代码，这些更改将会丢失。
//
//----------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace Ku.Core.CMS.Data.EntityFramework
{
    public partial class KuDbContext
    {
        /// <summary>
        /// 用户操作日志
        /// </summary>
        public DbSet<Domain.Entity.System.UserActionLog> UserActionLogs { get; set; }

        /// <summary>
        /// System 功能
        /// </summary>
        public DbSet<Domain.Entity.System.Function> SystemFunctions { get; set; }

        /// <summary>
        /// System 菜单
        /// </summary>
        public DbSet<Domain.Entity.System.Menu> SystemMenus { get; set; }

        /// <summary>
        /// System 公告
        /// </summary>
        public DbSet<Domain.Entity.System.Notice> SystemNotices { get; set; }

        /// <summary>
        /// 会员
        /// </summary>
        public DbSet<Domain.Entity.Membership.Member> Members { get; set; }

        /// <summary>
        /// 会员类型
        /// </summary>
        public DbSet<Domain.Entity.Membership.MemberType> MemberTypes { get; set; }

        /// <summary>
        /// 会员积分
        /// </summary>
        public DbSet<Domain.Entity.Membership.MemberPoint> MemberPoints { get; set; }

        /// <summary>
        /// 会员积分记录
        /// </summary>
        public DbSet<Domain.Entity.Membership.MemberPointRecord> MemberPointRecords { get; set; }

        /// <summary>
        /// 会员地址
        /// </summary>
        public DbSet<Domain.Entity.Membership.MemberAddress> MemberAddresss { get; set; }

        /// <summary>
        /// 公众号
        /// </summary>
        public DbSet<Domain.Entity.WeChat.WxAccount> WxAccounts { get; set; }

        /// <summary>
        /// 微信菜单
        /// </summary>
        public DbSet<Domain.Entity.WeChat.WxMenu> WxMenus { get; set; }

        /// <summary>
        /// 微信用户标签
        /// </summary>
        public DbSet<Domain.Entity.WeChat.WxUserTag> WxUserTags { get; set; }

        /// <summary>
        /// 微信用户
        /// </summary>
        public DbSet<Domain.Entity.WeChat.WxUser> WxUsers { get; set; }

        /// <summary>
        /// 微信二维码
        /// </summary>
        public DbSet<Domain.Entity.WeChat.WxQrcode> WxQrcodes { get; set; }

        /// <summary>
        /// 图片素材
        /// </summary>
        public DbSet<Domain.Entity.Material.Picture> Pictures { get; set; }

        /// <summary>
        /// 素材分组
        /// </summary>
        public DbSet<Domain.Entity.Material.MaterialGroup> MaterialGroups { get; set; }

        /// <summary>
        /// 文章
        /// </summary>
        public DbSet<Domain.Entity.Content.Article> ContentArticles { get; set; }

        /// <summary>
        /// 栏目
        /// </summary>
        public DbSet<Domain.Entity.Content.Column> Columns { get; set; }

        /// <summary>
        /// 配送模板
        /// </summary>
        public DbSet<Domain.Entity.Mall.DeliveryTemplet> DeliveryTemplets { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public DbSet<Domain.Entity.Mall.Payment> Payments { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public DbSet<Domain.Entity.Mall.Product> Products { get; set; }

        /// <summary>
        /// 商品SKU
        /// </summary>
        public DbSet<Domain.Entity.Mall.ProductSku> ProductSkus { get; set; }

        /// <summary>
        /// 商品类目
        /// </summary>
        public DbSet<Domain.Entity.Mall.ProductCategory> ProductCategorys { get; set; }

        /// <summary>
        /// DataCenter 应用
        /// </summary>
        public DbSet<Domain.Entity.DataCenter.App> DataCenterApps { get; set; }

        /// <summary>
        /// DataCenter 应用版本
        /// </summary>
        public DbSet<Domain.Entity.DataCenter.AppVersion> DataCenterAppVersions { get; set; }

        /// <summary>
        /// 应用反馈
        /// </summary>
        public DbSet<Domain.Entity.DataCenter.AppFeedback> AppFeedbacks { get; set; }

        /// <summary>
        /// 用户角色关联
        /// </summary>
        public DbSet<Domain.Entity.UserCenter.User> UserCenterUsers { get; set; }

        /// <summary>
        /// 用户角色关联
        /// </summary>
        public DbSet<Domain.Entity.UserCenter.Role> UserCenterRoles { get; set; }

        /// <summary>
        /// 用户角色关联
        /// </summary>
        public DbSet<Domain.Entity.UserCenter.UserRole> UserCenterUserRoles { get; set; }
		
        /// <summary>
        /// UserCenter 角色功能关联
        /// </summary>
        public DbSet<Domain.Entity.UserCenter.RoleFunction> UserCenterRoleFunctions { get; set; }

        /// <summary>
        /// Communication 短信账户
        /// </summary>
        public DbSet<Domain.Entity.Communication.SmsAccount> CommunicationSmsAccounts { get; set; }
		
        /// <summary>
        /// Communication 短信
        /// </summary>
        public DbSet<Domain.Entity.Communication.Sms> CommunicationSmss { get; set; }
		
        /// <summary>
        /// Communication 短信模板
        /// </summary>
        public DbSet<Domain.Entity.Communication.SmsTemplet> CommunicationSmsTemplets { get; set; }
		
        /// <summary>
        /// Communication 短信队列
        /// </summary>
        public DbSet<Domain.Entity.Communication.SmsQueue> CommunicationSmsQueues { get; set; }
    }
}
