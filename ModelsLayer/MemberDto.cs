using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer
{
    public class MemberDto
    {
        [Key]
        public int? MemberId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        [StringLength(20, MinimumLength = 3,
        ErrorMessage = "Username must be between 3 and 20 characters.")]
        public string? Nom { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Prenoms must be between 3 and 50 characters.")]
        public string? Prenoms { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        [StringLength(20, MinimumLength = 3,
        ErrorMessage = "Username must be between 3 and 20 characters.")]
        public string? UserName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")]
        [StringLength(20, MinimumLength = 3,
        ErrorMessage = "Password must be between 3 and 20 characters.")]
        public string? Password { get; set; }
        public string? Location { get; set; }
        //[Required]
        public int? Phone { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }  // Admin or Member
        public string? Statut { get; set; }
        public DateTime? DateJoined { get; set; }
        public IFormFile? MemberPhotoUrl { get; set; }
        public bool? IsActive { get; set; }
    }
}
