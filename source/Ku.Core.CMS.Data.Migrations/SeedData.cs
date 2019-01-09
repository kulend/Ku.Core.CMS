using Ku.Core.CMS.Data.Migrations;
using Ku.Core.CMS.Data.Migrations.EntityFramework;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.Infrastructure.IdGenerator;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class SeedDataExtensions
    {
        public static IApplicationBuilder UseSeedData(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData.Seed(serviceScope.ServiceProvider);
            }
            return app;
        }
    }
}

namespace Ku.Core.CMS.Data.Migrations
{
    public class SeedData
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            try
            {
                var context = serviceProvider.GetRequiredService<KuDbContext>();
                //角色
                if (!context.UserCenterRoles.Any(x => x.NameEn == "ku.developer"))
                {
                    var role = new Role
                    {
                        Id = ID.NewID(),
                        Name = "开发者",
                        NameEn = "ku.developer",
                        IsEnable = true,
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        Remarks = "自动创建角色，拥有所有权限"
                    };
                    context.UserCenterRoles.Add(role);
                }

                if (!context.UserCenterUsers.Any())
                {
                    //admin用户
                    var user = new User
                    {
                        Id = ID.NewID(),
                        Password = "123456",
                        Account = "admin",
                        NickName = "admin",
                        IsEnable = true,
                        Factor = new Random().Next(9999),
                        IsAdmin = true,
                        CreateTime = DateTime.Now,
                        IsDeleted = false
                    };
                    user.EncryptPassword();
                    context.UserCenterUsers.Add(user);

                    //用户角色
                    var devRole = context.UserCenterRoles.FirstOrDefault(x => x.NameEn == "ku.developer");
                    if (devRole != null)
                    {
                        context.UserCenterUserRoles.Add(new UserRole
                        {
                            UserId = user.Id,
                            RoleId = devRole.Id
                        });
                    }
                }

                //菜单


                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
