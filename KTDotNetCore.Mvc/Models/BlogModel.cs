namespace KTDotNetCore.Mvc.Models
{
    public class BlogModel
    {
        public List<BlogDataModels> BlogList { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageRowCount { get; set; }
    }
}
