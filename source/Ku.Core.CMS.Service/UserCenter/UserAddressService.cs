//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserAddressService.cs
// 功能描述：收货地址 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-01 13:14
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dnc.Extensions.Dapper.Builders;

namespace Ku.Core.CMS.Service.UserCenter
{
    public partial class UserAddressService : BaseService<UserAddress, UserAddressDto, UserAddressSearch>, IUserAddressService
    {
        #region 构造函数

        public UserAddressService()
        {
        }

        #endregion

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public override async Task<(int count, List<UserAddressDto> items)> GetListAsync(int page, int size, UserAddressSearch where, dynamic sort)
        {
            using (var dapper = DapperFactory.Create())
            {
                var builder = new QueryBuilder().Select<UserAddress>().Concat<User>(u => new { u.Id, u.NickName})
                    .From<UserAddress>()
                    .InnerJoin<User>().On(new ConditionBuilder().Equal<UserAddress, User>(m => m.UserId, u => u.Id))
                    .Where(where).Sort(sort as object).Limit(page, size);

                var data = await dapper.QueryPageAsync<UserAddress, User, UserAddress>(builder, (t1, t2) =>
                {
                    t1.User = t2;
                    return t1;
                }, splitOn: "Id");
                return (data.count, Mapper.Map<List<UserAddressDto>>(data.items));
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(UserAddressDto dto)
        {
            UserAddress model = Mapper.Map<UserAddress>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;

                using (var dapper = DapperFactory.Create())
                {
                    var user = await dapper.QueryOneAsync<User>(new { Id = model.UserId });
                    if (user == null)
                    {
                        throw new KuDataNotFoundException("无法取得用户信息！");
                    }

                    if (model.Default)
                    {
                        //更新其他地址为非默认地址
                        await dapper.UpdateAsync<UserAddress>(new { Default = false }, new { UserId = model.UserId, Default = true });
                    }

                    await dapper.InsertAsync(model);
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.Create())
                {
                    if (model.Default)
                    {
                        //更新其他地址为非默认地址
                        await dapper.UpdateAsync<UserAddress>(new { Default = false }, new { UserId = model.UserId, Default = true });
                    }

                    var item = new {
                        //这里进行赋值
                        model.Consignee,
                        model.Mobile,
                        model.Province,
                        model.ProvinceCode,
                        model.City,
                        model.CityCode,
                        model.Region,
                        model.RegionCode,
                        model.Street,
                        model.StreetCode,
                        model.Address,
                        model.Default
                    };
                    await dapper.UpdateAsync<UserAddress>(item, new { model.Id });
                }
            }
        }
    }
}
