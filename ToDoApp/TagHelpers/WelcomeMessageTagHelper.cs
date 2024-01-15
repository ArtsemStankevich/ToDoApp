using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ToDoApp.TagHelpers;
[HtmlTargetElement("welcome-message", Attributes = "name")]
public class WelcomeMessageTagHelper : TagHelper
{
    public string Name { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Content.SetHtmlContent($"Hello, {Name}!");
    }
}