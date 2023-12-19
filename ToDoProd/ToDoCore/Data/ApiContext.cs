using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using ToDoCore.Models;

namespace ToDoCore.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext()
        { }

        public ApiContext(DbContextOptions<ApiContext> options)
                   : base(options)
        {
        }

        public DbSet<ToDoNote> ToDoNotes { get; set; } = default!;
    }
}
