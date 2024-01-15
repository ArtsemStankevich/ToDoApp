using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Category
{
    private static int _idCounter = 1;

    public int Id { get; set; }

    public Category()
    {
        Id = _idCounter++;
    }

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(50, ErrorMessage = "Category name can have up to 50 characters.")]
    [Display(Name = "Category Name")]
    public string Name { get; set; }

    public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
