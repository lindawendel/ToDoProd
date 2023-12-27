using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoCore.Controllers;

namespace ToDoTest
{
    public class ShowToDos_Test : IClassFixture<TestDatabaseFixture>
    {
        public ShowToDos_Test(TestDatabaseFixture fixture)
       => Fixture = fixture;

        public TestDatabaseFixture Fixture { get; }

        [Fact]
        public void Should_Return_All_ToDos_From_Db()
        {

            //ARRANGE

            //Initializes DB with four notes(ToDos) -> See TestDataBaseFixture.cs
            using var context = Fixture.CreateContext();
            var toDoController = new ApiController(context);

            //ACT

            var response = toDoController.Get(null);
            var notesFromDb = context.ToDoNotes;

            //ASSERT

            Assert.Equal(response.Count(), notesFromDb.Count());

        }

        [Fact]
        public void Should_Return_Not_Done_ToDos_From_Db()
        {
            // ARRANGE

            //Initializes DB with four notes(ToDos)
            // Three of these notes are set to IsDone = false.
            using var context = Fixture.CreateContext();
            var toDoController = new ApiController(context);

            //ACT

            var response = toDoController.Get(false);
            var allNotesFromDb = context.ToDoNotes;
            var notDoneNotesFromDb = context.ToDoNotes.Where(x => !x.IsDone).ToList();


            //ASSERT
            Assert.NotEqual(response.Count(), allNotesFromDb.Count());
            Assert.Equal(response.Count(), notDoneNotesFromDb.Count());
            foreach (var note in response)
            {
                Assert.False(note.IsDone);
            }
        
        }

    }
}
