using BusinessLayer;
using DbLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsLayer;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MudeboAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MembersController : ControllerBase
    {
        // properties
        private readonly ILogger<MembersController> _logger;
        private readonly IMembers _members;
        private readonly MudeboDb _mudeboDb;

        // constructor to inject in business layer (MembersService)
        public MembersController(ILogger<MembersController> logger, IMembers member, MudeboDb mudeboDb)
        {
            _logger = logger;
            _members = member;
            _mudeboDb = mudeboDb;
        }
        // Methods
        [AllowAnonymous]
        [HttpGet]// GET all Members
        public async Task<IEnumerable<Members>> MembersList()
        {
            List<Members> members = await _members.MembersListAsync();
            return members;
        }

        [AllowAnonymous]
        [HttpGet("{id}")] // GET api/<MembersController>/5
        public async Task<Members> GetMember(int memberId)
        {
            List<Members> membersList = await _members.MembersListAsync();
            var returnedMember = membersList.Where(x=>x.MemberId==memberId).FirstOrDefault();
            return returnedMember;
        }

        [AllowAnonymous]
        [HttpPost("CreateNewMember")] // create new member
        // [Authorize(Roles = "Admin")]
        public async Task<bool> CreateNewMemberAsync(Members mem)
        {
            await _mudeboDb.Members.AddAsync(mem);
            try
            { await _mudeboDb.SaveChangesAsync(); }
            catch(DbUpdateConcurrencyException ex)
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

        [HttpPut("{memberId}")] // PUT api/<MembersController>/5
        // [Authorize(Roles = "Admin")]
        public async Task<bool> EditMemberA(int memberId, Members memberUpdated)
        {
            try
            {
                var mem = await _mudeboDb.Members.FindAsync(memberId);
                if(mem != null)
                { await _members.EditMemberAsync(mem, memberUpdated);} 
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

        [HttpDelete("{id}")] // DELETE api/<MembersController>/5
        [Authorize(Roles = "Admin")]
        public async Task<bool> DeleteMemberA(int memberId)
        {
            try
            {
                var mem = await _mudeboDb.Members.FindAsync(memberId);
                if (mem != null)
                { 
                    _mudeboDb.Remove(mem);
                    await _mudeboDb.SaveChangesAsync();
                }
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
    }
}
