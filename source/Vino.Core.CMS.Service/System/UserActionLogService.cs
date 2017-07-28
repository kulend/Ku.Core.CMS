using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Vino.Core.CMS.Core.Helper;
using Vino.Core.CMS.Data.Common;
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
    }
}
