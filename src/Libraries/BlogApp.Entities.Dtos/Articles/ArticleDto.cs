namespace BlogApp.Entities.Dtos.Articles;
public class ArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int ReadTime { get; set; }
    public byte[]? Thumbnail { get; set; }
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublishedDate { get; set; }
}
