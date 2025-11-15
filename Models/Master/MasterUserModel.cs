using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Master
{
    public class MasterUserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama wajib diisi.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email wajib diisi.")]
        [EmailAddress(ErrorMessage = "Format email tidak valid.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "No Telepon wajib diisi.")]
        public string? Telp { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Password dan Konfirmasi Password harus sama.")]
        public string? PasswordConfirmation { get; set; }
    }
}
