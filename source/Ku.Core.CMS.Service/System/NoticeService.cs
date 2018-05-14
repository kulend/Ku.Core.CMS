//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：NoticeService.cs
// 功能描述：公告 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-18 09:55
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
    public partial class NoticeService : BaseService<Notice, NoticeDto, NoticeSearch>, INoticeService
    {
        #region 构造函数

        public NoticeService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(NoticeDto dto)
        {
            Notice model = Mapper.Map<Notice>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                if (model.IsPublished && !model.PublishedTime.HasValue)
                {
                    model.PublishedTime = DateTime.Now;
                }
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
                        Title = model.Title,
                        PublishedTime = model.PublishedTime,
                        IsPublished = model.IsPublished,
                        ContentType = model.ContentType,
                        Content = model.Content,
                        StickyNum = model.StickyNum
                    };
                    await dapper.UpdateAsync<Notice>(item, new { model.Id });
                }
            }
        }

    }
}
