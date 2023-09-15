using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.Models;
namespace TaskManagement.Data
{
    public class TaskManagementDbContext : DbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options):base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Quote> Quotes { get; set; }
    }
}
