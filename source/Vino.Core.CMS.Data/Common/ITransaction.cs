using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.CMS.Data.Common
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
