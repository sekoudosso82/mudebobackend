using BusinessLayer;
using DbLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsLayer;
using System.Data;
using System.Numerics;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MudeboAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MembersController : ControllerBase
    {
        // properties
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<MembersController> _logger;
        private readonly IMembers _members;
        private readonly MudeboDb _mudeboDb;

        // constructor to inject in business layer (MembersService)
        public MembersController(ILogger<MembersController> logger, 
            IMembers member, MudeboDb mudeboDb, IWebHostEnvironment environment)
        {
            _logger = logger;
            _members = member;
            _mudeboDb = mudeboDb;
            _environment = environment;
        }
        // Methods
        [AllowAnonymous]
        [HttpGet]// GET all Members
        public async Task<IEnumerable<Members>> Get()
        {
            List<Members> members = await _members.MembersListAsync();
            return members;
        }

        [AllowAnonymous]
        [HttpPost("CreateMember")] // create new CreateMemberWithPhoto
        public async Task<IActionResult> CreateMember([FromForm] MemberDto dto)
        {
            var memberPhotoPath = " ";

            if (dto.MemberPhotoUrl != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "member-photo");
                if (!Directory.Exists(uploadsFolder)) { Directory.CreateDirectory(uploadsFolder); }


                var extension = Path.GetExtension(dto.MemberPhotoUrl.FileName);
                string fileName = Guid.NewGuid().ToString() + dto.MemberPhotoUrl.FileName;

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                { await dto.MemberPhotoUrl.CopyToAsync(stream); }
                var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{fileName}";
                memberPhotoPath = $"{Request.Scheme}://{Request.Host}/member-photo/{fileName}";
            }

            var member = new Members
            {
                //MemberId = dto.MemberId,
                Nom = dto.Nom,
                Prenoms = dto.Prenoms,
                UserName = dto.UserName,
                Password = dto.Password,
                Role = dto.Role,
                AccessLevel= dto.AccessLevel,
                Location = dto.Location,
                Phone = dto.Phone,
                Email = dto.Email,
                MemberPhotoUrl = memberPhotoPath,
                DateJoined = DateTime.UtcNow,
                IsActive = true,
            };

            _mudeboDb.Members.Add(member);
            await _mudeboDb.SaveChangesAsync();
            return Ok(member.MemberId);
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
        [HttpPost("RegisterMember")] // create new member
        // [Authorize(Roles = "Admin")]
        public async Task<bool> RegisterMemberAsync(Members mem)
        {
            await _mudeboDb.Members.AddAsync(mem);
            try
            { await _mudeboDb.SaveChangesAsync(); }
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

        [AllowAnonymous]
        [HttpPut("{memberId}")] // PUT api/<MembersController>/5
        // [Authorize(Roles = "Admin")]
        public async Task<bool> EditMember(int memberId, [FromForm] MemberDto dto)
        {
            var memberPhotoPath = " ";

            if (dto.MemberPhotoUrl != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "member-photo");
                if (!Directory.Exists(uploadsFolder)) { Directory.CreateDirectory(uploadsFolder); }


                var extension = Path.GetExtension(dto.MemberPhotoUrl.FileName);
                //string fileName = Guid.NewGuid().ToString() + extension;
                string fileName = Guid.NewGuid().ToString() + dto.MemberPhotoUrl.FileName;

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                { await dto.MemberPhotoUrl.CopyToAsync(stream); }
                var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{fileName}";
                memberPhotoPath = $"{Request.Scheme}://{Request.Host}/member-photo/{fileName}";
            }
            var updateMember = new Members
            {
                // MemberId = dto.MemberId,
                Nom = dto.Nom,
                Prenoms = dto.Prenoms,
                UserName = dto.UserName,
                Password = dto.Password,
                Phone = dto.Phone,
                Email = dto.Email,
                Role = dto.Role,
                AccessLevel = dto.AccessLevel,
                MemberPhotoUrl = memberPhotoPath,
                Location = dto.Location,
                DateJoined = DateTime.UtcNow,
                IsActive = true,
            };
            try
            {
                var mem = await _mudeboDb.Members.FindAsync(memberId);
                if(mem != null)
                {
                    await _members.EditMemberAsync(memberId, updateMember);
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

        [AllowAnonymous]
        [HttpDelete("{id}")] // DELETE api/<MembersController>/5
        //[Authorize(Roles = "Admin")]
        public async Task<bool> DeleteMember(int memberId)
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
