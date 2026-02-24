using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MemberService : IMember
    {
        Task<bool> IMember.AddMemberAsync(MemberService member)
        {
            throw new NotImplementedException();
        }

        Task<bool> IMember.DeleteMemberAsync(int memberId)
        {
            throw new NotImplementedException();
        }

        Task<bool> IMember.EditMemberAsync(MemberService member, MemberService memberUpdated)
        {
            throw new NotImplementedException();
        }

        Task<MemberService?> IMember.FindMemberAsync(int memberId)
        {
            throw new NotImplementedException();
        }

        Task<List<MemberService>> IMember.MemberListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
