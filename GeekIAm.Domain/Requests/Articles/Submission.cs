namespace GeekIAm.Domain.Requests.Articles;

public class Submission
{
    public ArticleDetail Article { get; set; }
    public List<string> Tags { get; set; }
    public List<string> Categories { get; set; }
}