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
        [Required]
        [Key]
        public int MemberId { get; set; }
        public string? Nom { get; set; }
        public string? Prenoms { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Location { get; set; }
        public int? Phone { get; set; }
        public string? Email {  get; set; }
        public string? Status { get; set; }
        public string? Role { get; set; }
        public string? Photo { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsActive { get; set; }
        public Members() { }
        public Members(int memberId, string nom, string prenoms,string userName, string password,string role, string location, 
            int phone, string email, string status, string photo, DateTime dateJoint, bool isActive ) 
        {
            MemberId = memberId;
            Nom = nom;
            Prenoms = prenoms;
            UserName = userName;
            Password = password;
            Role = role;
            Location = location;
            Phone = phone;
            Email = email;
            Status = status;
            Photo = photo;
            DateJoined = dateJoint;
            IsActive = isActive;
        }
    }
}
