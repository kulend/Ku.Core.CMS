using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.Material;

namespace Vino.Core.CMS.Service.Material
{
    public partial interface IPictureService
    {
        Task SaveAsync(PictureDto dto);
    }
}
