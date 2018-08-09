//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：SmsTempletService.cs
// 功能描述：短信模板 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-22 15:53
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.Communication;
using Ku.Core.CMS.Domain.Entity.Communication;
using Ku.Core.CMS.IService.Communication;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.Communication
{
    public partial class SmsTempletService : BaseService<SmsTemplet, SmsTempletDto, SmsTempletSearch>, ISmsTempletService
    {
        #region 构造函数

        public SmsTempletService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(SmsTempletDto dto)
        {
            SmsTemplet model = Mapper.Map<SmsTemplet>(dto);
            if (model.Id == 0)
            {
                //新增
                using (var dapper = DapperFactory.Create())
                {
                    //检查标记是否重复
                    var count = await dapper.QueryCountAsync<SmsTemplet>(new { Tag = model.Tag });
                    if (count > 0)
                    {
                        throw new KuArgNullException("存在相同标记的数据！");
                    }

                    model.Id = ID.NewID();
                    model.CreateTime = DateTime.Now;
                    model.IsDeleted = false;

                    await dapper.InsertAsync(model);
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.Create())
                {
                    //检查标记是否重复
                    var items = await dapper.QueryListAsync<SmsTemplet>(new { Tag = model.Tag, IsDeleted = false });
                    if (items.Any(x => x.Id != model.Id))
                    {
                        throw new KuArgNullException("存在相同标记的数据！");
                    }

                    var item = new
                    {
                        //这里进行赋值
                        model.Tag,
                        model.Name,
                        model.Example,
                        model.Templet,
                        model.TempletKey,
                        model.Signature,
                        model.IsEnable,
                        model.SmsAccountId
                    };
                    await dapper.UpdateAsync<SmsTemplet>(item, new { model.Id });
                }
            }
        }
    }
}
