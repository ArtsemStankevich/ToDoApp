using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class User
{
    private static int _idCounter = 1;

    public int Id { get; set; }

    public User()
    {
        Id = _idCounter++;
    }

    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, ErrorMessage = "Username can have up to 50 characters.")]
    [Display(Name = "User Name")]
    public string Username { get; set; }

    public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
