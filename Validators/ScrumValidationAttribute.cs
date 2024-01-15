using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Validators;
public class ScrumValidationAttribute : ValidationAttribute
{
    public ScrumValidationAttribute()
    {
        ErrorMessage = "The field must contain the term 'SCRUM'.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string stringValue = value.ToString();
            if (stringValue.Contains("SCRUM", StringComparison.OrdinalIgnoreCase))
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult(ErrorMessage);
    }
}
