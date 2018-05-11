//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：MemberAddressService.cs
// 功能描述：会员地址 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-02 14:29
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.Membership;
using Ku.Core.CMS.Domain.Entity.Membership;
using Ku.Core.CMS.IService.Membership;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.Membership
{
    public partial class MemberAddressService : BaseService<MemberAddress, MemberAddressDto, MemberAddressSearch>, IMemberAddressService
    {
        #region 构造函数

        public MemberAddressService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(MemberAddressDto dto)
        {
            MemberAddress model = Mapper.Map<MemberAddress>(dto);
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
                        //TODO:这里进行赋值
                    };
                    await dapper.UpdateAsync<MemberAddress>(item, new { model.Id });
                }
            }
        }
    }
}
