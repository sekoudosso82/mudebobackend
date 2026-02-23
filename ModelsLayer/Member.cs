using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLayer
{
    public class Member
    {
        [Required]
        [Key]
        public int MemberId { get; set; }
        public string? Nom { get; set; }
        public string? Prenoms { get; set; }
        public string? Location { get; set; }
        public int? Phone { get; set; }
        public string? Email {  get; set; }
        public string? Status { get; set; }
        public string? Photo { get; set; }
        public Member() { }
        public Member(int memberId, string nom, string prenoms, string location, int phone, string email, string status, string photo) 
        {
            MemberId = memberId;
            Nom = nom;
            Prenoms = prenoms;
            Location = location;
            Phone = phone;
            Email = email;
            Status = status;
            Photo = photo;
        }
    }
}
