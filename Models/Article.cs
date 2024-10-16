namespace NewsApp.Models
{
    public class Article
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? UrlToImage { get; set; }
        public string? PublishedAt { get; set; }
        public Source? Source { get; set; }
    }

    public class Source
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}
