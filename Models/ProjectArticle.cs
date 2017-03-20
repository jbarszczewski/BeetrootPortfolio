using System;

namespace BeetrootPortfolio.Models
{
    public class ProjectArticle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public string HtmlContent { get; set; }
    }
}
