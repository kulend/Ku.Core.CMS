using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ku.Core.CMS.Data.Repository.System;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.EventBus;
using Ku.Core.EventBus.CAP;
using Ku.Core.Infrastructure.IdGenerator;

namespace Ku.Core.CMS.Service.Events.System
{
    public interface IUserActionLogEventSubscriberService
    {
        Task SaveUserActionLog(UserActionLogDto dto);
    }

    public class UserActionLogEventSubscriberService: IUserActionLogEventSubscriberService, IEventSubscriberService
    {
        protected readonly IUserActionLogRepository _repository;

        public UserActionLogEventSubscriberService(IUserActionLogRepository repository)
        {
            this._repository = repository;
        }

        [EventSubscribe("backend_user_action_log")]
        public async Task SaveUserActionLog(UserActionLogDto dto)
        {
            UserActionLog model = Mapper.Map<UserActionLog>(dto);
            if (model != null)
            {
                model.Id = ID.NewID();
                await this._repository.InsertAsync(model);
                await this._repository.SaveAsync();
            }
        }
    }
}
