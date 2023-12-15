using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoCore.Controllers;
using ToDoCore.Models;

namespace ToDoTest
{
    public class AddToDoTest : IClassFixture<TestDatabaseFixture>
    {
        //REQUIREMENTS
        // when data is retrieved from db, we expect that
        // all details included

        public AddToDoTest(TestDatabaseFixture fixture)
        => Fixture = fixture;

        public TestDatabaseFixture Fixture { get; }
    

        [Fact]
        public void Should_Return_New_Todo_With_Details()
        {
            // ARRANGE

            using var context = Fixture.CreateContext();
            var toDoController = new ApiController(context);

            var newToDo = new ToDoNote
            {
                Text = "TestToDo",
                IsDone = false,
            };

            //var blog = controller.GetBlog("Blog2").Value;
            //Assert.Equal("http://blog2.com", blog.Url);

            //processors to handle the post
            //var processor = new AddToDoTestProcessor();


            //ACT 
            //method for processing the request
            toDoController.PostToDo(newToDo);
            var t = toDoController.GetNote(newToDo.Text);
            Assert.Equal(newToDo.Text, toDoController.GetNote(newToDo.Text).Text);

            //AddToDoResult

        }

    }
}
