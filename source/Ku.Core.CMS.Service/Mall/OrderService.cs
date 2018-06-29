//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：OrderService.cs
// 功能描述：订单 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-29 15:51
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;
using Ku.Core.CMS.IService.Mall;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.Mall
{
    public partial class OrderService : BaseService<Order, OrderDto, OrderSearch>, IOrderService
    {
        #region 构造函数

        public OrderService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(OrderDto dto)
        {
            Order model = Mapper.Map<Order>(dto);
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
                    await dapper.UpdateAsync<Order>(item, new { model.Id });
                }
            }
        }
    }
}
