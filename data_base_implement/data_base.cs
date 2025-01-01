using contracts.storage_contracts.db_models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_base_implement {
    public class data_base : DbContext {
        public DbSet<Department> departments { get; set; }
        public DbSet<Direction> directions { get; set; } 
        public DbSet<Document> documets { get; set; } 
        public DbSet<Faculty> faculties { get; set; }
        public DbSet<Student> students { get; set; } 
        public DbSet<StudentGroup> student_groups { get; set; } 
        public DbSet<Template> templates { get; set; } 
        public DbSet<User> users { get; set; } 
        public DbSet<StudentGroupUser> studentGroupUsers { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (optionsBuilder.IsConfigured == false) {
                optionsBuilder.UseSqlServer(@"Data Source=FEDOTOVILIA\SQLEXPRESS;
                                            Initial Catalog=AutomationOfTheEducationalProcess;
                                            Integrated Security=True;
                                            MultipleActiveResultSets=True;
                                            ;
                                            TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().HasMany(c => c.student_groups).WithMany(p => p.users)
                .UsingEntity<StudentGroupUser>();
        }
    }
}
