using System.IO;

namespace Vino.Core.Tools.VerificationCode
{
    public interface IVerificationCodeGen
    {
        MemoryStream Create(out string code, int numbers = 4);
    }
}
