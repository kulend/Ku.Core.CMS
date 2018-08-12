//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：AdvertisementService.cs
// 功能描述：广告 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-10 21:27
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
using System.Collections.Generic;
using System.Linq;
using Ku.Core.CMS.Domain;

namespace Ku.Core.CMS.Service.Content
{
    public partial class AdvertisementService : BaseService<Advertisement, AdvertisementDto, AdvertisementSearch>, IAdvertisementService
    {
        #region 构造函数

        public AdvertisementService()
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
        public override async Task<(int count, List<AdvertisementDto> items)> GetListAsync(int page, int size, AdvertisementSearch where, dynamic sort)
        {
            using (var dapper = DapperFactory.Create())
            {
                var data = await dapper.QueryPageAsync<Advertisement>(page, size, where.ParseToDapperSql(dapper.Dialect), sort as object);
                return (data.count, Mapper.Map<List<AdvertisementDto>>(data.items));
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(AdvertisementDto dto, long[] boards)
        {
            Advertisement model = Mapper.Map<Advertisement>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                using (var dapper = DapperFactory.CreateWithTrans())
                {
                    await dapper.InsertAsync(model);

                    //广告位处理
                    var refs = boards?.Select(x => new AdvertisementBoardRef { AdvertisementId = model.Id, AdvertisementBoardId = x });

                    await dapper.InsertAsync<AdvertisementBoardRef>(refs);

                    dapper.Commit();
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.CreateWithTrans())
                {
                    var item = new {
                        //这里进行赋值
                        model.Name,
                        model.Title,
                        model.Provenance,
                        model.ImageData,
                        model.FlashUrl,
                        model.IsPublished,
                        model.Link,
                        model.OrderIndex
                    };
                    await dapper.UpdateAsync<Advertisement>(item, new { model.Id });

                    //广告位处理
                    await dapper.DeleteAsync<AdvertisementBoardRef>(new { AdvertisementId = model.Id });

                    var refs = boards?.Select(x => new AdvertisementBoardRef { AdvertisementId = model.Id, AdvertisementBoardId = x });
                    await dapper.InsertAsync<AdvertisementBoardRef>(refs);

                    dapper.Commit();
                }
            }
        }

        /// <summary>
        /// 取得广告所在广告位列表
        /// </summary>
        public async Task<IEnumerable<AdvertisementBoardDto>> GetAdvertisementBoardsAsync(long id)
        {
            using (var dapper = DapperFactory.Create())
            {
                var data = await dapper.QueryListAsync<AdvertisementBoard>("t1.*", "content_advertisement_board t1",
                    where: new DapperSql("EXISTS (SELECT * FROM content_advertisement_board_ref t2 WHERE t2.AdvertisementBoardId=t1.Id AND t2.AdvertisementId=@AdvertisementId)", new { AdvertisementId = id }),
                    order: null);
                return Mapper.Map<IEnumerable<AdvertisementBoardDto>>(data);
            }
        }
    }
}
