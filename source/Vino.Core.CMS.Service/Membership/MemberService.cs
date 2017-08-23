using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vino.Core.CMS.Domain.Dto.Membership;
using Vino.Core.CMS.Domain.Entity.Membership;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.Membership
{
    public partial class MemberService
    {
        public async Task<(int count, List<MemberDto> items)> GetListAsync(int page, int size)
        {
            var includes = new List<Expression<Func<Member, object>>>();
            includes.Add(x=>x.MemberType);
            var data = await _repository.PageQueryAsync(page, size, null, "CreateTime asc", includes.ToArray());
            return (data.count, _mapper.Map<List<MemberDto>>(data.items));
        }

        public async Task<MemberDto> GetByIdAsync(long id)
        {
            return _mapper.Map<MemberDto>(await this._repository.GetByIdAsync(id));
        }

        public async Task SaveAsync(MemberDto dto)
        {
            Member model = _mapper.Map<Member>(dto);
            if (model.Id == 0)
            {
                //新增
                //检查手机号
                if (model.Mobile.IsNotNullOrEmpty())
                {
                    //格式
                    if (!model.Mobile.IsMobile())
                    {
                        throw new VinoDataNotFoundException("手机号格式不正确！");
                    }
                    //是否重复
                    var cnt = await _repository.GetQueryable().Where(x => x.Mobile.EqualOrdinalIgnoreCase(model.Mobile)).CountAsync();
                    if (cnt > 0)
                    {
                        throw new VinoDataNotFoundException("手机号重复！");
                    }
                }

                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                model.Points = 0;
                model.Level = 0;
                var random = new Random();
                model.Factor = random.Next(9999);
                //密码设置
                model.EncryptPassword();
                await _repository.InsertAsync(model);
            }
            else
            {
                //更新
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得会员数据！");
                }

                item.Name = model.Name;
                item.Mobile = model.Mobile;
                item.IsEnable = model.IsEnable;
                item.Sex = model.Sex;
                if (!model.Password.EqualOrdinalIgnoreCase("the password has not changed"))
                {
                    item.Password = model.Password;
                    //密码设置
                    item.EncryptPassword();
                }
                item.MemberTypeId = model.MemberTypeId;
                _repository.Update(item);
            }
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }
    }
}
