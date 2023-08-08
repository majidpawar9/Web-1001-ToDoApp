using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_1001_ToDoApp.Models;

namespace Web_1001_ToDoApp.Controllers;

public class HomeController : Controller
{
    private static List<Todo> todos = new List<Todo>
    {
        new Todo { Id = 1, Title = "Buy groceries", Description = "Milk, Eggs, Bread", IsDone = false },
        new Todo { Id = 2, Title = "Go for a run", Description = "Morning exercise", IsDone = true },
    };

    public IActionResult Index()
    {
        return View(todos);
    }

    public IActionResult Details(int? id)
    {
        if (id == null)
            return NotFound();

        var todo = todos.FirstOrDefault(t => t.Id == id);

        if (todo == null)
            return NotFound();

        return View(todo);
    }

    [HttpPost]
    public IActionResult Create(Todo todo)
    {
        if (ModelState.IsValid)
        {
            // Assign a new ID (for simplicity, you may need a more robust method in a real application)
            int newId = todos.Count + 1;
            todo.Id = newId;

            // Add the new Todo item to the list
            todos.Add(todo);

            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var todo = todos.FirstOrDefault(t => t.Id == id);

        if (todo == null)
            return NotFound();

        return View(todo);
    }

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

    public IActionResult Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var todo = todos.FirstOrDefault(t => t.Id == id);

        if (todo == null)
            return NotFound();

        return View(todo);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var todo = todos.FirstOrDefault(t => t.Id == id);

        if (todo != null)
            todos.Remove(todo);

        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult Update(Todo todo)
    {
        if (ModelState.IsValid)
        {
            var existingTodo = todos.Find(t => t.Id == todo.Id);
            if (existingTodo != null)
            {
                existingTodo.Title = todo.Title;
                existingTodo.Description = todo.Description;
                existingTodo.IsDone = todo.IsDone;
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        else
        {
            return BadRequest();
        }
    }
}