using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ToDoApp.TagHelpers
{
    [HtmlTargetElement("task-status")]
    public class TaskStatusTagHelper : TagHelper
    {
        public bool IsDone { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";

            if (IsDone)
            {
                output.Attributes.SetAttribute("class", "text-success");
                output.Content.SetContent("Done");
            }
            else
            {
                output.Attributes.SetAttribute("class", "text-danger");
                output.Content.SetContent("Not done");
            }
        }
    }
}

