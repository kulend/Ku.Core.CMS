//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：MemberPointService.cs
// 功能描述：会员积分 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-20 15:52
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.Membership;
using Vino.Core.CMS.Domain.Dto.Membership;
using Vino.Core.CMS.Domain.Entity.Membership;
using Vino.Core.CMS.IService.Membership;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.Membership
{
    public partial class MemberPointService : BaseService, IMemberPointService
    {
        protected readonly IMemberPointRepository _repository;

        #region 构造函数

        public MemberPointService(IMemberPointRepository repository)
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
        /// <returns>List<MemberPointDto></returns>
        public async Task<List<MemberPointDto>> GetListAsync(MemberPointSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<MemberPointDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<MemberPointDto> items)> GetListAsync(int page, int size, MemberPointSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<MemberPointDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<MemberPointDto> GetByIdAsync(long id)
        {
            return Mapper.Map<MemberPointDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(MemberPointDto dto)
        {
            MemberPoint model = Mapper.Map<MemberPoint>(dto);
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
                    throw new VinoDataNotFoundException("无法取得会员积分数据！");
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
            if (await _repository.RestoreAsync(id))
            {
                await _repository.SaveAsync();
            }
        }

        #endregion

        #region 其他方法

        #endregion
    }
}
