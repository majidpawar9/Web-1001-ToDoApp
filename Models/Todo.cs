// Todo class and its properties
namespace Web_1001_ToDoApp.Models{
public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Due {get; set; }
    public bool IsDone { get; set; }
}
}