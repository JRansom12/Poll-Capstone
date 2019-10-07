using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PollCapstone.Models
{
    public class RankPoll : Poll
    {
        [Key]
        public int RankId { get; set; }
        [Display(Name = "Poll Name")]
        public string PollName { get; set; }
        [Display(Name = "Is Public")]
        public bool IsPublic { get; set; }
        [Display(Name = "Number of Choices")]
        public int NumberOfChoices { get; set; }
        public string JSONChoices { get; set; }
        [Display(Name = "Polling Status")]
        public string PollingStatus { get; set; }
        [Display(Name = "Poll Start Date")]
        [DataType(DataType.Date)]
        public DateTime PollStartDate { get; set; }
        [Display(Name = "Poll Completion Date")]
        [DataType(DataType.Date)]
        public DateTime PollCompletionDate { get; set; }
        [ForeignKey("PollMaker")]
        public string MakerId { get; set; }
    }
}
