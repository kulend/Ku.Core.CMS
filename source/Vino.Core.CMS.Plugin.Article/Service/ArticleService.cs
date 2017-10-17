using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vino.Core.CMS.Plugin.Article.Models.Dto;
using Vino.Core.CMS.Plugin.Article.Repository;

namespace Vino.Core.CMS.Plugin.Article.Service
{
    public class ArticleService: IArticleService
    {
        protected readonly IMapper _mapper;
        protected readonly IArticleRepository _repository;

        public ArticleService(IMapper mapper, IArticleRepository repository)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        public async Task<(int count, List<ArticleDto> items)> GetListAsync(int page, int size)
        {
            //return (0, new List<ArticleDto>());
            var data = await _repository.PageQueryAsync(page, size, null, "CreateTime asc");
            return (data.count, _mapper.Map<List<ArticleDto>>(data.items));
        }
    }
}
