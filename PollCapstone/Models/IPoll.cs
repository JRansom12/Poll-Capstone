using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PollCapstone.Models
{
    public interface IPoll
    {
        [Display(Name = "Poll Name")]
        string PollName { get; set; }
        [Display(Name = "Is Public")]
        bool IsPublic { get; set; }
        [Display(Name = "Poll Start Date")]
        [DataType(DataType.Date)]
        DateTime PollStartDate { get; set; }
        [Display(Name = "Poll Completion Date")]
        [DataType(DataType.Date)]
        DateTime PollCompletionDate { get; set; }
    }
}
