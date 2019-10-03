using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PollCapstone.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        //public string Gender { get; set; }
        //public int Age { get; set; }
        //public enum Genders
        //{
        //    Male,
        //    Female
        //}
        [NotMapped]
        public bool IsSuperAdmin { get; set; }
        public string Role { get; internal set; }
    }
}
