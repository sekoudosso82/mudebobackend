using Microsoft.EntityFrameworkCore;    
using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DbLayer
{
    public class MudeboDb : DbContext
    {
        // add tables in DB
        public DbSet<Members> Members { get; set; }
        public DbSet<Activities> Activities { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Logins> Logins { get; set; }

        public MudeboDb() { }
        // public MudeboDb(DbContextOptions options) : base(options) { }
        public MudeboDb(DbContextOptions<MudeboDb> options ) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // base.OnConfiguring(optionsBuilder);
        }
    }
}
