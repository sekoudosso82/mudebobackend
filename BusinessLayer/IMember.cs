using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IMember
    {
        Task<List<Member>> MemberListAsync();
        Task<Member?> FindMemberAsync(int memberId);
        Task<bool> AddMemberAsync(Member member);
        Task<bool> EditMemberAsync(Member member, Member memberUpdated);
        Task<bool> DeleteMemberAsync(int memberId);

    }
}
