using System;
using System.ComponentModel.DataAnnotations;

public class TaskItem
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane")]
    [StringLength(100, ErrorMessage = "Opis zadania może mieć maksymalnie 100 znaków")]
    public string Description { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "Pole jest wymagane")]
    public DateTime DueDate { get; set; }

    public int CategoryId { get; set; }
}
