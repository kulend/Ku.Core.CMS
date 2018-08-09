//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：BrandService.cs
// 功能描述：品牌 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-26 11:09
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.Mall;
using Ku.Core.CMS.Domain.Entity.Mall;
using Ku.Core.CMS.IService.Mall;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.Mall
{
    public partial class BrandService : BaseService<Brand, BrandDto, BrandSearch>, IBrandService
    {
        #region 构造函数

        public BrandService()
        {
        }

        #endregion

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<BrandDto> GetByIdWithRefAsync(long id)
        {
            using (var dapper = DapperFactory.Create())
            {
                var categorys = await dapper.QueryListAsync<ProductCategory>("t1.*", "mall_product_category t1",
                    where: new DapperSql("EXISTS (SELECT * FROM mall_brand_category_ref t2 WHERE t2.ProductCategoryId=t1.Id AND t2.BrandId=@BrandId)", new { BrandId = id }),
                    order: null);
                var dto = Mapper.Map<BrandDto>(await dapper.QueryOneAsync<Brand>(new { Id = id }));
                dto.Categorys = Mapper.Map<IEnumerable<ProductCategoryDto>>(categorys);
                return dto;
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(BrandDto dto, long[] CategoryIds)
        {
            Brand model = Mapper.Map<Brand>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;

                var refs = CategoryIds.Select(x=> new BrandCategoryRef { BrandId = model.Id, ProductCategoryId = x });

                using (var dapper = DapperFactory.CreateWithTrans())
                {
                    await dapper.InsertAsync(model);
                    await dapper.InsertAsync(refs);

                    dapper.Commit();
                }
            }
            else
            {
                var refs = CategoryIds.Select(x => new BrandCategoryRef { BrandId = model.Id, ProductCategoryId = x });

                //更新
                using (var dapper = DapperFactory.CreateWithTrans())
                {
                    var item = new {
                        //这里进行赋值
                        model.Name,
                        model.Logo,
                        model.Intro,
                        model.IsEnable
                    };
                    await dapper.UpdateAsync<Brand>(item, new { model.Id });

                    //类目关联
                    await dapper.DeleteAsync<BrandCategoryRef>(new { BrandId = model.Id });
                    await dapper.InsertAsync(refs);

                    dapper.Commit();
                }
            }
        }
    }
}
