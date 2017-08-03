using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Vino.Core.Cache;
using Vino.Core.CMS.Data.Common;

namespace Vino.Core.CMS.Service
{
    public abstract class BaseService
    {
        protected readonly VinoDbContext context;
        protected readonly ICacheService _cache;
        protected readonly IMapper _mapper;

        protected BaseService(VinoDbContext context, ICacheService cache, IMapper mapper)
        {
            this.context = context;
            this._cache = cache;
            this._mapper = mapper;
        }
    }
}
