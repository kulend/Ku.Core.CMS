//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：MaterialGroupService.cs
// 功能描述：素材分组 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.Material;
using Ku.Core.CMS.Domain.Entity.Material;
using Ku.Core.CMS.IService.Material;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.Material
{
    public partial class MaterialGroupService : BaseService<MaterialGroup, MaterialGroupDto, MaterialGroupSearch>, IMaterialGroupService
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(MaterialGroupDto dto)
        {
            MaterialGroup model = Mapper.Map<MaterialGroup>(dto);
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
                        model.Name
                    };
                    await dapper.UpdateAsync<MaterialGroup>(item, new { model.Id });
                }
            }
        }
    }
}
