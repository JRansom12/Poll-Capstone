using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PollCapstone.Models;

namespace PollCapstone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<PollMaker> PollMaker { get; set; }

        public DbSet<PollTaker> PollTaker { get; set; }

        public DbSet<RankPoll> RankPoll { get; set; }

        public DbSet<PickOnePoll> PickOnePoll { get; set; }

        public DbSet<SurveyPoll> SurveyPoll { get; set; }
    }
}
