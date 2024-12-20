using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LDCAPIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LDCAPIProject.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<UserQuery> userQueries { get; set; }
        public DbSet<ChatBotResponse> chatBotResponses { get; set; }
    }
}