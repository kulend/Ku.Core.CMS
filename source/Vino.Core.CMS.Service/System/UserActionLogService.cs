using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Service.System
{
    public class UserActionLogService : IUserActionLogService
    {
        private IUserActionLogRepository repository;

        public UserActionLogService(IUserActionLogRepository repository)
        {
            this.repository = repository;
        }

        public void AddAsync(UserActionLogDto dto)
        {
            UserActionLog model = Mapper.Map<UserActionLog>(dto);
            if (model != null)
            {
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                 this.repository.Insert(model);
                 this.repository.Save();
            }
        }

        public Task<(int count, List<UserActionLogDto> items)> GetListAsync(int page, int size)
        {
            (int, List<UserActionLogDto>) Gets()
            {
                int startRow = (page - 1) * size;
                var queryable = repository.GetQueryable(x=>x.User);
                var count = queryable.Count();
                var query = queryable.OrderByDescending(x => x.CreateTime).Skip(startRow).Take(size);
                var list = query.ToList();
                var dtos = Mapper.Map<List<UserActionLogDto>>(list);
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
    }
}
