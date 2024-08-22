using PersonalPage.Models.Blog;

namespace PersonalPage.Models;

public class BlogPost
{
    public string Id { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }

    public List<Element> HtmlElements { get; set; }
}
