using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoCore.Controllers;

namespace ToDoTest
{
    public class RemoveToDo_Test : IClassFixture<TestDatabaseFixture>
    {
        //REQUIREMENTS
        // when an API-request to DeleteNote is made, we expect that
        // the ToDo is removed and the count is updated. 

        public RemoveToDo_Test(TestDatabaseFixture fixture)
       => Fixture = fixture;

        public TestDatabaseFixture Fixture { get; }


        [Fact]
        public void Should_Throw_Exception_On_Attempt_To_Remove_Nonexisting_ToDo()
        {
            //ARRANGE

            //Initialize Db with four notes (see TestDataBaseFixture.cs )
            using var context = Fixture.CreateContext();
            var toDoController = new ApiController(context);

            //ACT & ASSERT
            Assert.Throws<Exception>(() => toDoController.DeleteNote(10));
        }

        [Fact]
        public void Should_Remove_ToDo_With_A_Specified_Id()
        {
            //ARRANGE

            //Initialize Db with four notes (see TestDataBaseFixture.cs )
            using var context = Fixture.CreateContext();
            var toDoController = new ApiController(context);

            //ACT

            toDoController.DeleteNote(1);
            var response = toDoController.Get(null);

            Assert.Equal(3, response.Count());
        }

    }
}
