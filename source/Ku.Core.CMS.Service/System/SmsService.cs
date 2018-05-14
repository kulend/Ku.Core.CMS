//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：SmsService.cs
// 功能描述：短信 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.System
{
    public partial class SmsService : BaseService<Sms, SmsDto, SmsSearch>, ISmsService
    {
        #region 构造函数

        public SmsService()
        {
        }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        public async Task AddAsync(SmsDto dto)
        {
            Sms model = Mapper.Map<Sms>(dto);

            if (string.IsNullOrEmpty(model.Mobile))
            {
                throw new VinoArgNullException("未设置手机号！");
            }

            using (var dapper = DapperFactory.Create())
            {
                var templet = await dapper.QueryOneAsync<SmsTemplet>(new { Id = model.SmsTempletId });
                if (templet == null || templet.IsDeleted)
                {
                    throw new VinoDataNotFoundException("无法取得短信模板数据！");
                }
                if (!templet.IsEnable)
                {
                    throw new VinoDataNotFoundException("短信模板已禁用！");
                }

                var content = templet.Templet;
                foreach (var item in dto.Data)
                {
                    content = content.Replace("${" + item.Key + "}", item.Value);
                }
                model.Content = content + $"【{templet.Signature}】";

                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;

                //创建队列
                var queue = new SmsQueue
                {
                    Id = ID.NewID(),
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    Status = Domain.Enum.System.EmSmsQueueStatus.WaitSend,
                    SmsId = model.Id,
                    ExpireTime = templet.ExpireMinute == 0 ? DateTime.Now.AddDays(1) : DateTime.Now.AddMinutes(templet.ExpireMinute)
                };

                using (var trans = dapper.BeginTrans())
                {
                    await dapper.InsertAsync(model);
                    await dapper.InsertAsync(queue);
                    trans.Commit();
                }
            }
        }
    }
}
