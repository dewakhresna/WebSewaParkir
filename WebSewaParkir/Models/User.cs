using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebSewaParkir.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        [Required]
        [StringLength(25)]
        public string Email { get; set; }

        [StringLength(15)]
        public string Telp { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
    }
}