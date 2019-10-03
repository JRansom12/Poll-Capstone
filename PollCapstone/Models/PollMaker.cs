using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PollCapstone.Models
{
    public class PollMaker
    {
        [Key]
        public int MakerId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        [ForeignKey("User Id")]
        public string ApplicationUserId { get; set; }
    }
    public enum Genders
    {
        Male,
        Female
    }
}
