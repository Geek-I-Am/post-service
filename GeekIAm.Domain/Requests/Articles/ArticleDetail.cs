namespace GeekIAm.Domain.Requests.Articles;

public class ArticleDetail
{
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Author { get; set; }
    public DateTime Published { get; set; }
    public string Url { get; set; }

}