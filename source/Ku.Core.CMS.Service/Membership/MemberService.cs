//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：MemberService.cs
// 功能描述：会员 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.Membership;
using Ku.Core.CMS.Domain.Entity.Membership;
using Ku.Core.CMS.IService.Membership;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.Membership
{
    public partial class MemberService : BaseService<Member, MemberDto, MemberSearch>, IMemberService
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<MemberDto></returns>
        public override async Task<List<MemberDto>> GetListAsync(MemberSearch where, dynamic sort)
        {
            using (var dapper = DapperFactory.Create())
            {
                var data = await dapper.QueryListAsync<Member, MemberType, Member>("t1.*, t2.*", "membership_member t1 LEFT JOIN membership_member_type t2 ON t2.Id=t1.MemberTypeId",
                    where: where.ParseToDapperSql(dapper.Dialect),
                    order: sort as object,
                    map: (t1, t2) => {
                        t1.MemberType = t2;
                        return t1;
                    });
                return Mapper.Map<List<MemberDto>>(data.ToList());
            }
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public override async Task<(int count, List<MemberDto> items)> GetListAsync(int page, int size, MemberSearch where, dynamic sort)
        {
            using (var dapper = DapperFactory.Create())
            {
                var data = await dapper.QueryPageAsync<Member, MemberType, Member>(page, size, "t1.*, t2.*", "membership_member t1 LEFT JOIN membership_member_type t2 ON t2.Id=t1.MemberTypeId",
                    where: where.ParseToDapperSql(dapper.Dialect),
                    order: sort as object,
                    map: (t1, t2) => {
                        t1.MemberType = t2;
                        return t1;
                    });
                return (data.count, Mapper.Map<List<MemberDto>>(data.items));
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(MemberDto dto)
        {
            Member model = Mapper.Map<Member>(dto);
            if (model.Id == 0)
            {
                //新增
                using (var dapper = DapperFactory.Create())
                {
                    //检查手机号
                    if (model.Mobile.IsNotNullOrEmpty())
                    {
                        //格式
                        if (!model.Mobile.IsMobile())
                        {
                            throw new VinoDataNotFoundException("手机号格式不正确！");
                        }
                        //是否重复
                        var cnt = await dapper.QueryCountAsync<Member>(new { Mobile = model.Mobile });
                        if (cnt > 0)
                        {
                            throw new VinoDataNotFoundException("手机号重复！");
                        }
                    }

                    model.Id = ID.NewID();
                    model.CreateTime = DateTime.Now;
                    model.IsDeleted = false;
                    model.Points = 0;
                    model.Level = 0;
                    var random = new Random();
                    model.Factor = random.Next(9999);
                    //密码设置
                    model.EncryptPassword();
                    await dapper.InsertAsync(model);
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.Create())
                {
                    var item = new
                    {
                        model.Name,
                        model.Mobile,
                        model.IsEnable,
                        model.Sex,
                        model.MemberTypeId,
                    };
                    await dapper.UpdateAsync<Member>(item, new { model.Id });
                }
            }
        }

    }
}
