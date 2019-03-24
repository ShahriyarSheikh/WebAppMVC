using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppData.Models
{
    public class UserSessions
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string MovieName { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Required]
        public DateTime SearchedOn { get; set; }
    }
}
