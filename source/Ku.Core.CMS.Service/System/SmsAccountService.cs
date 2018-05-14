//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：SmsAccountService.cs
// 功能描述：短信账号 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-03-26 16:05
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.System
{
    public partial class SmsAccountService : BaseService<SmsAccount, SmsAccountDto, SmsAccountSearch>, ISmsAccountService
    {
        #region 构造函数

        public SmsAccountService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(SmsAccountDto dto)
        {
            SmsAccount model = Mapper.Map<SmsAccount>(dto);
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
                    var item = new
                    {
                        model.Name,
                        model.ParameterConfig,
                        model.IsEnable,
                        model.Remarks,
                        model.Type
                    };
                    await dapper.UpdateAsync<SmsAccount>(item, new { model.Id });
                }
            }
        }
    }
}
