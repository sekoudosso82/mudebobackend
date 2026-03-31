using DbLayer;
using Microsoft.EntityFrameworkCore;
using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MembersService : IMembers

    {
        private readonly MudeboDb _context;
        //constructor
        public MembersService(MudeboDb context)
        {
            _context = context;
        }
        public async Task<bool> AddMemberAsync(Members member)
        {
            member.DateJoined = DateTime.UtcNow;
            member.IsActive = true;
            await _context.AddAsync(member);

            try
            { await _context.SaveChangesAsync();}
            catch (DbUpdateConcurrencyException ex) { 
                Console.WriteLine($"there was a problem updating the db => {ex.InnerException}");
                return false;
            }
            return true;
        }
        

        public async Task<bool> DeleteMemberAsync(int memberId)
        {
            var memb = await _context.Members.FindAsync(memberId);
            try
            {
                if (memb != null)
                {
                    _context.Members.Remove(memb);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"there was a problem updating the db => {ex.InnerException}");
                return false;
            }
            catch(DbUpdateException ex)
            {
                Console.WriteLine($"there was a problem updating the db => {ex.InnerException}");
                return false;
            }
            return true;

        }
        

        public async Task<bool> EditMemberAsync(Members member, Members memberUpdated)
        {
            var memb = await _context.Members.FindAsync(member.MemberId);
            try
            {
                if (memb != null)
                {
                    memb.Nom = memberUpdated.Nom;
                    memb.Prenoms = memberUpdated.Prenoms;
                    memb.Location = memberUpdated.Location;
                    memb.Phone = memberUpdated.Phone;
                    memb.Email = memberUpdated.Email;
                    memb.Status = memberUpdated.Status;
                    memb.Photo = memberUpdated.Photo;
                    memb.DateJoined = memberUpdated.DateJoined;
                    memb.IsActive = memberUpdated.IsActive;
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"there was a problem updating the db => {ex.InnerException}");
                return false;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"there was a problem updating the db => {ex.InnerException}");
                return false;
            }
            return true;
        }
        public async Task<Members?> FindMemberAsync(int memberId)
        {
            var result = new Members();
            var noResult = new Members();
            try
            {
                var m = await _context.Members.SingleOrDefaultAsync(x => x.MemberId == memberId);
                if(m!=null) { result = m; }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"there was a problem finding this member => { ex.InnerException}");
                return noResult;
            }
            return result;
        }

        public async Task<List<Members>> MembersListAsync()
        {
            List<Members> noResult = new List<Members>();
            var mList = new List<Members>();
            try
            {
                if(await _context.Members.ToListAsync() is not null)
                {
                    mList = await _context.Members.ToListAsync();
                }
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine($"there was a problem finding this member => {ex.InnerException}");
                return noResult;
            }
            return mList;
        }
    }
}
