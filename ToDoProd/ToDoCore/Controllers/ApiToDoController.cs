using Microsoft.AspNetCore.Mvc;
using ToDoCore.Models;
using ToDoCore.Data;

namespace ToDoCore.Controllers
{
    [Route("/notes")] 
   
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApiContext database;

        public ApiController(ApiContext database)
        {
            this.database = database;
        }

        //[HttpGet("notes")]
        [HttpGet("/notes")]
        public ToDoNote[] Get(bool? completed)
        {
            if (completed == null)
            {
                return database.ToDoNotes.ToArray();

            }
            else if (completed == true)
            {
                return database.ToDoNotes.Where(m => m.IsDone == true).ToArray();

            }
            else if (completed == false)
            {
                return database.ToDoNotes.Where(m => m.IsDone == false).ToArray();
            }
            //kanske inte en så bra lösning
            else
            {
                return default;
            }
        }

        //[HttpGet("remaining")]
        [HttpGet("/remaining")]
        public int GetCount()
        {
            return database.ToDoNotes.Where(m => m.IsDone == false).Count();
        }

        //[HttpPost("notes")]
        [HttpPost]
       public ActionResult<ToDoNote> PostToDo(ToDoNote httpToDoNote)
        //public ActionResult PostToDo(ToDoNote httpToDoNote)
        {
            if (httpToDoNote is null)
            {
                throw new ArgumentNullException(nameof(httpToDoNote));
            }

            var dbToDoNote = new ToDoNote
                {
                    Text = httpToDoNote.Text,
                    IsDone = false
                };

                database.ToDoNotes.Add(dbToDoNote);
                database.SaveChanges();

                return CreatedAtAction(nameof(Get), new { id = dbToDoNote.Id }, dbToDoNote);
        }

        //[HttpPost("toggle-all")]
        [HttpPost("/toggle-all")]
        public void ToggleAll()
        {
            var notes = database.ToDoNotes.ToArray();

            if (notes.Length == 0)
            {
                return;
            }

            else if (notes.All(notes => notes.IsDone))
            {
                foreach (var note in notes)
                {
                    note.IsDone = false;
                }
            }

            else
            {
                foreach (var note in notes)
                {
                    note.IsDone = true;
                }
            }

            database.SaveChanges();
        }

        //[HttpPost("clear-completed")]
        [HttpPost("/clear-completed")]
        public void ClearCompletedNotes()
        {
            var completedNotes = database.ToDoNotes.Where(m => m.IsDone).ToArray();

            foreach (var note in completedNotes)
            {
                database.ToDoNotes.Remove(note);
            }
            database.SaveChanges();
        }

        //[HttpPut("notes/{id:int}")]
        [HttpPut("{id:int}")]
        public void ChangeNote(int id, ToDoNote updatedNote)
        {
            var noteToChange = database.ToDoNotes.FirstOrDefault(m => m.Id == id);

            if (noteToChange == null)
            {
                return;
            }

            else if (!noteToChange.IsDone)
            {
                noteToChange.IsDone = true;
            }

            else if (noteToChange.IsDone)
            {
                noteToChange.IsDone = false;
            }

            database.ToDoNotes.Update(noteToChange);
            database.SaveChanges();
        }

        //[HttpDelete("notes/{id:int}")]
        [HttpDelete("{id:int}")]
        public void DeleteNote(int id)
        {
            var noteToDelete = database.ToDoNotes.FirstOrDefault(m => m.Id == id);
            database.ToDoNotes.Remove(noteToDelete);
            database.SaveChanges();
        }

    }

}
