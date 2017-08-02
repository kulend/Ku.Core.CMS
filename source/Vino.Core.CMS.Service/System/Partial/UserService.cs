using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Data.Repository.System;

namespace Vino.Core.CMS.Service.System.Partial
{
    public partial interface IUserService
    {
    }

    public partial class UserService
    {
        protected readonly IUserRepository repository;

        public UserService(IUserRepository _repository)
        {
            this.repository = _repository;
        }
    }
}
