//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：MemberTypeService.cs
// 功能描述：会员类型 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.Extensions.Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.Membership;
using Vino.Core.CMS.Domain;
using Vino.Core.CMS.Domain.Dto.Membership;
using Vino.Core.CMS.Domain.Entity.Membership;
using Vino.Core.CMS.IService.Membership;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.Membership
{
    public partial class MemberTypeService : BaseService, IMemberTypeService
    {
        protected readonly IMemberTypeRepository _repository;

        #region 构造函数

        public MemberTypeService(IMemberTypeRepository repository)
        {
            this._repository = repository;
        }

        #endregion

        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<MemberTypeDto></returns>
        public async Task<List<MemberTypeDto>> GetListAsync(MemberTypeSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "OrderIndex asc");
            return Mapper.Map<List<MemberTypeDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<MemberTypeDto> items)> GetListAsync(int page, int size, MemberTypeSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "OrderIndex asc");
            return (data.count, Mapper.Map<List<MemberTypeDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<MemberTypeDto> GetByIdAsync(long id)
        {
            return Mapper.Map<MemberTypeDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(MemberTypeDto dto)
        {
            MemberType model = Mapper.Map<MemberType>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                await _repository.InsertAsync(model);
            }
            else
            {
                //更新
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得会员类型数据！");
                }

                //TODO:这里进行赋值
                item.Name = model.Name;
                item.OrderIndex = model.OrderIndex;
                _repository.Update(item);
            }
            await _repository.SaveAsync();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task DeleteAsync(params long[] id)
        {
            using (var _dapper = DapperFactory.Create())
            {
                await _dapper.DeleteAsync<MemberType>(new DapperSql("Id IN (@Ids)", new { Ids = id }));
            }
        }

        /// <summary>
        /// 恢复数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task RestoreAsync(params long[] id)
        {
            using (var _dapper = DapperFactory.Create())
            {
                await _dapper.RestoreAsync<MemberType>(new DapperSql("Id IN (@Ids)", new { Ids = id }));
            }
        }

        #endregion

        #region 其他方法

        #endregion
    }
}
