//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：MemberService.cs
// 功能描述：会员 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public partial class MemberService : BaseService, IMemberService
    {
        protected readonly IMemberRepository _repository;

        #region 构造函数

        public MemberService(IMemberRepository repository)
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
        /// <returns>List<MemberDto></returns>
        public async Task<List<MemberDto>> GetListAsync(MemberSearch where, string sort)
        {
            var includes = new List<Expression<Func<Member, object>>>();
			includes.Add(x=>x.MemberType);
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc", includes.ToArray());
            return Mapper.Map<List<MemberDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<MemberDto> items)> GetListAsync(int page, int size, MemberSearch where, string sort)
        {
            var includes = new List<Expression<Func<Member, object>>>();
            includes.Add(x => x.MemberType);
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc", includes.ToArray());
            return (data.count, Mapper.Map<List<MemberDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<MemberDto> GetByIdAsync(long id)
        {
            return Mapper.Map<MemberDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(MemberDto dto)
        {
            Member model = Mapper.Map<Member>(dto);
            if (model.Id == 0)
            {
                //新增
                //检查手机号
                if (model.Mobile.IsNotNullOrEmpty())
                {
                    //格式
                    if (!model.Mobile.IsMobile())
                    {
                        throw new VinoDataNotFoundException("手机号格式不正确！");
                    }
                    //是否重复
                    var cnt = await _repository.GetQueryable().Where(x => x.Mobile.EqualOrdinalIgnoreCase(model.Mobile)).CountAsync();
                    if (cnt > 0)
                    {
                        throw new VinoDataNotFoundException("手机号重复！");
                    }
                }

                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                model.Points = 0;
                model.Level = 0;
                var random = new Random();
                model.Factor = random.Next(9999);
                //密码设置
                model.EncryptPassword();
                await _repository.InsertAsync(model);
            }
            else
            {
                //更新
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得会员数据！");
                }

                //TODO:这里进行赋值
                item.Name = model.Name;
                item.Mobile = model.Mobile;
                item.IsEnable = model.IsEnable;
                item.Sex = model.Sex;
                if (!model.Password.EqualOrdinalIgnoreCase("the password has not changed"))
                {
                    item.Password = model.Password;
                    //密码设置
                    item.EncryptPassword();
                }
                item.MemberTypeId = model.MemberTypeId;
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
