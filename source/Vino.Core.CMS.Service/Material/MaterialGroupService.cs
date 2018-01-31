using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.Material;
using Vino.Core.CMS.Domain.Entity.Material;
using Vino.Core.CMS.IService.Material;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.Material
{
    public partial class MaterialGroupService : IMaterialGroupService
    {
        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<MaterialGroupDto></returns>
        public async Task<List<MaterialGroupDto>> GetListAsync(MaterialGroupSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return _mapper.Map<List<MaterialGroupDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<MaterialGroupDto> items)> GetListAsync(int page, int size, MaterialGroupSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, _mapper.Map<List<MaterialGroupDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<MaterialGroupDto> GetByIdAsync(long id)
        {
            return _mapper.Map<MaterialGroupDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(MaterialGroupDto dto)
        {
            MaterialGroup model = _mapper.Map<MaterialGroup>(dto);
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
                    throw new VinoDataNotFoundException("无法取得素材分组数据！");
                }

                //TODO:这里进行赋值
                item.Name = model.Name;
                _repository.Update(item);
            }
            await _repository.SaveAsync();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }

        #endregion

        #region 其他方法

        #endregion
    }
}
