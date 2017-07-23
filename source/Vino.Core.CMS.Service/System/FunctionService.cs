using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Service.System
{
    public class FunctionService : IFunctionService
    {
        private IFunctionRepository repository;

        public FunctionService(IFunctionRepository _repository)
        {
            this.repository = _repository;
        }

        #region 功能模块

        public Task<(int count, List<FunctionDto> list)> GetListAsync(long? parentId, int pageIndex, int pageSize)
        {
            (int, List<FunctionDto>) Gets()
            {
                int startRow = (pageIndex - 1) * pageSize;
                var queryable = repository.GetQueryable();
                if (parentId.HasValue)
                {
                    queryable = queryable.Where(u => u.ParentId == parentId);
                }
                else
                {
                    queryable = queryable.Where(u => u.ParentId == null);
                }
                var count = queryable.Count();
                var query = queryable.OrderBy(x => x.CreateTime).Skip(startRow).Take(pageSize);
                return (count, Mapper.Map<List<FunctionDto>>(query.ToList()));
            }
            return Task.FromResult(Gets());
        }

        public async Task SaveAsync(FunctionDto dto)
        {
            Function model = Mapper.Map<Function>(dto);
            if (model.Id == 0)
            {
                //新增
                //取得父功能              
                if (model.ParentId.HasValue)
                {
                    var pModel = await repository.GetByIdAsync(model.ParentId.Value);
                    if (pModel == null)
                    {
                        throw new VinoDataNotFoundException("无法取得父模块数据!");
                    }
                }
                else
                {
                    model.ParentId = null;
                }

                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                await repository.InsertAsync(model);
            }
            else
            {
                //更新
                var function = await repository.GetByIdAsync(model.Id);
                if (function == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }

                function.Name = model.Name;
                function.AuthCode = model.AuthCode;
                function.IsEnable = model.IsEnable;
                repository.Update(function);
            }
            await repository.SaveAsync();
        }

        public Task<List<FunctionDto>> GetParentsAsync(long parentId)
        {
            List<FunctionDto> Gets()
            {
                return GetParents(parentId);
            }
            return Task.FromResult(Gets());
        }

        public async Task<FunctionDto> GetByIdAsync(long id)
        {
            return Mapper.Map<FunctionDto>(await this.repository.GetByIdAsync(id));
        }

        public async Task DeleteAsync(long id)
        {
            await repository.DeleteAsync(id);
            await repository.SaveAsync();
        }

        private List<FunctionDto> GetParents(long parentId)
        {
            var list = new List<Function>();
            void GetModel(long pid)
            {
                var model = repository.FirstOrDefault(x => x.Id == pid);
                if (model != null)
                {
                    if (model.ParentId.HasValue)
                    {
                        GetModel(model.ParentId.Value);
                    }
                    list.Add(model);
                }
            }
            GetModel(parentId);
            return Mapper.Map<List<FunctionDto>>(list);
        }

        public async Task<List<FunctionDto>> GetSubModulesAsync(long? parentId)
        {
            var queryable = repository.GetQueryable();
            if (parentId.HasValue)
            {
                queryable = queryable.Where(u => u.ParentId == parentId);
            }
            else
            {
                queryable = queryable.Where(u => u.ParentId == null);
            }

            var query = queryable.OrderBy(x => x.CreateTime);
            return Mapper.Map<List<FunctionDto>>(await query.ToListAsync());
        }

        #endregion

    }
}
