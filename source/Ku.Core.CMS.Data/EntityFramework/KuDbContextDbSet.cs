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
        public DbSet<Ku.Core.CMS.Domain.Entity.DemoEntity> DemoEntitys { get; set; }

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
        /// WeChat 微信账户
        /// </summary>
        public DbSet<Domain.Entity.WeChat.WxAccount> WeChatWxAccounts { get; set; }

        /// <summary>
        /// 微信菜单
        /// </summary>
        public DbSet<Domain.Entity.WeChat.WxMenu> WxMenus { get; set; }

        /// <summary>
        /// WeChat 微信用户标签
        /// </summary>
        public DbSet<Domain.Entity.WeChat.WxUserTag> WeChatWxUserTags { get; set; }

        /// <summary>
        /// WeChat 微信用户
        /// </summary>
        public DbSet<Domain.Entity.WeChat.WxUser> WeChatWxUsers { get; set; }

        /// <summary>
        /// WeChat 微信二维码
        /// </summary>
        public DbSet<Domain.Entity.WeChat.WxQrcode> WeChatWxQrcodes { get; set; }

        /// <summary>
        /// MaterialCenter 图片素材
        /// </summary>
        public DbSet<Domain.Entity.MaterialCenter.Picture> MaterialCenterPictures { get; set; }

        /// <summary>
        /// 文章
        /// </summary>
        public DbSet<Domain.Entity.Content.Article> ContentArticles { get; set; }

        /// <summary>
        /// 栏目
        /// </summary>
        public DbSet<Domain.Entity.Content.Column> Columns { get; set; }

        /// <summary>
        /// Mall 配送模板
        /// </summary>
        public DbSet<Domain.Entity.Mall.DeliveryTemplet> MallDeliveryTemplets { get; set; }

        /// <summary>
        /// Mall 支付方式
        /// </summary>
        public DbSet<Domain.Entity.Mall.Payment> MallPayments { get; set; }

        /// <summary>
        /// Mall 商品
        /// </summary>
        public DbSet<Domain.Entity.Mall.Product> MallProducts { get; set; }

        /// <summary>
        /// Mall 商品SKU
        /// </summary>
        public DbSet<Domain.Entity.Mall.ProductSku> MallProductSkus { get; set; }

        /// <summary>
        /// 商品类目
        /// </summary>
        public DbSet<Domain.Entity.Mall.ProductCategory> ProductCategorys { get; set; }
		
        /// <summary>
        /// Mall 品牌
        /// </summary>
        public DbSet<Domain.Entity.Mall.Brand> MallBrands { get; set; }
		
        /// <summary>
        /// Mall 品牌类目关联
        /// </summary>
        public DbSet<Domain.Entity.Mall.BrandCategoryRef> MallBrandCategoryRefs { get; set; }
		
        /// <summary>
        /// Mall 计量单位
        /// </summary>
        public DbSet<Domain.Entity.Mall.ProductUnit> MallProductUnits { get; set; }
		
        /// <summary>
        /// Mall 订单
        /// </summary>
        public DbSet<Domain.Entity.Mall.Order> MallOrders { get; set; 
		
        /// <summary>
        /// Mall 订单商品
        /// </summary>
        public DbSet<Domain.Entity.Mall.OrderProduct> MallOrderProducts { get; set; }
		
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
        /// 用户角色
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
        /// UserCenter 收货地址
        /// </summary>
        public DbSet<Domain.Entity.UserCenter.UserAddress> UserCenterUserAddresss { get; set; }
        
		/// <summary>
        /// UserCenter 用户积分
        /// </summary>
        public DbSet<Domain.Entity.UserCenter.UserPoint> UserCenterUserPoints { get; set; }
		
        /// <summary>
        /// UserCenter 用户积分记录
        /// </summary>
        public DbSet<Domain.Entity.UserCenter.UserPointRecord> UserCenterUserPointRecords { get; set; }
        
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
