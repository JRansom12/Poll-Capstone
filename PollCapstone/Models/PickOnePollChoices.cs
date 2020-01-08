using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PollCapstone.Models
{
    public class PickOnePollChoices
    {
        [Key]
        public int PickOneChoicesId { get; set; }
        [ForeignKey("PickOnePoll")]
        public string PickOneId { get; set; }

    }
}
