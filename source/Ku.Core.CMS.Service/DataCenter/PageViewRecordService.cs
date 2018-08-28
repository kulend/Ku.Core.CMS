//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：PageViewRecordService.cs
// 功能描述：页面浏览记录 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-28 10:37
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using Ku.Core.CMS.IService.DataCenter;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.DataCenter
{
    public partial class PageViewRecordService : BaseService<PageViewRecord, PageViewRecordDto, PageViewRecordSearch>, IPageViewRecordService
    {
        #region 构造函数

        public PageViewRecordService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(PageViewRecordDto dto)
        {
            PageViewRecord model = Mapper.Map<PageViewRecord>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
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
                    };
                    await dapper.UpdateAsync<PageViewRecord>(item, new { model.Id });
                }
            }
        }
    }
}
