using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer
{
    public class Members
    {
        //[Required]
        [Key]
        public int? MemberId { get; set; }
        [Required]
        public string? Nom { get; set; }
        [Required]
        public string? Prenoms { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Location { get; set; }
        //[Required]
        public int? Phone { get; set; }
        public string? Email {  get; set; }
        public string? Role { get; set; }
        public string? AccessLevel { get; set; }
        public DateTime? DateJoined { get; set; }
        public string? MemberPhotoUrl { get; set; }
        public bool? IsActive { get; set; }
        public Members() { }
        public Members(int memberId, string nom, string prenoms,
            string userName, string password, string location, 
            int phone, string email, string role,string accessLevel, DateTime dateJoined,
            string memberPhotoUrl, bool isActive ) 
        {
            MemberId = memberId;
            Nom = nom;
            Prenoms = prenoms;
            UserName = userName;
            Password = password;
            Location = location;
            Phone = phone;
            Email = email;
            Role = role;
            AccessLevel = accessLevel;
            DateJoined = dateJoined;
            MemberPhotoUrl = memberPhotoUrl;
            IsActive = isActive;
        }
    }
}
