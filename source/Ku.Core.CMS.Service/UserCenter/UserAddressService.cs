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
using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

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
                    await dapper.InsertAsync(model);
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.Create())
                {
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
