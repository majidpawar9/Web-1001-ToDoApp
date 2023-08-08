using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_1001_ToDoApp.Models;

namespace Web_1001_ToDoApp.Controllers;

public class HomeController : Controller
{
    
    // Hardcoded list
    private static List<Todo> todos = new List<Todo>
    {
        new Todo { Id = 1, Title = "Buy groceries", Description = "Milk, Eggs, Bread",Due = "2023/12/01", IsDone = false },
        new Todo { Id = 2, Title = "Go for a run", Description = "Morning exercise",Due = "2023/01/17", IsDone = true },
    };

    public IActionResult Index()
    {
        return View(todos);
    }
    // this function is for creating a new todo
    public IActionResult Create()
    {
        return View();
    }
// this function is post the newly created todo
    [HttpPost]
    public IActionResult Create(Todo todo)
    {
            int newId = todos.Count > 0 ? todos.Max(t => t.Id) + 1 : 1;
            Todo newTodo = new Todo
            {
                Id = newId,
                Title = todo.Title,
                Description = todo.Description,
                Due = todo.Due,
                IsDone = todo.IsDone
            };
            todos.Add(newTodo);
            return RedirectToAction("Index");
    }

    
// this function will edit a todo
    public IActionResult Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var todo = todos.FirstOrDefault(t => t.Id == id);
        todo.Title = todo.Title;
        todo.Description = todo.Description;
        todo.IsDone = todo.IsDone;
        if (todo == null)
            return NotFound();
        

        return View(todo);
    }
// this function save the edited todo
    [HttpPost]
    public IActionResult Edit(Todo todo)
    {
        if (ModelState.IsValid)
        {
            
            todo = todos.FirstOrDefault(t => t.Id == todo.Id);
            todo.Title = todo.Title;
            todo.Description = todo.Description;
            todo.IsDone = todo.IsDone;
            todo.Due = todo.Due;
            todos.Add(todo);
            if (todo == null)
                return NotFound();
            
            
            return RedirectToAction("Index");
        }

        return View(todo);
    }

// this function will delete a todo
    public IActionResult Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var todo = todos.FirstOrDefault(t => t.Id == id);
        todos.Remove(todo);
        if (todo == null)
            return NotFound();

        return View(todo);
    }

// this function will delete a todo and update
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var todo = todos.FirstOrDefault(t => t.Id == id);

        if (todo != null)
            todos.Remove(todo);

        return RedirectToAction("Index");
    }
    
}