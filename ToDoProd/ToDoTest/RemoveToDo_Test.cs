using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoCore.Controllers;

namespace ToDoTest
{
    [Collection("Tests")]
    public class RemoveToDo_Test : IDisposable
    {
        //REQUIREMENTS
        // when an API-request to DeleteNote is made, we expect that
        // the specific ToDo is removed and the count is updated. 

        public RemoveToDo_Test(TestDatabaseFixture fixture)
       => Fixture = fixture;

        public TestDatabaseFixture Fixture { get; }

        public void Dispose()
             => Fixture.Cleanup();


        [Fact]
        public void Should_Throw_Exception_On_Attempt_To_Remove_Nonexisting_ToDo()
        {
            //ARRANGE

            //Initialize Db with four notes (see TestDataBaseFixture.cs )
            using (var context = Fixture.CreateContext())
            {
                var toDoController = new ApiController(context);

            //ACT & ASSERT

                Assert.Throws<Exception>(() => toDoController.DeleteNote(789));
            }
        }

        [Fact]
        public void Should_Remove_ToDo_With_A_Specified_Id()
        {
            //ARRANGE

            //Initialize Db with four notes (see TestDataBaseFixture.cs )
            using (var context = Fixture.CreateContext())
            {
                var toDoController = new ApiController(context);

            
            //ACT

                var initialResponse = toDoController.Get(null);
                var idToDelete = initialResponse[0].Id;
                toDoController.DeleteNote(idToDelete);
                var secondResponse = toDoController.Get(null);


            //ASSERT

                Assert.Equal(4, initialResponse.Count());
                Assert.Equal(3, secondResponse.Count());

                foreach (var response in secondResponse)
                {
                    Assert.NotEqual(response.Id, idToDelete);
                }
            }
        }
    }
}
