namespace PersonalPage.Models.Blog
{
    public class Element
    {
        public Guid Id { get; set; }
        public string TagName { get; set; }
        public string Class { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
    }
}
