using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IMember
    {
        Task<List<MemberService>> MemberListAsync();
        Task<MemberService?> FindMemberAsync(int memberId);
        Task<bool> AddMemberAsync(MemberService member);
        Task<bool> EditMemberAsync(MemberService member, MemberService memberUpdated);
        Task<bool> DeleteMemberAsync(int memberId);

    }
}
