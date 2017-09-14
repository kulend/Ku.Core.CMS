using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Entity.System;
using Vino.Core.EventBus;
using Vino.Core.EventBus.CAP;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.Events.System
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
