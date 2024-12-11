using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
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
        public DbSet<TechnoMobileProject.Models.items> items { get; set; } = default!;
        public DbSet<TechnoMobileProject.report> report { get; set; } = default!;
        public DbSet<TechnoMobileProject.Models.orders> orders { get; set; } = default!;
        public DbSet<TechnoMobileProject.Models.orderline> orderline { get; set; }
    }
}
