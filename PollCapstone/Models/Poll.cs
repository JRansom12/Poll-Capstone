using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PollCapstone.Models
{
    public abstract class Poll
    {
        [Key]
        public int PollId { get; set; }
        [Display(Name = "Poll Name")]
        public string PollName { get; set; }
        [Display(Name = "Is Public")]
        public bool IsPublic { get; set; }
        [Display(Name = "Poll Start Date")]
        [DataType(DataType.Date)]
        public DateTime PollStartDate { get; set; }
        [Display(Name = "Poll Completion Date")]
        [DataType(DataType.Date)]
        public DateTime PollCompletionDate { get; set; }
    }
}
