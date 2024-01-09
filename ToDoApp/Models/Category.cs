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

    [Required(ErrorMessage = "Pole jest wymagane")]
    [StringLength(50, ErrorMessage = "Nazwa kategorii może mieć maksymalnie 50 znaków")]
    public string Name { get; set; }

    public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
