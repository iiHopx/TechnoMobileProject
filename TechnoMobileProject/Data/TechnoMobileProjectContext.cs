using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechnoMobileProject.Models;

namespace TechnoMobileProject.Data
{
    public class TechnoMobileProjectContext : DbContext
    {
        public TechnoMobileProjectContext (DbContextOptions<TechnoMobileProjectContext> options)
            : base(options)
        {
        }

        public DbSet<TechnoMobileProject.Models.usersaccounts> usersaccounts { get; set; } = default!;
    }
}
