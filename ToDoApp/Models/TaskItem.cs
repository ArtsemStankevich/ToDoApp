using System;
using System.ComponentModel.DataAnnotations;
using ToDoApp.Validators;
public class TaskItem
{
    private static int _idCounter = 1;

    public int Id { get; set; }

    public TaskItem()
    {
        Id = _idCounter++;
    }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(100, ErrorMessage = "Description can have up to 100 characters.")]
    [ScrumValidation]
    [Display(Name = "Task Description")]
    public string Description { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "Due date is required.")]
    [Display(Name = "Due Date")]
    public DateTime DueDate { get; set; }

    [Required(ErrorMessage = "Category ID is required.")]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "User ID is required.")]
    [Display(Name = "User")]
    public int UserId { get; set; }
}
