using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MotoScore.Models;

namespace MotoScore.Data
{
    public class MotoScoreContext : DbContext
    {
        public MotoScoreContext (DbContextOptions<MotoScoreContext> options)
            : base(options)
        {
        }

        public DbSet<Tourmanent> Tourmanent { get; set; } = default!;
        public DbSet<Contender> Contender { get; set; } = default!;
    }
}
