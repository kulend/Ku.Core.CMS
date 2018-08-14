//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：SmsQueueService.cs
// 功能描述：短信队列 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-22 15:57
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.Communication;
using Ku.Core.CMS.Domain.Entity.Communication;
using Ku.Core.CMS.IService.Communication;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dnc.Extensions.Dapper.Builders;

namespace Ku.Core.CMS.Service.Communication
{
    public partial class SmsQueueService : BaseService<SmsQueue, SmsQueueDto, SmsQueueSearch>, ISmsQueueService
    {
        #region 构造函数

        public SmsQueueService()
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
        public override async Task<(int count, List<SmsQueueDto> items)> GetListAsync(int page, int size, SmsQueueSearch where, dynamic sort)
        {
            using (var dapper = DapperFactory.Create())
            {
                var builder = new QueryBuilder().Select<SmsQueue>().Concat<Sms>()
                    .From<SmsQueue>()
                    .InnerJoin<Sms>().On(new ConditionBuilder().Equal<SmsQueue, Sms>(m => m.SmsId, t => t.Id))
                    .Where(where).Sort(sort as object).Limit(page, size);

                var data = await dapper.QueryPageAsync<SmsQueue, Sms, SmsQueue>(builder, (t1, t2) =>
                {
                    t1.Sms = t2;
                    return t1;
                }, splitOn: "Id");
                return (data.count, Mapper.Map<List<SmsQueueDto>>(data.items));
            }
        }
    }
}
