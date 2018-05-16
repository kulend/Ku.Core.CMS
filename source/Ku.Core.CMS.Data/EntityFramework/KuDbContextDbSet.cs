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
        public DbSet<Domain.Entity.System.UserRole> SystemUserRoles { get; set; }

        public DbSet<Domain.Entity.System.RoleFunction> SystemRoleFunctions { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<Domain.Entity.System.User> SystemUsers { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<Domain.Entity.System.Role> SystemRoles { get; set; }

        /// <summary>
        /// 用户操作日志
        /// </summary>
        public DbSet<Domain.Entity.System.UserActionLog> UserActionLogs { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        public DbSet<Domain.Entity.System.Function> Functions { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public DbSet<Domain.Entity.System.Menu> Menus { get; set; }

        /// <summary>
        /// 短信
        /// </summary>
        public DbSet<Domain.Entity.System.Sms> Smss { get; set; }

        /// <summary>
        /// 短信队列
        /// </summary>
        public DbSet<Domain.Entity.System.SmsQueue> SmsQueues { get; set; }

        /// <summary>
        /// 短信账号
        /// </summary>
        public DbSet<Domain.Entity.System.SmsAccount> SmsAccounts { get; set; }

        /// <summary>
        /// 短信模板
        /// </summary>
        public DbSet<Domain.Entity.System.SmsTemplet> SmsTemplets { get; set; }

        /// <summary>
        /// 公告
        /// </summary>
        public DbSet<Domain.Entity.System.Notice> Notices { get; set; }

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
        public DbSet<Domain.Entity.Content.Article> Articles { get; set; }

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
        /// 应用
        /// </summary>
        public DbSet<Domain.Entity.DataCenter.App> Apps { get; set; }

        /// <summary>
        /// 应用版本
        /// </summary>
        public DbSet<Domain.Entity.DataCenter.AppVersion> AppVersions { get; set; }

        /// <summary>
        /// 应用反馈
        /// </summary>
        public DbSet<Domain.Entity.DataCenter.AppFeedback> AppFeedbacks { get; set; }

        /// <summary>
        /// 用户角色关联
        /// </summary>
        public DbSet<Domain.Entity.UserCenter.User> Users { get; set; }

        /// <summary>
        /// 用户角色关联
        /// </summary>
        public DbSet<Domain.Entity.UserCenter.Role> Roles { get; set; }

        /// <summary>
        /// 用户角色关联
        /// </summary>
        public DbSet<Domain.Entity.UserCenter.UserRole> UserRoles { get; set; }

    }
}
