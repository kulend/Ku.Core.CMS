//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：AdvertisementBoardService.cs
// 功能描述：广告牌 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-10 22:15
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using Ku.Core.CMS.IService.Content;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.Content
{
    public partial class AdvertisementBoardService : BaseService<AdvertisementBoard, AdvertisementBoardDto, AdvertisementBoardSearch>, IAdvertisementBoardService
    {
        #region 构造函数

        public AdvertisementBoardService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(AdvertisementBoardDto dto)
        {
            AdvertisementBoard model = Mapper.Map<AdvertisementBoard>(dto);
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
                        model.Name,
                        model.Tag,
                        model.IsEnable
                    };
                    await dapper.UpdateAsync<AdvertisementBoard>(item, new { model.Id });
                }
            }
        }
    }
}
