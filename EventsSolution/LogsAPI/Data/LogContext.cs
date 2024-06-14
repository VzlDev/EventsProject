using Microsoft.EntityFrameworkCore;
using LogsAPI.Model;

namespace LogsAPI.Data
{
    public class LogContext : DbContext
    {
        public LogContext(DbContextOptions<LogContext> options)
            : base(options)
        {
        }

        public DbSet<LogEntry> LogEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configuration if needed
        }
    }
}
