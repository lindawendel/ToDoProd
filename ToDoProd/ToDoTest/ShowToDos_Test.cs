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


            //Initializes DB with four notes(ToDos)
            using var context = Fixture.CreateContext();
            var toDoController = new ApiController(context);


            //ACT

            var response = toDoController.Get(null);
            var notesFromDb = context.ToDoNotes;

            //ASSERT

            Assert.Equal(response.Count(), notesFromDb.Count());

        }




    }

}
