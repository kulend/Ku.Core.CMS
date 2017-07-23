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
    public class FunctionModuleService: IFunctionModuleService
    {
        private IFunctionModuleRepository repository;
        private IFunctionModuleActionRepository actionRepository;

        public FunctionModuleService(IFunctionModuleRepository _repository,
            IFunctionModuleActionRepository _actionRepository)
        {
            this.repository = _repository;
            this.actionRepository = _actionRepository;
        }

        #region 功能模块

        public Task<(int count, List<FunctionModuleDto> list)> GetListAsync(long? parentId, int pageIndex, int pageSize)
        {
            (int, List<FunctionModuleDto>) Gets()
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
                var query = queryable.OrderBy(x => x.OrderIndex)
                    .ThenBy(x => x.CreateTime).Skip(startRow).Take(pageSize);
                return (count, Mapper.Map<List<FunctionModuleDto>>(query.ToList()));
            }
            return Task.FromResult(Gets());
        }

        public async Task SaveAsync(FunctionModuleDto dto)
        {
            FunctionModule model = Mapper.Map<FunctionModule>(dto);
            if (model.Id == 0)
            {
                //新增
                //取得父菜单               
                if (model.ParentId.HasValue)
                {
                    var pModel = await repository.GetByIdAsync(model.ParentId.Value);
                    if (pModel == null)
                    {
                        throw new VinoDataNotFoundException("无法取得父模块数据!");
                    }
                    model.Depth = pModel.Depth + 1;
                    if (pModel.IsLeaf)
                    {
                        pModel.IsLeaf = false;
                        repository.Update(pModel);
                    }
                }
                else
                {
                    model.ParentId = null;
                    model.Depth = 1;
                }

                model.HasCode = false;
                model.IsLeaf = true;
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                await repository.InsertAsync(model);
            }
            else
            {
                //更新
                var module = await repository.GetByIdAsync(model.Id);
                if (module == null)
                {
                    throw new VinoDataNotFoundException("无法取得模块数据!");
                }

                module.Name = model.Name;
                module.Url = model.Url;
                module.Icon = model.Icon;
                module.IsMenu = model.IsMenu;
                module.OrderIndex = model.OrderIndex;
                repository.Update(module);
            }
            await repository.SaveAsync();
        }

        public Task<List<FunctionModuleDto>> GetParentsAsync(long parentId)
        {
            List<FunctionModuleDto> Gets()
            {
                return GetParents(parentId);
            }
            return Task.FromResult(Gets());
        }

        public async Task<FunctionModuleDto> GetByIdAsync(long id)
        {
            return Mapper.Map<FunctionModuleDto>(await this.repository.GetByIdAsync(id));
        }

        public async Task DeleteAsync(long id)
        {
            await repository.DeleteAsync(id);
            await repository.SaveAsync();
        }

        private List<FunctionModuleDto> GetParents(long parentId)
        {
            var list = new List<FunctionModule>();
            void GetModel(long pid)
            {
                var model = repository.FirstOrDefault(x => x.Id == pid);
                if (model != null)
                {
                    list.Add(model);
                    if (model.ParentId.HasValue)
                    {
                        GetModel(model.ParentId.Value);
                    }
                }
            }
            GetModel(parentId);
            return Mapper.Map<List<FunctionModuleDto>>(list.OrderBy(x => x.Depth));
        }

        public async Task<List<FunctionModuleDto>> GetSubModulesAsync(long? parentId)
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

            var query = queryable.OrderBy(x => x.OrderIndex).ThenBy(x => x.CreateTime);
            return Mapper.Map<List<FunctionModuleDto>>(await query.ToListAsync());
        }

        #endregion

        #region Action

        public Task<(int count, List<FunctionModuleActionDto> list)> GetActionListAsync(long moduleId, int pageIndex, int pageSize)
        {
            (int, List<FunctionModuleActionDto>) Gets()
            {
                int startRow = (pageIndex - 1) * pageSize;
                var queryable = actionRepository.GetQueryable().Where(x=>x.ModuleId == moduleId);
                var count = queryable.Count();
                var query = queryable.OrderBy(x => x.CreateTime).Skip(startRow).Take(pageSize);
                return (count, Mapper.Map<List<FunctionModuleActionDto>>(query.ToList()));
            }
            return Task.FromResult(Gets());
        }

        public async Task<FunctionModuleActionDto> GetActionByIdAsync(long id)
        {
            return Mapper.Map<FunctionModuleActionDto>(await this.actionRepository.GetByIdAsync(id));
        }

        public async Task SaveActionAsync(FunctionModuleActionDto dto)
        {
            FunctionModuleAction model = Mapper.Map<FunctionModuleAction>(dto);
            if (model.Id == 0)
            {
                //新增
                //取得模块数据             
                var module = await repository.GetByIdAsync(model.ModuleId);
                if (module == null)
                {
                    throw new VinoDataNotFoundException("无法取得模块数据!");
                }
                if (!module.HasCode)
                {
                    module.HasCode = true;
                    repository.Update(module);
                }
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                await actionRepository.InsertAsync(model);
            }
            else
            {
                //更新
                var action = await actionRepository.GetByIdAsync(model.Id);
                if (action == null)
                {
                    throw new VinoDataNotFoundException("无法取得操作码数据!");
                }

                action.Name = model.Name;
                action.Code = model.Code;
                actionRepository.Update(action);
            }
            await actionRepository.SaveAsync();
        }

        public async Task DeleteActionAsync(long id)
        {
            await actionRepository.DeleteAsync(id);
            await actionRepository.SaveAsync();
        }
        #endregion
    }
}
