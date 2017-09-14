using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Data.Common
{
    public class VinoDbContextTransaction : ITransaction
    {
        private IDbContextTransaction _trans;
        public VinoDbContextTransaction(IDbContextTransaction trans)
        {
            this._trans = trans;
        }

        public void Dispose()
        {
            if (this._trans != null)
            {
                this._trans.Dispose();
            }
            this._trans = null;
        }

        public void Commit()
        {
            if (this._trans != null)
            {
                this._trans.Commit();
            }
        }

        public void Rollback()
        {
            if (this._trans != null)
            {
                this._trans.Rollback();
            }
        }
    }
}
