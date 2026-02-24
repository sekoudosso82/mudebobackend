using Microsoft.EntityFrameworkCore;    
using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelsLayer;


namespace DbLayer
{
    public class MudeboDb : DbContext
    {
        // add tables in DB
        public DbSet<Members> Members { get; set }
        public DbSet<Activities> Activities { get; set}
        public DbSet<Projects> Projects { get; set }
        public Dbset<Logins> Logins { get; set }

        public MudeboDb() { }
        public MudeboDb(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // base.OnConfiguring(optionsBuilder);
        }
    }
}
