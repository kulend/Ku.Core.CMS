//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：SmsTempletService.cs
// 功能描述：短信模板 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-02 09:50
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Data.Repository.System;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.IdGenerator;

namespace Ku.Core.CMS.Service.System
{
    public partial class SmsTempletService : BaseService, ISmsTempletService
    {
        protected readonly ISmsTempletRepository _repository;

        #region 构造函数

        public SmsTempletService(ISmsTempletRepository repository)
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
        /// <returns>List<SmsTempletDto></returns>
        public async Task<List<SmsTempletDto>> GetListAsync(SmsTempletSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<SmsTempletDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<SmsTempletDto> items)> GetListAsync(int page, int size, SmsTempletSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<SmsTempletDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<SmsTempletDto> GetByIdAsync(long id)
        {
            return Mapper.Map<SmsTempletDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(SmsTempletDto dto)
        {
            SmsTemplet model = Mapper.Map<SmsTemplet>(dto);
            if (model.Id == 0)
            {
                //新增
                //检查标记是否重复
                var count = await _repository.CountAsync(x=>x.Tag.Equals(model.Tag));
                if (count > 0)
                {
                    throw new VinoArgNullException("存在相同标记的数据！");
                }
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
                    throw new VinoDataNotFoundException("无法取得短信模板数据！");
                }
                //检查标记是否重复
                var count = await _repository.CountAsync(x => x.Tag.Equals(model.Tag) && x.Id != model.Id);
                if (count > 0)
                {
                    throw new VinoArgNullException("存在相同标记的数据！");
                }

                //这里进行赋值
                item.Tag = model.Tag;
                item.Name = model.Name;
                item.Example = model.Example;
                item.Templet = model.Templet;
                item.TempletKey = model.TempletKey;
                item.Signature = model.Signature;
                item.IsEnable = model.IsEnable;
                item.SmsAccountId = model.SmsAccountId;

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
