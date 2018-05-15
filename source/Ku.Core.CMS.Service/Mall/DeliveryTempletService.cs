//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：DeliveryTempletService.cs
// 功能描述：运费模板 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-05 10:25
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
    public partial class DeliveryTempletService : BaseService<DeliveryTemplet, DeliveryTempletDto, DeliveryTempletSearch>, IDeliveryTempletService
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(DeliveryTempletDto dto)
        {
            DeliveryTemplet model = Mapper.Map<DeliveryTemplet>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                model.IsSnapshot = false;
                model.SnapshotCount = 0;
                model.OriginId = null;
                model.EffectiveTime = DateTime.Now;
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
                    var item = new
                    {
                        //这里进行赋值

                    };
                    await dapper.UpdateAsync<DeliveryTemplet>(item, new { model.Id });
                }

                //using (var trans = await _repository.BeginTransactionAsync())
                //{
                //    //生成快照
                //    if (item.ChargeMode != model.ChargeMode
                //        || !item.ChargeConfig.Equals(model.ChargeConfig))
                //    {
                //        DeliveryTemplet snapshot = new DeliveryTemplet();
                //        snapshot.Id = ID.NewID();
                //        snapshot.IsDeleted = false;
                //        snapshot.CreateTime = DateTime.Now;
                //        snapshot.Name = item.Name;
                //        snapshot.Description = item.Description;
                //        snapshot.IsEnable = item.IsEnable;
                //        snapshot.ChargeConfig = item.ChargeConfig;
                //        snapshot.ChargeMode = item.ChargeMode;
                //        snapshot.SnapshotCount = item.SnapshotCount;
                //        snapshot.EffectiveTime = item.EffectiveTime;
                //        snapshot.ExpireTime = DateTime.Now;
                //        snapshot.IsSnapshot = true;
                //        snapshot.OriginId = item.OriginId;

                //        await _repository.InsertAsync(snapshot);

                //        item.ChargeMode = model.ChargeMode;
                //        item.ChargeConfig = model.ChargeConfig;
                //        item.EffectiveTime = DateTime.Now;
                //        item.SnapshotCount = item.SnapshotCount + 1;
                //    }
                //    item.Name = model.Name;
                //    item.Description = model.Description;
                //    item.IsEnable = model.IsEnable;

                //    _repository.Update(item);
                //    await _repository.SaveAsync();

                //    trans.Commit();
                //}
            }    
        }
    }
}
