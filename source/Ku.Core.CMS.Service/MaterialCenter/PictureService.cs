//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：PictureService.cs
// 功能描述：图片素材 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-28 14:27
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Ku.Core.CMS.Service.MaterialCenter
{
    public partial class PictureService : BaseService<Picture, PictureDto, PictureSearch>, IPictureService
    {
        #region 构造函数

        public PictureService()
        {
        }

        #endregion

        /// <summary>
        /// 新增数据
        /// </summary>
        public async Task AddAsync(PictureDto dto, long[] groups)
        {
            Picture model = Mapper.Map<Picture>(dto);
            //新增
            model.CreateTime = DateTime.Now;
            model.IsDeleted = false;

            var grouprefs = groups?.Select(x => new UserMaterialGroupRef { MaterialId = model.Id, GroupId = x });

            using (var dapper = DapperFactory.CreateWithTrans())
            {
                await dapper.InsertAsync(model);
                //素材分组
                await dapper.InsertAsync<UserMaterialGroupRef>(grouprefs);

                dapper.Commit();
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        public async Task UpdateAsync(PictureDto dto)
        {
            Picture model = Mapper.Map<Picture>(dto);
            //更新
            using (var dapper = DapperFactory.Create())
            {
                var item = new
                {
                    //这里进行赋值
                    model.Title,
                    model.IsPublic
                };
                await dapper.UpdateAsync<Picture>(item, new { model.Id });
            }
        }
    }
}
