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
        if (ModelState.IsValid)
        {
            todo.Id = todos.Max(t => t.Id) + 1;
            todos.Add(todo);
            return RedirectToAction("Index");
        }

        return View(todo);
    }
    
// this function will edit a todo
    public IActionResult Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var todo = todos.FirstOrDefault(t => t.Id == id);

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
            var existingTodo = todos.FirstOrDefault(t => t.Id == todo.Id);

            if (existingTodo == null)
                return NotFound();

            existingTodo.Title = todo.Title;
            existingTodo.Description = todo.Description;
            existingTodo.IsDone = todo.IsDone;
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