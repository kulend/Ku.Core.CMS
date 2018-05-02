//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：ColumnRepository.cs
// 功能描述：栏目 数据访问
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-23 14:15
// 说明：此代码由工具自动生成，对此文件的更改可能会导致不正确的行为，
// 并且如果重新生成代码，这些更改将会丢失。
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain.Entity.Content;

namespace Vino.Core.CMS.Data.Repository.Content
{
    /// <summary>
    /// 栏目 仓储接口
    /// </summary>
    public partial interface IColumnRepository : IRepository<Column>
    {
    }

    /// <summary>
    /// 栏目 仓储接口实现
    /// </summary>
    public partial class ColumnRepository : BaseRepository<Column>, IColumnRepository
    {
        public ColumnRepository(VinoDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}