using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDBContext(DbContextOptions options) : DbContext(options)
    {
        public required DbSet<UserQuery> UserQueries { get; set; }
        public required DbSet<ChatBotResponse> ChatBotResponses { get; set; }

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
            .HasForeignKey<ChatBotResponse>(r => r.queryId);
        }
    }
}