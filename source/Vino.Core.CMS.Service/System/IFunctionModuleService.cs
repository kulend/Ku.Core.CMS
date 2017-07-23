using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Domain.Dto.System;

namespace Vino.Core.CMS.Service.System
{
    public interface IFunctionModuleService
    {
        Task<(int count, List<FunctionModuleDto> list)> GetListAsync(long? parentId, int pageIndex, int pageSize);

        Task SaveAsync(FunctionModuleDto dto);

        Task<List<FunctionModuleDto>> GetParentsAsync(long parentId);

        Task<FunctionModuleDto> GetByIdAsync(long id);

        Task DeleteAsync(long id);

        Task<List<FunctionModuleDto>> GetSubModulesAsync(long? parentId);


        Task<(int count, List<FunctionModuleActionDto> list)> GetActionListAsync(long moduleId, int pageIndex,
            int pageSize);

        Task<FunctionModuleActionDto> GetActionByIdAsync(long id);

        Task SaveActionAsync(FunctionModuleActionDto dto);

        Task DeleteActionAsync(long id);
    }
}
