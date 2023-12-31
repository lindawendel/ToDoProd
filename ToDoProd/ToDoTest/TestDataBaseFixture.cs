﻿using System;
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

        public ApiContext CreateContext()
           => new ApiContext(
               new DbContextOptionsBuilder<ApiContext>()
                   .UseSqlServer(ConnectionString)
                   .Options);

        public TestDatabaseFixture()
        {
            using var context = CreateContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Cleanup();
        }

        public void Cleanup()
        {
            using var context = CreateContext();

            context.ToDoNotes.RemoveRange(context.ToDoNotes);

            context.AddRange(
                        new ToDoNote { Text = "Milk", IsDone = false },
                        new ToDoNote { Text = "Butter", IsDone = false },
                        new ToDoNote { Text = "Bread", IsDone = true },
                        new ToDoNote { Text = "Tomatoes", IsDone = false });
            context.SaveChanges();
        }

    }
    
    [CollectionDefinition("Tests")]
    public class TestsCollection : ICollectionFixture<TestDatabaseFixture>
    {
    }

}