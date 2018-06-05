//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：FunctionService.cs
// 功能描述：功能 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.Helper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.System
{
    public partial class FunctionService : BaseService<Function, FunctionDto, FunctionSearch>, IFunctionService
    {
        protected readonly ICacheProvider _cache;

        #region 构造函数

        public FunctionService(ICacheProvider cache)
        {
            this._cache = cache;
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(FunctionDto dto)
        {
            Function model = Mapper.Map<Function>(dto);
            if (model.Id == 0)
            {
                //新增
                using (var dapper = DapperFactory.CreateWithTrans())
                {
                    //取得父功能              
                    if (model.ParentId.HasValue)
                    {
                        var pModel = await dapper.QueryOneAsync<Function>(new { Id = model.ParentId.Value });
                        if (pModel == null)
                        {
                            throw new KuDataNotFoundException("无法取得父模块数据!");
                        }
                        if (!pModel.HasSub)
                        {
                            pModel.HasSub = true;

                            await dapper.UpdateAsync<Function>(new { HasSub = true}, new { pModel.Id });
                        }
                        model.Level = pModel.Level + 1;
                    }
                    else
                    {
                        model.ParentId = null;
                        model.Level = 1;
                    }

                    model.Id = ID.NewID();
                    model.CreateTime = DateTime.Now;
                    await dapper.InsertAsync(model);

                    dapper.Commit();
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
                        model.Name,
                        model.AuthCode,
                        model.IsEnable,
                    };
                    await dapper.UpdateAsync<Function>(item, new { model.Id });
                }
            }
        }

        #region 其他方法
		
        public async Task<List<FunctionDto>> GetParentsAsync(long parentId)
        {
            using (var dapper = DapperFactory.Create())
            {
                var list = new List<Function>();
                async Task GetWhithParentAsync(long pid)
                {
                    var model = await dapper.QueryOneAsync<Function>(new { Id = pid });
                    if (model != null)
                    {
                        if (model.ParentId.HasValue)
                        {
                            await GetWhithParentAsync(model.ParentId.Value);
                        }
                        list.Add(model);
                    }
                }

                await GetWhithParentAsync(parentId);
                return Mapper.Map<List<FunctionDto>>(list);
            }
        }
		
        public async Task<List<FunctionDto>> GetSubsAsync(long? parentId)
        {
            using (var dapper = DapperFactory.Create())
            {
                var list = await dapper.QueryListAsync<Function>(new { ParentId = parentId }, new { CreateTime = "asc" });
                return Mapper.Map<List<FunctionDto>>(list);
            }
        }
		
        #endregion
    }
}
