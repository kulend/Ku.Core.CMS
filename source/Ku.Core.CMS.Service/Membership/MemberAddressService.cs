//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：MemberAddressService.cs
// 功能描述：会员地址 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-02 14:29
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.Extensions.Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Data.Repository.Membership;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.Membership;
using Ku.Core.CMS.Domain.Entity.Membership;
using Ku.Core.CMS.IService.Membership;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.IdGenerator;

namespace Ku.Core.CMS.Service.Membership
{
    public partial class MemberAddressService : BaseService, IMemberAddressService
    {
        protected readonly IMemberAddressRepository _repository;

        #region 构造函数

        public MemberAddressService(IMemberAddressRepository repository)
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
        /// <returns>List<MemberAddressDto></returns>
        public async Task<List<MemberAddressDto>> GetListAsync(MemberAddressSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<MemberAddressDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<MemberAddressDto> items)> GetListAsync(int page, int size, MemberAddressSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<MemberAddressDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<MemberAddressDto> GetByIdAsync(long id)
        {
            return Mapper.Map<MemberAddressDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(MemberAddressDto dto)
        {
            MemberAddress model = Mapper.Map<MemberAddress>(dto);
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
                    throw new VinoDataNotFoundException("无法取得会员地址数据！");
                }

                //TODO:这里进行赋值

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
            if (await _repository.DeleteAsync(id))
            {
                await _repository.SaveAsync();
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
                await _dapper.RestoreAsync<MemberAddress>(new DapperSql("Id IN (@Ids)", new { Ids = id }));
            }
        }

        #endregion

        #region 其他方法

        #endregion
    }
}
