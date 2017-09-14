using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.Infrastructure.Data
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
