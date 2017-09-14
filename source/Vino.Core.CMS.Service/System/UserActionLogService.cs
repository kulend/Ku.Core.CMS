using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;
using Vino.Core.Infrastructure.Helper;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.System
{
    public partial class UserActionLogService
    {
        public async Task AddAsync(UserActionLogDto dto)
        {
            UserActionLog model = _mapper.Map<UserActionLog>(dto);
            if (model != null)
            {
                model.Id = ID.NewID();
                await this._repository.InsertAsync(model);
                await this._repository.SaveAsync();
            }
        }

        public Task<(int count, List<UserActionLogDto> items)> GetListAsync(int page, int size)
        {
            (int, List<UserActionLogDto>) Gets()
            {
                int startRow = (page - 1) * size;
                var queryable = _repository.GetQueryable(x=>x.User);
                var count = queryable.Count();
                var query = queryable.OrderByDescending(x => x.CreateTime).Skip(startRow).Take(size);
                var list = query.ToList();
                var dtos = _mapper.Map<List<UserActionLogDto>>(list);
                foreach (var item in dtos)
                {
                    if (item.User != null)
                    {
                        item.User.Password = "";
                    }
                }
                return (count, dtos);
            }
            return Task.FromResult(Gets());
        }

        public async Task<UserActionLogDto> GetByIdAsync(long id)
        {
            return _mapper.Map<UserActionLogDto>(await this._repository.GetByIdAsync(id));
        }
    }
}
