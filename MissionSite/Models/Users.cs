using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MissionSite.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter a valid email address.")]
        public string UserEmail { get; set; }

        [DisplayName("Password")]
        public string UserPassword { get; set; }
        [DisplayName("First Name")]
        public string UserFirstName { get; set; }
        [DisplayName("Last Name")]
        public string UserLastName { get; set; }

    }
}