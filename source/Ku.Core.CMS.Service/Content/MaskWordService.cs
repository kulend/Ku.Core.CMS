//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：MaskWordService.cs
// 功能描述：屏蔽关键词 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-03 14:22
//
//----------------------------------------------------------------

using AutoMapper;
using Dapper;
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using Ku.Core.CMS.IService.Content;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.Content
{
    public partial class MaskWordService : BaseService<MaskWord, MaskWordDto, MaskWordSearch>, IMaskWordService
    {
        #region 构造函数

        public MaskWordService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(MaskWordDto dto)
        {
            MaskWord model = Mapper.Map<MaskWord>(dto);
            model.Tag = model.Tag.Replace("，", ",");
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.MatchCount = 0;
                using (var dapper = DapperFactory.Create())
                {
                    await dapper.InsertAsync(model);
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.Create())
                {
                    var item = new {
                        //这里进行赋值
                        model.Word,
                        model.Level,
                        model.Tag
                    };
                    await dapper.UpdateAsync<MaskWord>(item, new { model.Id });
                }
            }
        }

        /// <summary>
        /// 取得所有标签
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetTags()
        {
            using (var dapper = DapperFactory.Create())
            {
                var tags =  await dapper.Connection.QueryAsync<string>("select Tag from content_maskword");
                List<string> list = new List<string>();
                var allTags = tags.Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries));
                foreach (var items in allTags)
                {
                    list.AddRange(items);
                }

                return list.Distinct();
            }
        }
    }
}
