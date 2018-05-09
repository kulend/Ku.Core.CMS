using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.CMS.Data.Common
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
