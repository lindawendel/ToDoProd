using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoCore.Controllers;
using ToDoCore.Models;

namespace ToDoTest
{
    [Collection("Tests")]
    public class AddToDoTest : IDisposable
    {
        //REQUIREMENTS
        // when data an API-request is made, we expect that
        // all details are included in the response and that the
        // database is updated

        public AddToDoTest(TestDatabaseFixture fixture)
        => Fixture = fixture;

        public TestDatabaseFixture Fixture { get; }
        public void Dispose()
             => Fixture.Cleanup();

        [Fact]
        public void Should_Return_PostToDo_Response_With_Details()
        {
            // ARRANGE

            using (var context = Fixture.CreateContext())
            {
                var toDoController = new ApiController(context);

                ToDoNote inputToDo = new ToDoNote
                {
                    Text = "TestToDo",
                    IsDone = false,
                };

                //ACT 

                var response = toDoController.PostToDo(inputToDo).Result as CreatedAtActionResult;
                ToDoNote responseToDo = response.Value as ToDoNote;


                //Assert
                Assert.Equal(responseToDo.Text, inputToDo.Text);
                Assert.Equal(responseToDo.IsDone, inputToDo.IsDone);
            }
        }
             
       
        [Fact]
        public void Should_Return_Database_Query_Response_With_Details_After_Posting_ToDo()
        {

            //ARRANGE
            using (var context = Fixture.CreateContext())
            {
                var toDoController = new ApiController(context);

                ToDoNote inputToDo = new ToDoNote
                {
                    Text = "TestToDo",
                    IsDone = false,
                };

                //ACT

                var postResponse = toDoController.PostToDo(inputToDo).Result as CreatedAtActionResult;
                ToDoNote responseToDo = postResponse.Value as ToDoNote;

                var dataBaseNote = context.ToDoNotes.Where(note => note.Id == responseToDo.Id).FirstOrDefault();


                //ASSERT
                Assert.Equal(dataBaseNote.Text, inputToDo.Text);
                Assert.Equal(dataBaseNote.IsDone, inputToDo.IsDone);
            }
        }


        [Fact]
        public void Should_Throw_Exception_For_Null_Request()
        {
            // ARRANGE
            using (var context = Fixture.CreateContext())
            { 
            var toDoController = new ApiController(context);

            //ACT AND ASSERT
            Assert.Throws<ArgumentNullException>(() => toDoController.PostToDo(null!));
            }
        }
    }
}
