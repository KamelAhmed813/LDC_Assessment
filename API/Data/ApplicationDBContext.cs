using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<UserQuery> userQueries { get; set; }
        public DbSet<ChatBotResponse> chatBotResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserQuery>()
                .Property(u => u.timestamp)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ChatBotResponse>()
                .Property(r => r.timestamp)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<UserQuery>()
                .HasOne(u => u.response)
                .WithOne(r => r.query)
            .HasForeignKey<ChatBotResponse>(r => r.queryID);
        }
    }
}