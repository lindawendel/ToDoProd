using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ToDoCore.Data;
using ToDoCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoTest
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=ToDoNotes;Trusted_Connection=True";
            //@"Server=(localdb)\mssqllocaldb;Database=EFTestSample;Trusted_Connection=True";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public TestDatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                        context.AddRange(
                            new ToDoNote { Id = 1, Text = "Milk", IsDone = false },
                            new ToDoNote { Id = 1, Text = "Butter", IsDone = false },
                            new ToDoNote { Id = 1, Text = "Bread", IsDone = true},
                            new ToDoNote { Id = 1, Text = "Tomatoes", IsDone = false });
                        context.SaveChanges();
                    }
                   
                }

                    _databaseInitialized = true;
                }
            }
        public ApiContext CreateContext()
           => new ApiContext(
               new DbContextOptionsBuilder<ApiContext>()
                   .UseSqlServer(ConnectionString)
                   .Options);
    }

}
