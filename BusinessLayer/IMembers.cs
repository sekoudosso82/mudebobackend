using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IMembers
    {
        Task<List<Members>> MembersListAsync();
        Task<Members?> FindMemberAsync(int memberId);
        Task<bool> AddMemberAsync(Members member);
        Task<bool> EditMemberAsync(int memberId, Members member);
        Task<bool> DeleteMemberAsync(int memberId);

    }
}
