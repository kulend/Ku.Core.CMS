//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：AppService.cs
// 功能描述：应用 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-15 10:34
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.DataCenter;
using Vino.Core.CMS.Domain.Dto.DataCenter;
using Vino.Core.CMS.Domain.Entity.DataCenter;
using Vino.Core.CMS.IService.DataCenter;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.Helper;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.DataCenter
{
    public partial class AppService : BaseService, IAppService
    {
        protected readonly IAppRepository _repository;

        #region 构造函数

        public AppService(IAppRepository repository)
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
        /// <returns>List<AppDto></returns>
        public async Task<List<AppDto>> GetListAsync(AppSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<AppDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<AppDto> items)> GetListAsync(int page, int size, AppSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<AppDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<AppDto> GetByIdAsync(long id)
        {
            return Mapper.Map<AppDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(AppDto dto)
        {
            App model = Mapper.Map<App>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.AppKey = CryptHelper.EncryptMD5Short(model.Id.ToString());
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
                    throw new VinoDataNotFoundException("无法取得应用数据！");
                }

                //这里进行赋值
                item.Type = model.Type;
                item.Name = model.Name;
                item.Intro = model.Intro;
                item.IconData = model.IconData;
                item.DownloadUrl = model.DownloadUrl;

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
