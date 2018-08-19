using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.MaterialCenter
{
    public class MaterialCenterConfigService : IMaterialCenterConfigService
    {
        private readonly ICacheProvider _cache;

        #region 构造函数

        public MaterialCenterConfigService(ICacheProvider cache)
        {
            _cache = cache;
        }

        #endregion

        public async Task<MaterialCenterConfig> GetAsync()
        {
            var key = string.Format(CacheKeyDefinition.Config, nameof(MaterialCenterConfig).ToLower());
            if (await _cache.ExistsAsync(key))
            {
                return await _cache.GetAsync<MaterialCenterConfig>(key);
            }
            return new MaterialCenterConfig();
        }

        public async Task SetAsync(MaterialCenterConfig config)
        {
            var key = string.Format(CacheKeyDefinition.Config, nameof(MaterialCenterConfig).ToLower());
            await _cache.SetAsync(key, config);
        }
    }
}
