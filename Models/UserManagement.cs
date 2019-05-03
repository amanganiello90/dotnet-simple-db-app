using System.ComponentModel.DataAnnotations;

namespace dotnet_tutorial_app.Models {
    public class UserManagement {

        [Key]
        public int UserManagementID { get; set; }

        [Required]
        [MaxLength (128)]
        public string Username { get; set; }

        [Required]
        [MaxLength (256)]
        public string Password { get; set; }

    }
}